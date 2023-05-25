using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseThree : IState
{
    private BossAISM boss;

    private Vector3 playingPosition;

    private Vector3 nextSolarPosition;
    private int randomPosition;

    private GameObject shield;

    private Rigidbody2D rigidBody2D;

    private bool isSolarShooting;
    private float particleBeamTimer;

    public PhaseThree(BossAISM _boss, Rigidbody2D _rigidbody2D, Vector3 _playingPosition, Transform _solarBeam, GameObject _shield)
    {
        boss = _boss;
        playingPosition = _playingPosition;
        shield = _shield;
        rigidBody2D = _rigidbody2D;

    }
    public void OnEnter()
    {
        EventController.RaiseOnSpaceshipEnableInput();
        isSolarShooting = false;
        boss.gameObject.transform.position = playingPosition;
        shield.SetActive(false);
        randomPosition = Random.Range(0, 1) * 2 - 1;
        nextSolarPosition = new Vector3(randomPosition * Random.Range(0f, 3f), 13f, 0f);
        particleBeamTimer = 0.5f;
    }

    public void OnExit()
    {
    }

    public void Tick()
    {
        if (!isSolarShooting)
        {
            QuickMove();
        }
        else
        {
            SolarBeamShoot();
        }
    }

    public void QuickMove()
    {
        if (nextSolarPosition.x < 0)
        {
            if (boss.gameObject.transform.position.x > nextSolarPosition.x)
            {
                rigidBody2D.MovePosition(boss.gameObject.transform.position + (Vector3)Vector2.left * 20 * Time.fixedDeltaTime);
            }
            else
            {
                isSolarShooting = true;
                randomPosition = -randomPosition;
                nextSolarPosition = new Vector3(randomPosition * Random.Range(0f, 3f), 13f, 0f);
                shield.SetActive(true);
            }
        }
        else
        {
            if (boss.gameObject.transform.position.x < nextSolarPosition.x)
            {
                rigidBody2D.MovePosition(boss.gameObject.transform.position + (Vector3)Vector2.right * 20 * Time.fixedDeltaTime);
            }
            else
            {
                isSolarShooting = true;
                randomPosition = -randomPosition;
                nextSolarPosition = new Vector3(randomPosition * Random.Range(0f, 3f), 13f, 0f);
                shield.SetActive(true);
            }
        }
    }

    public void SolarBeamShoot()
    {

    }
}
