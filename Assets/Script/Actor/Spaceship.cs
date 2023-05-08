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
    private Animator animator;
    private IEnumerator moveCoroutine;



    #region UnityMessages
    private void Awake()
    {
        trans = gameObject.transform;
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        spriteRenderer.sprite = DataSO.itemSprite;
        animator.runtimeAnimatorController = DataSO.moveAnimator;
    }
    private void OnEnable()
    {
        DataSO.moveAction.started += OnMoveStarted;
        DataSO.moveAction.canceled += OnMoveFinished;
        DataSO.shootAction.started += OnShoot;
    }

    private void OnDisable()
    {
        DataSO.moveAction.started -= OnMoveStarted;
        DataSO.moveAction.canceled -= OnMoveFinished;
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
