using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : CharacterBase
{
    private Rigidbody2D rb;
    
    public BossData bossData;

    [Header("移动")] 
    [SerializeField] protected float moveSpeed;

    [Header("玩家层级")] 
    [SerializeField] protected LayerMask playerLayer;

    public float MoveSpeed => moveSpeed;
    
    public Transform PlayerPos { get; private set; }
    public Vector3 OriginalPos { get; private set; }
    protected Vector2 playerDir;
    
    protected readonly Vector3 flipXScale = new Vector3(-1, 1, 1);

    public virtual bool FoundPlayer => false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        OriginalPos = transform.position;
        
        bossData.InitializeHealth();
    }
    
    public void Start()
    {
        PlayerPos = FindObjectOfType<PlayerController>().transform;
    }

    protected virtual void OnDrawGizmos()
    {
        
    }

    protected bool CloseToDestination(Vector3 destination) => Vector2.Distance(transform.position, destination) <= 0.1f;
    protected bool CloseToPlayer(float dis) => Vector2.Distance(transform.position, PlayerPos.position) <= dis;
   
    public override void TakenDamage(float value)
    {
        if(bossData.baseData.CurHealth <= 0) return;
        
        base.TakenDamage(value);

        bossData.baseData.CurHealth = Mathf.Max(bossData.baseData.CurHealth - value, 0f);
        
        if (bossData.baseData.CurHealth == 0)
        {
            Death();
        }
        
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
    
    public void DontCollidePlayer()
    {
        gameObject.layer = 13;
    }
    
    public void RecoverNormalLayer()
    {
        gameObject.layer = 11;
    }
}
