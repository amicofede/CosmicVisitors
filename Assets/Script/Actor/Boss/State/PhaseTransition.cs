using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseTransition : IState
{
    private BossAISM boss;

    private Vector3 playingPosition;
    private Vector3 startPosition;
    private Vector3 currentPosition;

    private GameObject shield;

    private Rigidbody2D rigidBody2D;

    private float shieldUPCD;
    private bool transitionEnded;
    public bool TransitionEnded { get { return transitionEnded; } }


    public PhaseTransition(BossAISM _boss, Vector3 _playingPosition, GameObject _shield, Rigidbody2D _rigidbody2D)
    {
        boss = _boss;
        playingPosition = _playingPosition;
        shield = _shield;
        rigidBody2D = _rigidbody2D;
    }
    public void OnEnter()
    {
        startPosition = boss.transform.position;
        shield.SetActive(true);
        transitionEnded = false;
        shieldUPCD = 0;
    }

    public void OnExit()
    {
    }


    public void Tick()
    {
        if (boss.gameObject.transform.position != playingPosition)
        {
            ReturnToPlayingPosition();
        }
        else
        {
            ShieldUP();
        }
    }

    public void ReturnToPlayingPosition()
    {
        if (startPosition.x > 0)
        {
            currentPosition = boss.gameObject.transform.position;
            Vector2 movement = (playingPosition - currentPosition).normalized;
            rigidBody2D.MovePosition((Vector2)currentPosition + movement * 1 * Time.fixedDeltaTime);
            if (currentPosition.x < 0)
            {
                boss.gameObject.transform.position = playingPosition;
            }
        }
        else
        {
            currentPosition = boss.gameObject.transform.position;
            Vector2 movement = (playingPosition - currentPosition).normalized;
            rigidBody2D.MovePosition((Vector2)currentPosition + movement * 1 * Time.fixedDeltaTime);
            if (currentPosition.x > 0)
            {
                boss.gameObject.transform.position = playingPosition;
            }

        }
    }

    public void ShieldUP()
    {
        if (shieldUPCD <= 1)
        {
            shieldUPCD += Time.deltaTime;
        }
        else
        {
            shield.SetActive(false);
            transitionEnded = true;
        }
    }
}
