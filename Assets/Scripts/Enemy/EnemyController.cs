using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemyController : CharacterBase
{
    private Rigidbody2D rb;
    
    public EnemyData enemyData;

    [Header("移动")] 
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float chaseSpeed;

    [Header("玩家检测(远程攻击有所区别)")] 
    [SerializeField] private float detectorRange;
    [SerializeField] protected LayerMask playerLayer;
    
    [Header("攻击间隔")] 
    [SerializeField] private float attackInterval;
    
    [Header("攻击检测(远程攻击有所区别)")] 
    [SerializeField] private float attackStopDistance;
    [SerializeField] protected float attackRange;
    [SerializeField] protected Transform attackPoint;
    
    //Sign:属性也是可以复写的
    public virtual bool FoundPlayer => Physics2D.OverlapCircle(transform.position, detectorRange, playerLayer);
    public virtual bool PlayerInAttackRange => Physics2D.OverlapCircle(attackPoint.position, attackRange ,playerLayer);

    public Transform PlayerPos { get; private set; }
    public Vector3 OriginalPos { get; private set; }
    protected Vector2 playerDir;

    public float MoveSpeed => moveSpeed;
    public float ChaseSpeed => chaseSpeed;

    public float AttackInterval => attackInterval;

    protected readonly Vector3 flipXScale = new Vector3(-1, 1, 1);
    
    private Vector2 randomPoint;
    private float randomPointX;
    private float randomPointY;

    public bool FlyAttackedPlayer { get; set; }

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
        PlayerPos = FindObjectOfType<PlayerController>().transform;
    }
  
    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,detectorRange);
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint.position,attackRange);
    }

    public bool CloseToDestination(Vector3 destination,float distance)
    {
        return Vector2.Distance(transform.position, destination) <= distance;
    }
 
    public bool WillTouchPlayer()
    {
        return Vector2.Distance(transform.position, PlayerPos.position) <= attackStopDistance;
    }
    
    public virtual void Attack()
    {
        Collider2D player = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);

        if (player != null)
        {
            player.GetComponent<ITakenDamage>().TakenDamage(enemyData.baseData.attackDamage);
        }
    }
    public void FlyAttack()
    {
        FlyAttackedPlayer = true;
    }
    
    public void ChasePlayerHorizontal()
    {
        FaceToPlayer();
        
        playerDir = (PlayerPos.position - transform.position).normalized;
      
        SetRbVelocityOnlyX(playerDir * chaseSpeed);
    }

    public void MoveToDestination(float speed,Vector3 destination)
    {
        SetRbVelocity((destination - transform.position).normalized * speed);
    }

    public void FaceToPlayer()
    {
        transform.localScale = PlayerPos.transform.position.x < transform.position.x
            ? Vector3.one
            : flipXScale;
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
        
        if (enemyData.baseData.CurHealth == 0)
        {
            Death();
        }
    }
    
    public void DontCollidePlayer()
    {
        gameObject.layer = 13;
    }
    public void RecoverNormalLayer()
    {
        gameObject.layer = 11;
    }

    public Vector2 GetRandomPointAroundSth(Transform target,Vector2 min,Vector2 max)
    {
        randomPointX = Random.Range(min.x, max.x);
        randomPointY = Random.Range(min.y, max.y);
        
        randomPoint = new Vector2(target.position.x + randomPointX, target.position.y + randomPointY);
      
        return randomPoint;
    }
   
}
