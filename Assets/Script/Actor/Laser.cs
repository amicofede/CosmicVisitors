using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Laser : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private Vector2 direction;

    #region UnityMessage
    private void FixedUpdate()
    {
        transform.position += (Vector3)direction * speed * Time.fixedDeltaTime;
    }
    #endregion

    #region Collision
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Visitor>())
        {
        Destroy(gameObject);
        }
    }
    #endregion
}
