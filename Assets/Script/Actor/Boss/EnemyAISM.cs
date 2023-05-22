//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class EnemyAISM : MonoBehaviour, IDamageable
//{
//    [Tooltip("Link the scriptable object of this.")]
//    [SerializeField] private SpaceshipSO DataSO;
//    [Tooltip("Add an Empty Object children in the location of cannonDx and link to this.")]
//    [SerializeField] private Transform cannonDx;
//    [Tooltip("Add an Empty Object children in the location of cannonSx and link to this.")]
//    [SerializeField] private Transform cannonSx;
//    [Tooltip("Add an Empty Object children in the location of OrbitCannon and link to this.")]
//    [SerializeField] private Transform OrbitCannon;
//    [Tooltip("Add the Object children of the shield.")]
//    [SerializeField] private GameObject shield;


//    [Header("Stats")]
//    [SerializeField] private int maxLifePoint;
//    [SerializeField] private int currentLifePoint;
//    [SerializeField] private int initialSpeed;

//    [SerializeField] private int fireRate;
//    public int FireRate { get { return fireRate; } }

//    [SerializeField] private int speed;
//    public int Speed { get { return speed; } }

//    [Header("Position")]
//    [SerializeField] private Vector3 startPosition;
//    [SerializeField] private Vector3 playingPosition;
//    private Vector3 nextOrbitPosition;


//    private bool ShieldUP;
//    private bool boundsHitted;

//    private bool orbitShooting;
//    public bool OrbitShooting { get { return orbitShooting; } }

//    private Rigidbody2D rigidBody;

//    private IState currentBossState;

//    #region UnityMessages
//    private void Awake()
//    {
//        maxLifePoint = 100;
//        currentLifePoint = maxLifePoint;
//        currentLifePoint = 68;
//        initialSpeed = 1;
//        speed = initialSpeed;
//        startPosition = new Vector3(0f, 19f, 0f);
//        playingPosition = new Vector3(0f, 13f, 0f);
//        orbitShooting = false;
//        ShieldUP = false;
//        rigidBody = GetComponent<Rigidbody2D>();
//        shield.SetActive(false);

//        ChangeState(new BossEnteringPhase(startPosition, playingPosition));
//    }
//    private void Update()
//    {
//        currentBossState.UpdateState();
//    }

//    private void OnCollisionEnter2D(Collision2D collision)
//    {
//        if (collision.collider.gameObject.GetComponent<Laser>() && !ShieldUP)
//        {
//            OnTakeDamage();
//        }
//        else if (collision.collider.gameObject.GetComponent<ArenaController>())
//        {
//            HitBounds();
//        }
//    }
//    #endregion

//    #region AI
//    public void ChangeState(IState _newState)
//    {
//        Debug.Log(_newState.ToString());

//        currentBossState?.ExitState();
//        currentBossState = _newState;
//        currentBossState?.EnterState(this);
//    }
//    private bool ChangePhase()
//    {
//        if (currentLifePoint < ((maxLifePoint * 2) / 3))
//        {
//            StopAllCoroutines();
//            ChangeState(new TransitionPhase(gameObject.transform.position, playingPosition));
//            return currentLifePoint < ((maxLifePoint * 2) / 3);
//        }
//        return false;
//    }
//    #endregion

//    #region Transition_Phase
//    public void ActivateShield()
//    {
//        ShieldUP = true;
//        shield.SetActive(true);
//        StartCoroutine(ShieldON());
//    }

//    private IEnumerator ShieldON()
//    {
//        while (gameObject.transform.position.x != playingPosition.x)
//        {
//            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, playingPosition, 1 * Time.deltaTime);
//            yield return null;
//        }
//        yield return new WaitForSeconds(1);
//        ShieldUP = false;
//        shield.SetActive(false);
//        if (currentLifePoint >= (maxLifePoint / 3))
//        {
//            nextOrbitPosition = new Vector3(Random.Range(-3f, 3f), 13f, 0f);
//            ChangeState(new Phase2(nextOrbitPosition));
//        }
//        else
//        {
//            ChangeState(new Phase3());
//        }
//    }
//    #endregion

//    #region Phase_1
//    public bool HitBounds()
//    {
//        boundsHitted = gameObject.transform.position.x > 0;
//        return gameObject.transform.position.x > 0;
//    }
//    public void MoveHorizontal()
//    {
//        StartCoroutine(MoveAnimation());
//    }
//    private IEnumerator MoveAnimation()
//    {
//        yield return new WaitForSeconds(1);
//        while (!ChangePhase())
//        {
//            if (boundsHitted)
//            {
//                rigidBody.MovePosition(gameObject.transform.position + (Vector3)Vector2.left * speed * Time.fixedDeltaTime);
//            }
//            else
//            {
//                rigidBody.MovePosition(gameObject.transform.position + (Vector3)Vector2.right * speed * Time.fixedDeltaTime);
//            }
//            yield return new WaitForFixedUpdate();
//        }
//    }
//    public void SimpleShoot()
//    {
//        StartCoroutine(ShootPhase1());
//    }
//    private IEnumerator ShootPhase1()
//    {
//        while (!ChangePhase())
//        {
//            yield return new WaitForSeconds(1f);
//            GameObject BossLaserShooted1;
//            GameObject BossLaserShooted2;
//            BossLaserShooted1 = Factory.Instance.activateBossLaser();
//            BossLaserShooted2 = Factory.Instance.activateBossLaser();
//            BossLaserShooted1.SetActive(true);
//            BossLaserShooted2.SetActive(true);
//            BossLaserShooted1.transform.position = cannonSx.transform.position;
//            BossLaserShooted2.transform.position = cannonDx.transform.position;
//        }
//        yield return null;
//    }
//    #endregion

//    #region Phase_2
//    public void OnOrbitShoot()
//    {
//        orbitShooting = true;
//        StartCoroutine(OrbitShoot());
//    }

//    private IEnumerator OrbitShoot()
//    {
//        int orbitLaserCount = 0;
//        while (orbitLaserCount < 3)
//        {
//            orbitLaserCount++;
//            GameObject BossLaserOrbit;
//            BossLaserOrbit = Factory.Instance.activateBossLaserOrbit();
//            BossLaserOrbit.SetActive(true);
//            BossLaserOrbit.transform.position = new Vector3(OrbitCannon.transform.position.x + orbitLaserCount, OrbitCannon.transform.position.y, OrbitCannon.transform.position.z);
//        }

//        yield return null;
//    }

//    #endregion

//    #region Interface Method
//    public void OnTakeDamage()
//    {
//        currentLifePoint--;
//        if (currentLifePoint <= 0)
//        {
//            OnKill();
//        }
//    }

//    public void OnKill()
//    {
//        Destroy(gameObject);
//    }
//    #endregion

//}
