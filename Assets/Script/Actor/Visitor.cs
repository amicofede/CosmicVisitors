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
        else if (collision.collider.gameObject.GetComponent<Spaceship>())
        {
            EventController.RaiseOnGameOverUI();
            Destroy(collision.collider.gameObject);
        }
        else if (collision.collider.GetComponent<CircleCollider2D>())
        {
            lifePoint = 0;
            OnTakeDamage();
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
        GameObject ReturnFireShooted1;
        GameObject ReturnFireShooted2;
        switch (DataSO.visitorType)
        {
            case VisitorSO.VisitorType.Fighter:
                ReturnFireShooted1 = Factory.Instance.activateReturnFireFighter();
                ReturnFireShooted2 = Factory.Instance.activateReturnFireFighter();
                SpawnReturnFire(ReturnFireShooted1, ReturnFireShooted2);
                break;
            case VisitorSO.VisitorType.Bomber:
                ReturnFireShooted1 = Factory.Instance.activateReturnFireBomber();
                ReturnFireShooted2 = Factory.Instance.activateReturnFireBomber();
                SpawnReturnFire(ReturnFireShooted1, ReturnFireShooted2);
                break;
        }
    }

    public void SpawnReturnFire(GameObject _ReturnFireShooted1, GameObject _ReturnFireShooted2)
    {
        if (_ReturnFireShooted1 != null && _ReturnFireShooted2 != null)
        {
            _ReturnFireShooted1.SetActive(true);
            _ReturnFireShooted2.SetActive(true);
            _ReturnFireShooted1.transform.position = cannonSx.transform.position;
            _ReturnFireShooted2.transform.position = cannonDx.transform.position;
        }
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
        VisitorController.Instance.OnVisitorKilled(gameObject);
        switch (DataSO.visitorType)
        {
            case VisitorSO.VisitorType.Fighter:
                Factory.Instance.deactiveVisitorFighter(gameObject);
                break;
            case VisitorSO.VisitorType.Bomber:
                Factory.Instance.deactiveVisitorBomber(gameObject);
                break;
        }
        //Destroy(gameObject);
    }
    #endregion
}
