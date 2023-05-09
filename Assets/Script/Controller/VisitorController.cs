using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitorController : MonoSingleton<VisitorController>
{
    public List<GameObject> visitorPrefab;
    public Visitor[] visitors;
    private Vector2 direction;
    private float speed = 3;
    private float moveDownOverTime = 0.2f;

    public const char ALIEN_TILE = '#';
    public const char EMPTY_TILE = '-';

    private string[] AlienData = new string[]
        {
            "-------",
            "--####-",
            "-###---",
            "--####-",
            "-###---",
            "--#--#-",
            "-------",
        };

    #region UnityMessages
    private void Awake()
    {
        base.Awake();
        visitors = GetComponentsInChildren<Visitor>();
        direction = Vector2.right;
        BuildMonsterArmy();
    }
    private void OnEnable()
    {
        EventController.OnVisitorHitBounds += ChangeDirection;
    }
    private void OnDisable()
    {
        EventController.OnVisitorHitBounds -= ChangeDirection;
    }
    private void FixedUpdate()
    {
        for (int i = 0; i < visitorPrefab.Count; i++)
        {
            if (!visitors[i].gameObject.activeInHierarchy) continue;

            visitors[i].Move(direction, speed * Time.fixedDeltaTime);
        }
    }
    #endregion

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

    private void SpawnAlien(int _r, int _c)
    {
        GameObject Alien = Instantiate(visitorPrefab[Random.Range(0, visitorPrefab.Count)], new Vector2(_c - 3, (((AlienData.Length - 1) - _r)*1.5f) + 7.5f), Quaternion.Euler(0f, 0f, -90f), this.transform);

    }

    private void BuildMonsterArmy()
    {
        for (int r = 0; r < AlienData.Length; r++)
        {
            for (int c = 0; c < AlienData[r].Length; c++)
            {
                switch (AlienData[r][c])
                {
                    case EMPTY_TILE:
                        continue;

                    case ALIEN_TILE:
                        SpawnAlien(r, c);
                        break;
                }
            }
        }
    }


}
