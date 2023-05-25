using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseThree : IState
{
    private BossAISM boss;

    private Vector3 playingPosition;

    private GameObject shield;

    private Rigidbody2D rigidBody2D;

    private float shieldUPCD;



    public PhaseThree(BossAISM _boss, Rigidbody2D _rigidbody2D, Vector3 _playingPosition, Transform _solarBeam, GameObject _shield)
    {
        boss = _boss;
        playingPosition = _playingPosition;
        shield = _shield;
        rigidBody2D = _rigidbody2D;

    }
    public void OnEnter()
    {
        boss.gameObject.transform.position = playingPosition;
        shield.SetActive(false);
    }

    public void OnExit()
    {
    }

    public void Tick()
    {
    }
}
