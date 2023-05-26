using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseThree : IState
{
    private BossAISM boss;

    private Vector3 playingPosition;

    private Vector3 nextSolarPosition;
    private int randomPosition;

    private Transform SolarLocation;

    private GameObject solarBeam;
    private GameObject solarBeamPrefab;
    private GameObject shield;

    private Rigidbody2D rigidBody2D;

    private bool isSolarShooting;
    private bool isBeamFinished;

    public PhaseThree(BossAISM _boss, Rigidbody2D _rigidbody2D, Vector3 _playingPosition, Transform _solarBeam, GameObject _solarBeamPrefab, GameObject _shield)
    {
        boss = _boss;
        playingPosition = _playingPosition;
        SolarLocation = _solarBeam;
        solarBeamPrefab = _solarBeamPrefab;
        shield = _shield;
        rigidBody2D = _rigidbody2D;
    }
    public void OnEnter()
    {
        EventController.RaiseOnSpaceshipEnableInput();
        isSolarShooting = false;
        boss.gameObject.transform.position = playingPosition;
        shield.SetActive(true);
        randomPosition = Random.Range(0, 1) * 2 - 1;
        nextSolarPosition = new Vector3(2 * randomPosition, 13, 0);
    }

    public void OnExit()
    {
    }

    public void Tick()
    {
        if (!isSolarShooting)
        {
            SlowMove();
        }
        else
        {
            SolarBeamShoot();
        }
    }

    public void SlowMove()
    {
        if (nextSolarPosition.x < 0)
        {
            if (boss.gameObject.transform.position.x > nextSolarPosition.x)
            {
                shield.SetActive(true);
                rigidBody2D.MovePosition(boss.gameObject.transform.position + (Vector3)Vector2.left * 20 * Time.fixedDeltaTime);
            }
            else
            {
                solarBeam = Factory.Instance.activateSolarBeam();
                SetNextSolarPosition();
                isSolarShooting = true;
            }
        }
        else
        {
            if (boss.gameObject.transform.position.x < nextSolarPosition.x)
            {
                shield.SetActive(true);
                rigidBody2D.MovePosition(boss.gameObject.transform.position + (Vector3)Vector2.right * 20 * Time.fixedDeltaTime);
            }
            else
            {
                SetNextSolarPosition();
                solarBeam = Factory.Instance.activateSolarBeam();
                isSolarShooting = true;
            }
        }
    }

    public void SetNextSolarPosition()
    {
        randomPosition = -randomPosition;
        nextSolarPosition = new Vector3(2 * randomPosition, 13, 0);
        shield.SetActive(false);
    }

    public void SolarBeamShoot()
    {
        //if (solarBeam.activeSelf == false)
        //{
        //    isSolarShooting = false;
        //}
        //else
        //{
            solarBeam.transform.position = SolarLocation.position;
            solarBeam.transform.rotation = Quaternion.Euler(0 , 0, -90);
        //}
    }
}
