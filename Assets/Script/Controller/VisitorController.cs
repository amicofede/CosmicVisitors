using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitorController : MonoSingleton<VisitorController>
{

    public List<GameObject> visitorPrefab;
    public Visitor[] visitors;
    private Vector2 direction;
    private float speed = 1;
    private float moveDownOverTime = 0.5f;

    public const char VisitorShip = '#';
    public const char EmptyShip = '-';

    private string[] VisitorData = new string[]
        {
            "-------",
            "--####-",
            "-###-#-",
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
        BuildVisitorArmy();
        StartCoroutine(ReturnFire());
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
        for (int i = 0; i < visitors.Length; i++)
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

    private void BuildVisitorArmy()
    {
        for (int r = 0; r < VisitorData.Length; r++)
        {
            for (int c = 0; c < VisitorData[r].Length; c++)
            {
                switch (VisitorData[r][c])
                {
                    case EmptyShip:
                        continue;

                    case VisitorShip:
                        Array.Resize(ref visitors, visitors.Length + 1);
                        GameObject VisitorObj = Instantiate(visitorPrefab[UnityEngine.Random.Range(0, visitorPrefab.Count)], new Vector2(c - 3, (((VisitorData.Length - 1) - r)*1.5f) + 7.5f), Quaternion.Euler(0f, 0f, -90f), this.transform);
                        visitors[visitors.Length - 1] = VisitorObj.gameObject.GetComponent<Visitor>();
                        break;
                }
            }
        }
    }

    private IEnumerator ReturnFire()
    {
        int fireRate = 30;
        while (IsOneVisitorActive())
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

    private bool IsOneVisitorActive()
    {
        for (int i = 0; i < visitors.Length; i++)
        {
            if (visitors[i].gameObject.activeInHierarchy)
                return true;

        }
        return false;
    }

}
