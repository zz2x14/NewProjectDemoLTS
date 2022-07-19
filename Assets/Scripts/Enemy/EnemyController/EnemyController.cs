using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using MyEventSpace;
using UnityEditor.Animations;
using UnityEngine.Events;

public class EnemyController : CharacterBase,IEnemy ,ISpeedDown,ILimitAction//TODO:玩家和敌人的动画冲突问题 以及动画被打断问题
{
    private Rigidbody2D rb;
    private SpriteRenderer sR;
    private ItemDroppedFromEnemy dropTool;
    
    public EnemyData enemyData;

    [Header("移动")] 
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float chaseSpeed;

    [Header("玩家检测")] 
    [SerializeField] protected Transform detectorPoint;
    [SerializeField] protected Vector2 detectorRange;
    [SerializeField] protected LayerMask playerLayer;
    
    [FormerlySerializedAs("attackStopDistance")]
    [Header("玩家模型前停留距离")] 
    [SerializeField] protected float stopDistance;
    
    [Header("攻击通用")] 
    [SerializeField] protected float attackRange;
    [SerializeField] protected Transform attackPoint;

    //Sign:属性也是可以复写的
    public virtual bool FoundPlayer => Physics2D.OverlapBox(detectorPoint.position, detectorRange, 0f,playerLayer);
    public bool PlayerInAttackRange => Physics2D.OverlapCircle(attackPoint.position, attackRange ,playerLayer);

    public Transform PlayerPos { get; private set; }
    public Vector3 OriginalPos { get; private set; }

    public float MoveSpeed => moveSpeed;
    public float ChaseSpeed => chaseSpeed;

    protected readonly Vector3 flipXScale = new Vector3(-1, 1, 1);

    private EnemyStateMachine enemyStateMachine;
    private List<EnemyStateBase> enemyStateStock;
    
    public event UnityAction OnDealth = delegate {  };

    public float OnGroundHeight => transform.position.y;
    
    private Color coldColor;
    private Color poisoningColor;
    private Color paralyticColor;

    private Coroutine overTimeDamageCor;
    
    public Vector2 ThrowedForce { get; set; }

    private Animator anim;
    private RuntimeAnimatorController defaultAnimController;
    public float TransmutationDuration { get; set; }
    public bool inTransmutation;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sR = GetComponentInChildren<SpriteRenderer>();
        dropTool = GetComponent<ItemDroppedFromEnemy>();

        coldColor = new Color(0f, 0.85f, 1f, 1f);
        poisoningColor = new Color(0.7f, 0f, 1f, 1f);
        paralyticColor = new Color(0.3f, 0.3f, 0.3f, 1f);

        InitializeEnemy();

        anim = GetComponentInChildren<Animator>();
        defaultAnimController = anim.runtimeAnimatorController;
    }

    protected virtual void OnEnable()
    {
        OriginalPos = transform.position;
      
        enemyData.InitializeHealth();

        inTransmutation = false;
    }
    
    private void OnDisable()
    {
        DropSth();
    }

    protected virtual void Start()
    {
        PlayerPos = FindObjectOfType<PlayerController>().transform;
    }

    protected virtual void OnDrawGizmos()
    {
        // Gizmos.color = Color.magenta;
        // Gizmos.DrawWireCube(transform.position,detectorRange);
        //
        // Gizmos.color = Color.yellow;
        // Gizmos.DrawWireSphere(attackPoint.position,attackRange);
    }

    private void InitializeEnemy()
    {
        enemyStateMachine = GetComponent<EnemyStateMachine>();
        FillStateStock();
        //和状态是同样的道理，敌人不该共用共享一个敌人数据，而是根据提供的数据克隆出自己的数据
        enemyData = Instantiate(enemyData);
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
        if(inTransmutation) return;
        
        if(enemyData.baseData.curHealth <= 0) return;
        
        base.TakenDamage(value);

        enemyData.baseData.curHealth = Mathf.Max(enemyData.baseData.curHealth - value, 0f);
        
        if (enemyData.baseData.curHealth == 0)
        {
            Death();
        }
    }
    
    public void StartOverTimeDamageCor(float interval, float duration, float damage)
    {
        if (overTimeDamageCor != null)
        {
            StopCoroutine(overTimeDamageCor);
        }

        overTimeDamageCor = StartCoroutine(TakenDamageOverTimeCor(interval, duration, damage));
    }

    public IEnumerator TakenDamageOverTimeCor(float interval, float duration, float damage)
    {
        float t = 0;
        var intervalWFS = new WaitForSeconds(interval);
        
        while (t < duration)
        {
            sR.color = poisoningColor;
            
            t += interval;
            TakenDamage(damage);
            yield return intervalWFS;
        }
        
        sR.color = Color.white;
    }

    private void Death()
    {
        GameManager.Instance.DepartFromBattleList(this);
        OnDealth.Invoke();
        EventManager.Instance.EventHandlerTrigger(EventName.OnEnemyDeath,null);
    }

    public void DropSth()
    {
        dropTool.DropItemAndCoin();
    }

    public void StartSpeedDownCor(float duration)
    {
        StartCoroutine(SpeedDownCor(duration));
    }

    public IEnumerator SpeedDownCor(float duration)
    {
        chaseSpeed /= 2;
        moveSpeed /= 2;
        sR.color = coldColor;

        yield return new WaitForSeconds(duration);

        chaseSpeed *= 2;
        moveSpeed *= 2;
        sR.color = Color.white;
    }

    public void BeThrowed(Vector2 force)
    {
        ThrowedForce = force;
        enemyStateMachine.ToThrowedState();
    }

    public void BeTransmutation(RuntimeAnimatorController transmutationAnimController,float duration)
    {
        inTransmutation = true;
        anim.runtimeAnimatorController = transmutationAnimController;
        enemyStateMachine.ToTransmutationState();
        TransmutationDuration = duration;
    }

    public void DefartFromTransmutation()
    {
        inTransmutation = false;
        anim.runtimeAnimatorController = defaultAnimController;
    }

    public void StartLimitActionCor(float duration)
    {
        StartCoroutine(LimitActionCor(duration));
    }

    public IEnumerator LimitActionCor(float duration)
    {
        sR.color = paralyticColor;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        
        yield return new WaitForSeconds(duration);

        sR.color = Color.white;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
