using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyController : CharacterBase
{
    private Rigidbody2D rb;
    
    public EnemyData enemyData;

    [Header("攻击")] 
    [SerializeField] private float attackInterval;
    
    [Header("移动")] 
    [SerializeField] protected float moveSpeed;
    [SerializeField] private float chaseSpeed;

    [Header("玩家检测")] 
    [SerializeField] private float detectorRange;
    [SerializeField] private LayerMask playerLayer;

    [Header("攻击检测")] 
    [SerializeField] private float attackStopDistance;
    [SerializeField] private float attackRange;
    [SerializeField] private Transform attackPoint;

    protected Transform playerPos;
    public Vector3 OriginalPos { get; private set; }

    protected Vector2 playerDir;

    //Sign:属性也是可以复写的
    public virtual bool FoundPlayer => Physics2D.OverlapCircle(transform.position, detectorRange, playerLayer);
    public virtual bool PlayerInAttackRange => Physics2D.OverlapCircle(attackPoint.position, attackRange ,playerLayer);

    public float MoveSpeed => moveSpeed;
    public float ChaseSpeed => chaseSpeed;

    public float AttackInterval => attackInterval;

    private readonly Vector3 flipXScale = new Vector3(-1, 1, 1);

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void OnEnable()
    {
        OriginalPos = transform.position;
        
        enemyData.InitializeHealth();
    }

    protected virtual void Start()
    {
        playerPos = FindObjectOfType<PlayerController>().transform;
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,detectorRange);
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint.position,attackRange);
    }

    public bool ArrivedDestination(Vector3 destination)
    {
        return Vector2.Distance(transform.position, destination) <= 0.1f;
    }
    public bool WillTouchPlayer()
    {
        return Vector2.Distance(transform.position, playerPos.position) <= attackStopDistance;
    }

    public void Attack()
    {
        Collider2D player = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);

        if (player != null)
        {
            player.GetComponent<ITakenDamage>().TakenDamage(enemyData.baseData.attackDamage);
        }
    }

    public void ChasePlayer()
    {
        transform.localScale = playerPos.transform.position.x < transform.position.x
            ? Vector3.one
            : flipXScale;
        
        playerDir = (playerPos.position - transform.position).normalized;
      
        SetRbVelocityOnlyX(playerDir * chaseSpeed);
    }

    public void MoveToDestination(float speed,Vector3 destination)
    {
        transform.localScale = transform.position.x > destination.x ? Vector3.one : flipXScale;
        
        SetRbVelocity((destination - transform.position).normalized * speed);
    }
    
    public void SetRbVelocity(Vector2 velocity)
    {
        rb.velocity = velocity;
    }
    public void SetRbVelocityOnlyX(Vector2 velocity)
    {
        rb.velocity = new Vector2(velocity.x, rb.velocity.y);
    }
    public void SetRbVelocityOnlyY(Vector2 velocity)
    {
        rb.velocity = new Vector2(rb.velocity.x, velocity.y);
    }
    public void SetRbVelocityX(float moveX)
    {
        rb.velocity = new Vector2(moveX, rb.velocity.y);
    }
    public void SetRbVelocityY(float velocityY)
    {
        rb.velocity = new Vector2(rb.velocity.x, velocityY);
    }

    public override void TakenDamage(float value)
    {
        if(enemyData.baseData.CurHealth <= 0) return;
        
        base.TakenDamage(value);

        enemyData.baseData.CurHealth = Mathf.Max(enemyData.baseData.CurHealth - value, 0f);
    }
}
