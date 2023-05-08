using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
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

    private int lifePoint;


    #region UnityMessages
    private void Awake()
    {
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
            lifePoint--;
            if (lifePoint <= 0)
            {
                EventController.VisitorKilled();
                animator.runtimeAnimatorController = DataSO.destroyAnimator;
                StartCoroutine(SetActiveVisitor());
                //Destroy(gameObject);
            }
            else if (lifePoint < DataSO.initialLifePoint)
            {
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

    public void Move(Vector2 _direction, float _magnitude)
    {
        rigidBody.MovePosition(trans.position + (Vector3)_direction * _magnitude);
    }
}
