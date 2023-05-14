using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Visitor : MonoBehaviour, IDamageable
{
    [Tooltip("Link the scriptable object of this.")]
    [SerializeField] private VisitorSO DataSO;
    [Tooltip("Add an Empty Object children in the location of cannonDx and link to this.")]
    [SerializeField] private Transform cannonDx;
    [Tooltip("Add an Empty Object children in the location of cannonSx and link to this.")]
    [SerializeField] private Transform cannonSx;

    private Transform trans;
    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;

    private Vector2 direction;
    private int lifePoint;


    #region UnityMessages
    private void Awake()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        lifePoint = DataSO.initialLifePoint;
        trans = gameObject.transform;
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = DataSO.itemSprite;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
            if (collision.collider.gameObject.GetComponent<Laser>())
            {
                 OnTakeDamage();
            }
            else if (collision.collider.gameObject.GetComponent<ArenaController>())
            {
                EventController.RaiseOnVisitorHitBounds(trans.position.x > 0);
            }
    }
    #endregion

    #region Move

    public void Move(Vector2 _direction, float _magnitude)
    {
        direction = _direction;
        rigidBody.MovePosition(trans.position + (Vector3)direction * _magnitude);
    }
    #endregion

    #region Shoot
    public void OnShoot()
    {
        Instantiate(DataSO.ReturnFirePrefab, cannonSx.transform.position, Quaternion.Euler(0f, 0f, -90f));
        Instantiate(DataSO.ReturnFirePrefab, cannonDx.transform.position, Quaternion.Euler(0f, 0f, -90f));
    }

    #endregion

    #region Interface Method
    public void OnTakeDamage()
    {
        lifePoint--;
        if (lifePoint <= 0)
        {

            OnKill();
        }
    }

    public void OnKill()
    {
        EventController.RaiseOnVisitorKilled();
        gameObject.GetComponentInParent<VisitorController>().OnVisitorKilled(gameObject);
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }
    #endregion
}
