using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : CharacterBase
{
    private Rigidbody2D rb;
    
    [SerializeField] private EnemyData enemyData;
    
    [Header("移动")] 
    [SerializeField] private float moveSpeed;
    [SerializeField] private float chaseSpeed;

    [Header("玩家检测")] 
    [SerializeField] private float detectorRange;
    [SerializeField] private LayerMask playerLayer;

    [Header("攻击检测")] 
    [SerializeField] private float attackStopDistance;
    [SerializeField] private float attackRange;
    [SerializeField] private Transform attackPoint;

    private Transform playerPos;
    public Vector3 OriginalPos { get; private set; }

    private Vector2 playerDir;

    public bool FoundPlayer => Physics2D.OverlapCircle(transform.position, detectorRange, playerLayer);

    public float MoveSpeed => moveSpeed;
    public float ChaseSpeed => chaseSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        OriginalPos = transform.position;
        
        enemyData.InitializeHealth();
    }

    private void Start()
    {
        playerPos = FindObjectOfType<PlayerController>().transform;
    }

    private void OnDrawGizmosSelected()
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
            player.GetComponent<ITakenDamage>().TakenDamage(enemyData.baseData.AttackDamage);
        }
    }

    public void ChasePlayerOnlyX(float speed)
    {
        transform.localScale = playerPos.transform.position.x < transform.position.x
            ? Vector3.one
            : new Vector3(-1, 1, 1);  
        
        playerDir = (playerPos.position - transform.position).normalized;
      
        SetRbVelocityOnlyX(playerDir * speed);
    }

    public void MoveToDestination(float speed,Vector3 destination)
    {
        Vector2 dir = (destination - transform.position).normalized;
        SetRbVelocity(dir * speed);
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
