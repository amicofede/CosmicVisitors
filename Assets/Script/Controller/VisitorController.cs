using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitorController : MonoSingleton<VisitorController>
{
    private Visitor[] visitors;
    private Vector2 direction;
    private float speed = 3;
    private float moveDownOverTime = 0.2f;

    #region UnityMessages
    private void Awake()
    {
        visitors = GetComponentsInChildren<Visitor>();
        direction = Vector2.right;
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


}
