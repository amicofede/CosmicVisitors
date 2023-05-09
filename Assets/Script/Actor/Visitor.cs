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
    private Animator animator;

    private Vector2 direction;
    private int lifePoint;


    #region UnityMessages
    private void Awake()
    {
        this.GetComponent<BoxCollider2D>().enabled = true;
        lifePoint = DataSO.initialLifePoint;
        trans = gameObject.transform;
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        spriteRenderer.sprite = DataSO.itemSprite;
        animator.runtimeAnimatorController = DataSO.moveAnimator;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
            if (collision.collider.gameObject.GetComponent<Laser>())
            {
                if (lifePoint <= 0)
                {
                    this.GetComponent<BoxCollider2D>().enabled = false;
                    EventController.VisitorKilled();
                    animator.runtimeAnimatorController = DataSO.destroyAnimator;
                    StartCoroutine(SetActiveVisitor());
                    //Destroy(gameObject);
                }
                else if (lifePoint <= DataSO.initialLifePoint)
                {
                    lifePoint--;
                    animator.runtimeAnimatorController = DataSO.damageAnimator;
                }
            }
            else if (collision.collider.gameObject.GetComponent<ArenaController>())
            {
                EventController.VisitorHitBounds(trans.position.x > 0);
            }
    }
    #endregion
    IEnumerator SetActiveVisitor()
    {
        yield return new WaitForSeconds(this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        gameObject.SetActive(false);
    }

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
        animator.runtimeAnimatorController = DataSO.shootAnimator;
        StartCoroutine(SpawnReturnFire());
    }

    private IEnumerator SpawnReturnFire()
    {
        yield return new WaitForSeconds(this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        Instantiate(DataSO.ReturnFirePrefab, cannonSx.transform.position, Quaternion.Euler(0f, 0f, -90f));
        Instantiate(DataSO.ReturnFirePrefab, cannonDx.transform.position, Quaternion.Euler(0f, 0f, -90f));
        if (lifePoint != DataSO.initialLifePoint)
        {
            animator.runtimeAnimatorController = DataSO.damageAnimator;
        }
         animator.runtimeAnimatorController = DataSO.moveAnimator;
    }
    #endregion
}
