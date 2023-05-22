using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

[RequireComponent(typeof(Rigidbody2D))]
public class Laser : MonoBehaviour
{
    [Tooltip("Link the scriptable object of this")]
    [SerializeField] LaserSO laserData;

    private Transform trans;
    private Rigidbody2D rigidBody;
    public SpriteRenderer spriteRenderer;

    #region UnityMessage
    private void Awake()
    {
        trans = gameObject.transform;
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = laserData.itemSprite;
    }

    private void OnEnable()
    {
        EventController.RestartGameUI += DeactivateLaser;
        EventController.ClearStage += DeactivateLaser;
    }

    private void OnDisable()
    {
        EventController.RestartGameUI += DeactivateLaser;
        EventController.ClearStage -= DeactivateLaser;
    }

    private void FixedUpdate()
    {
        Vector2 direction = laserData.Direction.normalized;
        float movementMagnitude = laserData.Speed * Time.fixedDeltaTime;

        rigidBody.MovePosition(trans.position + (Vector3)direction * movementMagnitude);
    }
    #endregion

    #region Collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        laserData.ReturnToFactory(gameObject);
        //Destroy(gameObject);
    }
    #endregion

    public void DeactivateLaser()
    {
        laserData.ReturnToFactory(gameObject);
    }
}
