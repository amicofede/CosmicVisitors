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
    private BoxCollider2D boxCollider;
    private Rigidbody2D rigidBody;

    private void Awake()
    {
        trans = gameObject.transform;
        boxCollider = GetComponent<BoxCollider2D>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    #region UnityMessage
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
        if (collision.collider.gameObject.GetComponent<Visitor>())
        {
            Destroy(gameObject);
        }
        else if (collision.collider.gameObject.GetComponent<ArenaController>())
        {
            Destroy(gameObject);
        }
    }
    #endregion
}
