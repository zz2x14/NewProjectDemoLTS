using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using MyEventSpace;

public class EnemyController : CharacterBase,IEnemy //TODO:玩家和敌人的动画冲突问题 以及动画被打断问题
{
    private Rigidbody2D rb;
    
    public EnemyData enemyData;

    [Header("移动")] 
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float chaseSpeed;
    
    [Header("玩家检测")] 
    [SerializeField] protected float detectorRange;
    [SerializeField] protected LayerMask playerLayer;
    
    [FormerlySerializedAs("attackStopDistance")]
    [Header("玩家模型前停留距离")] 
    [SerializeField] protected float stopDistance;
    
    [Header("攻击通用")] 
    [SerializeField] protected float attackRange;
    [SerializeField] protected Transform attackPoint;
    
    //Sign:属性也是可以复写的
    public virtual bool FoundPlayer => Physics2D.OverlapCircle(transform.position, detectorRange, playerLayer);
    public bool PlayerInAttackRange => Physics2D.OverlapCircle(attackPoint.position, attackRange ,playerLayer);

    public Transform PlayerPos { get; private set; }
    public Vector3 OriginalPos { get; private set; }

    public float MoveSpeed => moveSpeed;
    public float ChaseSpeed => chaseSpeed;

    protected readonly Vector3 flipXScale = new Vector3(-1, 1, 1);

    private EnemyStateMachine enemyStateMachine;
    private List<EnemyStateBase> enemyStateStock;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
       
        InitializeEnemy();
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

    private void InitializeEnemy()
    {
        enemyStateMachine = GetComponent<EnemyStateMachine>();
        FillStateStock();
        
        enemyData = Instantiate(enemyData);//和状态是同样的道理，敌人不该共用共享一个敌人数据，而是根据提供的数据克隆出自己的数据
    }
    private void FillStateStock()
    {
        enemyStateStock = new List<EnemyStateBase>(enemyStateMachine.StatesList.Count);
        
        foreach (var myState in enemyStateMachine.StatesList)
        {
            enemyStateStock.Add(myState);
        }

        enemyStateMachine.StatesList.Clear();
        
        foreach (var state in enemyStateStock)
        {
            EnemyStateBase stateCopy = Instantiate(state);
            enemyStateMachine.StatesList.Add(stateCopy);
        }
        
        //Sign:将状态机中状态列表的状态复制提取出来，再克隆覆盖到状态机状态列表，使每个敌人都使用"自己独有的状态"
    }

    public bool CloseToTarget(Vector3 target,float distance)
    {
        return Vector2.Distance(transform.position, target) <= distance;
    }
    
    public virtual bool CloseToPlayer()
    {
       return CloseToTarget(PlayerPos.position, stopDistance);
    }
    
    public virtual void Attack1()
    {
        Collider2D player = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);

        if (player != null)
        {
            player.GetComponent<ITakenDamage>().TakenDamage(enemyData.baseData.attackDamage);
        }
    }
  
    public void FaceToTarget(Vector3 target)
    {
        transform.localScale = target.x < transform.position.x
            ? Vector3.one
            : flipXScale;
    }
    public void FaceToPlayer()
    {
        transform.localScale = PlayerPos.transform.position.x < transform.position.x
            ? Vector3.one
            : flipXScale;
    }
    
    public void ChasePlayerHorizontal()
    {
        FaceToPlayer();
      
        SetRbVelocityOnlyX((PlayerPos.position - transform.position).normalized * chaseSpeed);
    }
    public void MoveToTargetHorizontal(float speed,Vector3 target)
    {
        SetRbVelocityOnlyX((target - transform.position).normalized * speed);
    }
    public void MoveToTarget(float speed, Vector3 target)
    {
        SetRbVelocity((target - transform.position).normalized * speed);
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

    public void SetLocalScale(Vector3 scale)
    {
        transform.localScale = scale;
    }

    public override void TakenDamage(float value)
    {
        if(enemyData.baseData.curHealth <= 0) return;
        
        base.TakenDamage(value);

        enemyData.baseData.curHealth = Mathf.Max(enemyData.baseData.curHealth - value, 0f);
        
        if (enemyData.baseData.curHealth == 0)
        {
            EventManager.Instance.EventHandlerTrigger(EventName.OnEnemyDeath,this);
        }
    }
   
}
