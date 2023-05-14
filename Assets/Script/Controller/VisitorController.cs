using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VisitorController : MonoSingleton<VisitorController>
{

    public List<GameObject> visitorPrefab;
    public Visitor[] visitors;
    public List<GameObject> activeVisitors;
    private Vector2 direction;
    [SerializeField] private float initialSpeed = 4;
    [SerializeField] private float speed;
    private float moveDownOverTime = 0.5f;

    public const char VisitorShip = '#';
    public const char EmptyShip = '-';

    #region UnityMessages
    private new void Awake()
    {
        base.Awake();
        visitors = GetComponentsInChildren<Visitor>();
        direction = Vector2.right;
    }
    private void OnEnable()
    {
        EventController.VisitorAnimationStarted += DisableAI;
        EventController.VisitorAnimationFinished += EnableAI;

        EventController.VisitorHitBounds += ChangeDirection;
        EventController.BuildVisitorArmy += BuildVisitorArmy;

        EventController.PauseGameUI += DisableAI;
        EventController.ResumeGameUI += EnableAI;
        EventController.RestartGameUI += ClearVisitorArmy;
        EventController.GameOverUI += DisableAI;
    }
    private void OnDisable()
    {
        EventController.VisitorAnimationStarted -= DisableAI;
        EventController.VisitorAnimationFinished -= EnableAI;

        EventController.VisitorHitBounds -= ChangeDirection;
        EventController.BuildVisitorArmy -= BuildVisitorArmy;

        EventController.PauseGameUI -= DisableAI;
        EventController.ResumeGameUI -= EnableAI;
        EventController.RestartGameUI -= ClearVisitorArmy;
        EventController.GameOverUI -= DisableAI;
    }
    #endregion

    #region AI
    private void EnableAI()
    {
        StartCoroutine(ReturnFire());
        StartCoroutine(MoveHorizontal());
    }
    private void DisableAI()
    {
        StopCoroutine(ReturnFire());
        StopCoroutine(MoveHorizontal());
    }
    private bool IsOneVisitorActive()
    {
        for (int i = 0; i < visitors.Length; i++)
        {
            if (visitors[i].gameObject.activeInHierarchy)
                return true;
        }
        ClearVisitorArmy();
        EventController.RaiseOnStageCleared();
        return false;
    }
    public void OnVisitorKilled(GameObject _visitor)
    {
        activeVisitors.Remove(_visitor);
        SetSpeed();
    }
    #endregion

    #region Movement
    private void SetSpeed()
    {
        speed = initialSpeed - (activeVisitors.Count / 5);
    }
    public void ChangeDirection(bool _hitBounds)
    {
        StartCoroutine(MoveTransition(_hitBounds));
    }
    private IEnumerator MoveTransition(bool _hitBounds)
    {
        direction = Vector2.down;
        yield return new WaitForSeconds(moveDownOverTime / speed);
        if (_hitBounds)
        {
            direction = Vector2.left;
        }
        else
        {
            direction = Vector2.right;
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
            yield return null;
        }
    }
    #endregion

    #region Spawn
    private void BuildVisitorArmy()
    {
        EventController.RaiseOnVisitorAnimationStarted();
        ClearVisitorArmy();
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
                        GameObject VisitorObj = Instantiate(visitorPrefab[UnityEngine.Random.Range(0, visitorPrefab.Count)], new Vector2(c - 3, (((StageController.Instance.VisitorArmy.Length - 1) - r) * 1.5f) + 6f), Quaternion.Euler(0f, 0f, -90f), this.transform);
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

    private void ClearVisitorArmy()
    {
        activeVisitors.Clear();
        visitors = new Visitor[0];
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
        SetSpeed();
        EventController.RaiseOnSpaceshipAnimationFinished();
        EventController.RaiseOnVisitorAnimationFinished();
    }
    #endregion

    #region Shoot
    private IEnumerator ReturnFire()
    {
        int fireRate = 30;
        while (true)
        {
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
