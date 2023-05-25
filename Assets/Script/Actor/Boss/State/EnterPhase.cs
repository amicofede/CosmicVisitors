using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterPhase : IState
{
    private BossAISM boss;

    private Vector3 startPosition;
    private Vector3 currentPosition;
    private Vector3 playingPosition;


    private Rigidbody2D rigidBody2D;


    private bool isEntered;
    public bool IsEntered { get { return isEntered; } }
    public EnterPhase(BossAISM _boss, Rigidbody2D _rigidBody2D, Vector3 _startPosition, Vector3 _playingPosition)
    {
        boss = _boss;
        rigidBody2D = _rigidBody2D;
        startPosition = _startPosition;
        playingPosition = _playingPosition;
    }
    public void OnEnter()
    {
        rigidBody2D.isKinematic = true;
        boss.gameObject.transform.position = startPosition;
        rigidBody2D.constraints = RigidbodyConstraints2D.None;
        isEntered = false;
    }

    public void OnExit()
    {
        rigidBody2D.isKinematic = false;
        rigidBody2D.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
    }

    public void Tick()
    {
        if (boss.gameObject.transform.position.y > playingPosition.y)
        {
            currentPosition = boss.gameObject.transform.position;
            Vector2 movement = (playingPosition - currentPosition).normalized;
            rigidBody2D.MovePosition((Vector2)currentPosition + movement * 1 * Time.fixedDeltaTime);
        }
        else
        {
            isEntered = true;
        }
    }
}
