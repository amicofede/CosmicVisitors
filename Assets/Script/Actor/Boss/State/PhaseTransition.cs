using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseTransition : IState
{
    private BossAISM2 boss;

    private Vector3 playingPosition;
    private Vector3 currentPosition;

    private GameObject shield;


    private float shieldUPCD;
    private bool transitionEnded;
    public bool TransitionEnded { get { return transitionEnded; } }


    public PhaseTransition(BossAISM2 _boss, Vector3 _playingPositon, GameObject _shield)
    {
        boss = _boss;
        playingPosition = _playingPositon;
        shield = _shield;
    }
    public void OnEnter()
    {
        shield.SetActive(true);
        transitionEnded = false;
        shieldUPCD = 0;
    }

    public void OnExit()
    {
        transitionEnded = false;
    }

    public void Tick()
    {
        //Debug.Log("Tick");

        if (boss.gameObject.transform.position.x != playingPosition.x)
        {
            currentPosition = boss.gameObject.transform.position;
            boss.gameObject.transform.position = Vector2.MoveTowards(currentPosition, playingPosition, 1 * Time.deltaTime);
        }
        else
        {
            if (shieldUPCD <= 1)
            {
                //Debug.Log(shieldUPCD);
                shieldUPCD += Time.deltaTime;
            }
            else
            {
                shield.SetActive(false);
                transitionEnded = true;
            }
        }
    }
}
