using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;

public class Spaceship : MonoBehaviour, IDamageable
{
    [Tooltip("Link the scriptable object of this.")]
    [SerializeField] private SpaceshipSO DataSO;
    [Tooltip("Add an Empty Object children in the location of cannonDx and link to this.")]
    [SerializeField] private Transform cannonDx;
    [Tooltip("Add an Empty Object children in the location of cannonSx and link to this.")]
    [SerializeField] private Transform cannonSx;


    private Transform trans;
    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    private IEnumerator moveCoroutine;

    [SerializeField] private int lifePoint;
    [SerializeField] private float speed;



    #region UnityMessages
    private void Awake()
    {
        trans = gameObject.transform;
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        lifePoint = DataSO.initialLifePoint;
        EventController.RaiseOnLivesChanged(lifePoint);

        spriteRenderer.sprite = DataSO.itemSprite;
    }
    private void OnEnable()
    {
        EventController.SpaceshipAnimationStarted += DataSO.DisableInputs;
        EventController.SpaceshipAnimationFinished += DataSO.EnableInputs;
        DataSO.moveAction.started += OnMoveStarted;
        DataSO.moveAction.canceled += OnMoveFinished;
        DataSO.shootAction.started += OnShoot;
    }

    private void OnDisable()
    {
        EventController.SpaceshipAnimationStarted -= DataSO.DisableInputs;
        EventController.SpaceshipAnimationFinished -= DataSO.EnableInputs;
        DataSO.moveAction.started -= OnMoveStarted;
        DataSO.moveAction.canceled -= OnMoveFinished;
        DataSO.shootAction.started -= OnShoot;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.GetComponent<Laser>())
        {
            if (lifePoint <= 1)
            {
                Time.timeScale = 0;
                //Destroy(gameObject);
            }
            else if (lifePoint <= DataSO.initialLifePoint)
            {
                lifePoint--;
                EventController.RaiseOnLivesChanged(lifePoint);
            }
        }
    }
    #endregion

    #region Movement

    private void OnMoveStarted(InputAction.CallbackContext context)
    {
        moveCoroutine = OnMove(context.ReadValue<float>());
        StartCoroutine(moveCoroutine);
    }

    private void OnMoveFinished(InputAction.CallbackContext context)
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
            float movementMagnitude = DataSO.Speed * Time.fixedDeltaTime;

            rigidBody.MovePosition(transform.position + (Vector3)direction * movementMagnitude);
        }
    }
    #endregion

    #region Shoot
    private void OnShoot(InputAction.CallbackContext context)
    {
        Instantiate(DataSO.LaserPrefab, cannonSx.transform.position, Quaternion.Euler(0f,0f,90f));
        Instantiate(DataSO.LaserPrefab, cannonDx.transform.position, Quaternion.Euler(0f, 0f, 90f));
    }
    #endregion
}
