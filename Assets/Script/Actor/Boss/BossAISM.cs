using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAISM : MonoBehaviour, IDamageable
{
    private StateMachine stateMachine;

    [Tooltip("Add an Empty Object children in the location of cannonDx and link to this.")]
    [SerializeField] private Transform cannonDx;
    [Tooltip("Add an Empty Object children in the location of cannonSx and link to this.")]
    [SerializeField] private Transform cannonSx;
    [Tooltip("Add an Empty Object children in the location of OrbitCannon and link to this.")]
    [SerializeField] private Transform OrbitCannon;
    [Tooltip("Add an Empty Object children in the location of SolarBeam and link to this.")]
    [SerializeField] private Transform SolarBeam;
    [Tooltip("Add the Prefab of the SolarBeam.")]
    [SerializeField] private GameObject SolarBeamPrefab;
    [Tooltip("Add the Object children of the shield.")]
    [SerializeField] private GameObject shield;


    private Vector3 startPosition;
    private Vector3 playingPosition;

    [SerializeField] private int maxLifePoint;
    [SerializeField] private int currentLifePoint;

    private float speed;
    private float timeBetweenShoot;

    private bool boundsHitted;
    public bool BoundsHitted { get { return boundsHitted; } }


    #region UnityMessages
    private void Awake()
    {
        var rigidBody2D = GetComponent<Rigidbody2D>();
        var spaceship = FindAnyObjectByType<Spaceship>();

        stateMachine = new StateMachine();

        startPosition = new Vector3(0f, 19f, 0f);
        playingPosition = new Vector3(0f, 13f, 0f);

        speed = 3f;
        timeBetweenShoot = 0.75f;

        maxLifePoint = 100;
        currentLifePoint = maxLifePoint;

        shield.SetActive(false);

        var EnterPhase = new EnterPhase(this, rigidBody2D, startPosition, playingPosition);
        var PhaseOne = new PhaseOne(this, rigidBody2D, playingPosition, speed, timeBetweenShoot, cannonDx, cannonSx);
        var PhaseTwo = new PhaseTwo(this, rigidBody2D, playingPosition, OrbitCannon, spaceship);
        var PhaseThree = new PhaseThree(this, rigidBody2D, playingPosition, SolarBeam, SolarBeamPrefab, shield);
        var PhaseTransition = new PhaseTransition(this, playingPosition, shield, rigidBody2D);

        At(EnterPhase, PhaseOne, EnterPhaseEnded());
        At(PhaseOne, PhaseTransition, PhaseOneEnded());
        At(PhaseTransition, PhaseTwo, PhaseTransitionOneEnded());
        At(PhaseTwo, PhaseTransition, PhaseTwoEnded());
        At(PhaseTransition, PhaseThree, PhaseTransitionTwoEnded());

        switch (StageController.Instance.BossPhase)
        {
            case StageController.PhaseType.EnterPhase:
                stateMachine.SetState(EnterPhase);
                break;
            case StageController.PhaseType.PhaseOne:
                stateMachine.SetState(PhaseOne);
                break;
            case StageController.PhaseType.PhaseTwo:
                stateMachine.SetState(PhaseTwo);
                break;
            case StageController.PhaseType.PhaseThree:
                stateMachine.SetState(PhaseThree);
                break;
            case StageController.PhaseType.PhaseTransition:
                stateMachine.SetState(PhaseTransition);
                break;
            //default:
            //    stateMachine.SetState(EnterPhase);
            //    break;
        }

        void At(IState to, IState from, Func<bool> condition)
        {
            stateMachine.AddTransition(to, from, condition);
        }

        Func<bool> EnterPhaseEnded() => () => EnterPhase.IsEntered;
        Func<bool> PhaseOneEnded() => () => currentLifePoint <= (maxLifePoint * 2 / 3);
        Func<bool> PhaseTransitionOneEnded() => () => (gameObject.transform.position == playingPosition && PhaseTransition.TransitionEnded && currentLifePoint > (maxLifePoint * 1 / 3));
        Func<bool> PhaseTwoEnded() => () => currentLifePoint <= (maxLifePoint * 1 / 3);
        Func<bool> PhaseTransitionTwoEnded() => () => (gameObject.transform.position == playingPosition && PhaseTransition.TransitionEnded && currentLifePoint <= (maxLifePoint * 1 / 3));
    }

    private void Update()
    {
        stateMachine.Tick();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.GetComponent<Laser>() && !shield.activeSelf)
        {
            OnTakeDamage();
        }
        else if (collision.collider.gameObject.GetComponent<ArenaController>())
        {
            HitBounds();
        }
    }
    public bool HitBounds()
    {
        boundsHitted = (gameObject.transform.position.x >= 0);
        return gameObject.transform.position.x >= 0;
    }

    #endregion

    #region Interface Method
    public void OnTakeDamage()
    {
        currentLifePoint--;
        if (currentLifePoint <= 0)
        {

            OnKill();
        }
    }

    public void OnKill()
    {
        EventController.RaiseOnBossDeath();
        EventController.RaiseOnStageComplete();
        Destroy(gameObject);
    }
    #endregion
}
