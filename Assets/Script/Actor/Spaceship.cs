using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

public class Spaceship : MonoBehaviour, IDamageable
{
    [Tooltip("Link the scriptable object of this.")]
    [SerializeField] private SpaceshipSO DataSO;
    [Tooltip("Add an Empty Object children in the location of cannonDx and link to this.")]
    [SerializeField] private Transform cannonDx;
    [Tooltip("Add an Empty Object children in the location of cannonSx and link to this.")]
    [SerializeField] private Transform cannonSx;
    [Tooltip("Add the Object children of the shield.")]
    [SerializeField] private GameObject shield;

    private Transform trans;
    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    private IEnumerator moveCoroutine;

    [Header("Position")]
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 playingPosition;
    [SerializeField] private Vector3 endPosition;

    [Header("Stats")]
    [SerializeField] private int lifePoint;
    [SerializeField] private float speed;

    [Header("CD Timer")]
    [SerializeField] private float shootTimer;
    [SerializeField] private float shieldTimer;
    [SerializeField] private float hitTimer;

    [Header("CD Type")]
    [SerializeField] private bool shooted;
    [SerializeField] private bool IsShieldUP;
    [SerializeField] private bool IsShieldInCD;
    [SerializeField] private bool IsHitted;

    #region UnityMessages
    private void Awake() 
    {
        trans = gameObject.transform;
        gameObject.transform.position = startPosition;
        gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        shooted = false;
        IsShieldUP = false;
        IsShieldInCD = false;
        IsHitted = false;
        shootTimer = 0.5f;
        shieldTimer = 3f;
        hitTimer = 1f;
        startPosition = new Vector3(0f, -1f, 0f);
        playingPosition = new Vector3(0f, 1.5f, 0f);
        endPosition = new Vector3(0f, 20f, 0f);
        speed = DataSO.Speed;
        lifePoint = DataSO.initialLifePoint;
        EventController.RaiseOnLivesChanged(lifePoint);

        spriteRenderer.sprite = DataSO.itemSprite;
    }
    private void OnEnable()
    {

        EventController.SpaceshipSpawn += Spawn;
        EventController.SpaceshipMoveStarted += OnMoveStarted;
        EventController.SpaceshipMoveFinished += OnMoveFinished;

        EventController.SpaceshipShoot += Shoot;
        EventController.SpaceshipShield += Shield;

        EventController.StageComplete += StageComplete;
        EventController.GameOverUI += DisableAnimation;
    }
    private void OnDisable()
    {
        EventController.SpaceshipSpawn -= Spawn;
        EventController.SpaceshipMoveStarted -= OnMoveStarted;
        EventController.SpaceshipMoveFinished -= OnMoveFinished;

        EventController.SpaceshipShoot -= Shoot;
        EventController.SpaceshipShield -= Shield;

        EventController.StageComplete -= StageComplete;
        EventController.GameOverUI -= DisableAnimation;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.GetComponent<Laser>()
            && !IsShieldUP
            && !IsHitted)
        {
            OnTakeDamage();
        }
    }
    #endregion

    #region Movement

    private void OnMoveStarted(InputAction.CallbackContext _context)
    {
        moveCoroutine = OnMove(_context.ReadValue<float>());
        StartCoroutine(moveCoroutine);
    }

    private void OnMoveFinished(InputAction.CallbackContext _context)
    {
        StopCoroutine(moveCoroutine);
        moveCoroutine = null;
    }
    private IEnumerator OnMove(float _input)
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            Vector2 direction = Vector2.right * _input;
            float movementMagnitude = speed * Time.fixedDeltaTime;

            rigidBody.MovePosition(transform.position + (Vector3)direction * movementMagnitude);
        }
    }
    #endregion

    #region Shoot

    private void Shoot(InputAction.CallbackContext context)
    {
        if (!shooted)
        {
            shooted = true;
            GameObject laserShooted1 = Factory.Instance.activateLaser();
            GameObject laserShooted2 = Factory.Instance.activateLaser();
            if (laserShooted1 != null && laserShooted2 != null)
            {
                laserShooted1.SetActive(true);
                laserShooted1.transform.position = cannonSx.transform.position;
                laserShooted2.SetActive(true);
                laserShooted2.transform.position = cannonDx.transform.position;
                StartCoroutine(shootCD());
            }
        }
    }
    private IEnumerator shootCD()
    {
        yield return new WaitForSeconds(shootTimer);
        shooted = false;
    }
    #endregion

    #region Ability
    private void Shield(InputAction.CallbackContext _context)
    {
        if (!IsShieldUP && !IsShieldInCD)
        {
            IsShieldInCD = true;
            IsShieldUP = true;
            shield.SetActive(true);
            StartCoroutine(shieldCD());
        }
    }
    private IEnumerator shieldCD()
    {
        yield return new WaitForSeconds(shieldTimer);
        shield.SetActive(false);
        IsShieldUP = false;
        yield return new WaitForSeconds(shieldTimer*2);
        IsShieldInCD = false;
    }

    #endregion

    #region Animation

    private void DisableAnimation()
    {
        StopAllCoroutines();
    }

    private void Spawn()
    {
        EventController.RaiseOnSpaceshipDisableInput();
        StartCoroutine(SpawnPlayerAnimation());
    }
    private IEnumerator SpawnPlayerAnimation()
    {
        while (transform.position.y <= 1.5f)
        {
            transform.position = Vector2.MoveTowards(transform.position, playingPosition, 3 * Time.deltaTime);
            yield return null;
        }
        EventController.RaiseOnGenerateStage();
    }
    private void StageComplete()
    {
        StartCoroutine(StageCompleteAnimation());
    }
    private IEnumerator StageCompleteAnimation()
    {
        EventController.RaiseOnSpaceshipDisableInput();
        EventController.RaiseOnClearStage();
        yield return new WaitForSeconds(0.5f);
        endPosition = new Vector3(transform.position.x, endPosition.y, 0f);
        while (transform.position.y <= endPosition.y - 1)
        {
            speed = speed + 0.1f;
            transform.position = Vector2.MoveTowards(transform.position, endPosition, speed * Time.deltaTime);
            yield return null;
        }
        EventController.RaiseOnStageCompleteUI();
        Destroy(gameObject);
    }
    #endregion

    #region Interface Method
    public void OnTakeDamage()
    {
        IsHitted = true;
        lifePoint--;
        EventController.RaiseOnLivesChanged(lifePoint);
        if (lifePoint < 0)
        {
            OnKill();
        }
        StartCoroutine(invulnerabilityCD());
    }

    private IEnumerator invulnerabilityCD()
    {
        yield return new WaitForSeconds(hitTimer);
        IsHitted = false;
    }

    public void OnKill()
    {
        DisableAnimation();
        EventController.RaiseOnGameOverUI();
    }
    #endregion
}
