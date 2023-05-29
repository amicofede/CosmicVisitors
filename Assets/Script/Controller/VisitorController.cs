using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class VisitorController : Utility.MonoSingleton<VisitorController>
{

    [SerializeField] private Visitor[] visitors;
    [SerializeField] private List<GameObject> activeVisitors;

    [SerializeField] private GameObject BossPrefab;

    private Vector2Int direction;
    private float maxSpeed;
    private float speed;
    private float moveDownOverTime = 0.5f;
    private float maxFireRate;
    private float fireRate;

    private bool armySpawned;

    public const char VisitorShip = '#';
    public const char EmptyShip = '-';

    #region UnityMessages
    private new void Awake()
    {
        base.Awake();
        armySpawned = false;
        direction = new Vector2Int((UnityEngine.Random.Range(0, 1) * 2 -1), 0);
        maxSpeed = 3f;
        maxFireRate = 50f;
        visitors = GetComponentsInChildren<Visitor>();
        activeVisitors = new List<GameObject>();
    }
    private void OnEnable()
    {
        EventController.VisitorHitBounds += ChangeDirection;
        EventController.BuildVisitorArmy += BuildVisitorArmy;
        EventController.PauseGameUI += DisableAI;
        EventController.ResumeGameUI += EnableAI;

        EventController.BossSpawn += SpawnBoss;

        EventController.RestartGameUI += ClearVisitorArmy;
        EventController.RestartGameUI += DisableAI;
        EventController.GameOverUI += DisableAI;
    }
    private void OnDisable()
    {
        EventController.VisitorHitBounds -= ChangeDirection;
        EventController.BuildVisitorArmy -= BuildVisitorArmy;
        EventController.PauseGameUI -= DisableAI;
        EventController.ResumeGameUI -= EnableAI;

        EventController.BossSpawn -= SpawnBoss;

        EventController.RestartGameUI -= ClearVisitorArmy;
        EventController.RestartGameUI -= DisableAI;
        EventController.GameOverUI -= DisableAI;
    }
    #endregion

    #region AI
    private void EnableAI()
    {
        if (!StageController.Instance.IsBossFight && armySpawned)
        {
            StartCoroutine(ReturnFire());
            StartCoroutine(MoveHorizontal());
        }
    }
    private void DisableAI()
    {
        StopCoroutine(ReturnFire());
        StopCoroutine(MoveHorizontal());
    }
    private bool IsOneVisitorActive()
    {
        if(armySpawned)
        {
            for (int i = 0; i < visitors.Length; i++)
            {
                if (visitors[i].gameObject.activeInHierarchy)
                    return true;
            }
            DisableAI();
            EventController.RaiseOnStageComplete();
        }
        armySpawned = false;
        return false;
    }
    public void OnVisitorKilled(GameObject _visitor)
    {
        activeVisitors.Remove(_visitor);
        SetSpeed();
        EventController.RaiseOnVisitorKilled();
    }
    #endregion

    #region Movement
    private void SetSpeed()
    {
        speed = 1f + (maxSpeed * (1f - (((float)activeVisitors.Count) / (float)visitors.Length)));
        if (speed <= 0)
        {
            speed = 1;
        }
    }
    public void ChangeDirection(bool _hitBounds)
    {
        StartCoroutine(MoveTransition(_hitBounds));
    }
    private IEnumerator MoveTransition(bool _hitBounds)
    {
        direction = Vector2Int.down;
        yield return new WaitForSeconds(moveDownOverTime / speed);
        if (_hitBounds)
        {
            direction = Vector2Int.left;
        }
        else
        {
            direction = Vector2Int.right;
        }
    }
    private IEnumerator MoveHorizontal()
    {
        while (IsOneVisitorActive())
        {
            for (int i = 0; i < visitors.Length; i++)
            {
                if (!visitors[i].gameObject.activeInHierarchy) continue;

                visitors[i].Move(direction, speed * Time.fixedDeltaTime);
            }
            yield return new WaitForFixedUpdate();
        }
    }
    #endregion

    #region Spawn
    private void ClearVisitorArmy()
    {
        for (int i = visitors.Length - 1; i >= 0; i--)
        {
            visitors[i].OnKill();
        }
        Debug.Log("clear");
        DisableAI();
        activeVisitors.Clear();
        visitors = new Visitor[0];
        armySpawned = false;
    }
    private void BuildVisitorArmy()
    {
        armySpawned = false;
        DisableAI();
        activeVisitors.Clear();
        visitors = new Visitor[0];
        for (int r = 0; r < StageController.Instance.VisitorArmy.Length; r++)
        {
            for (int c = 0; c < StageController.Instance.VisitorArmy[r].Length; c++)
            {
                switch (StageController.Instance.VisitorArmy[r][c])
                {
                    case EmptyShip:
                        continue;

                    case VisitorShip:
                        Array.Resize(ref visitors, visitors.Length + 1);
                        GameObject VisitorObj = Factory.Instance.ActivateRandomVisitors();
                        VisitorObj.transform.position = new Vector2(c - 3, (((StageController.Instance.VisitorArmy.Length - 1) - r) * 1.5f) + 9f);
                        VisitorObj.transform.rotation = Quaternion.Euler(0f, 0f, -90f);
                        //VisitorObj.transform.SetParent(gameObject.transform);
                        visitors[visitors.Length - 1] = VisitorObj.gameObject.GetComponent<Visitor>();
                        visitors[visitors.Length - 1].gameObject.SetActive(false);
                        break;
                }
            }
        }
        var rng = new System.Random();
        var keys = visitors.Select(e => rng.NextDouble()).ToArray();
        Array.Sort(keys, visitors);
        StartCoroutine(SpawnVisitor());
    }

    private IEnumerator SpawnVisitor()
    {
        for(int i = 0;i < visitors.Length;i++)
        {
            yield return new WaitForSeconds(0.1f);
            visitors[i].gameObject.SetActive(true);
            activeVisitors.Add(visitors[i].gameObject);
        }
        yield return new WaitForSeconds(1f);
        armySpawned = true;
        SetSpeed();
        EventController.RaiseOnSpaceshipEnableInput();
        EnableAI();
    }

    private void SpawnBoss()
    {
        Instantiate(BossPrefab);
    }

    #endregion

    #region Shoot
    private void SetFireRate()
    {
        fireRate = 10f + (maxFireRate * (1f - (((float)activeVisitors.Count) / (float)visitors.Length)));
        if (fireRate < 10)
        {
            fireRate = 10;
        }
    }
    private IEnumerator ReturnFire()
    {
        while (IsOneVisitorActive())
        {
            SetFireRate();
            yield return new WaitForSeconds(UnityEngine.Random.Range(1,3));
            for (int i = 0; i < visitors.Length; i++)
            {
                if (!visitors[i].gameObject.activeInHierarchy) continue;
                if (UnityEngine.Random.Range(1,100) < fireRate)
                {
                    visitors[i].OnShoot();
                }
            }
        }
    }
    #endregion
}
