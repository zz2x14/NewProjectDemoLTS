using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;


public class PlayerController : CharacterBase,IPlayerDebuff
{
    [SerializeField] private PlayerData playerData;
    
    [Header("无敌时间")]
    [SerializeField] private float invincibleInterval;
    
    [Header("碰撞体")]
    [SerializeField] private Collider2D normalColl;
    [SerializeField] private Collider2D hangColl;

    [Header("攻击检测")] 
    [SerializeField] private Transform attackPoint01;
    [SerializeField] private Transform attackPoint02;
    [SerializeField] private Transform attackPoint03;
    [SerializeField] private float attackRange01;
    [SerializeField] private float attackRange02;
    [SerializeField] private LayerMask enemyLayer;

    [Header("射击")] 
    [SerializeField] private float shootInterval;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;

    private float lastShootTime;
    public bool CanShoot => lastShootTime <= Time.time - shootInterval;

    private Rigidbody2D rb;
    
    private PlayerInput playerInput;
    private PlayerGroundDetector groundDetector;
    private PlayerHangDetector hangDetector;
    private PlayerOnStairsDetector stairsDetector;
    
    public float MoveSpeed => rb.velocity.x;

    public int JumpCount{get; set;}
    
    public bool IsGrounded => groundDetector.IsOnGround;
    public bool CanHang => hangDetector.CanHang;
    public bool IsFalling => rb.velocity.y < 0f;

    public bool IsInStairs => stairsDetector.IsInStairs;
    public bool IsOnStairs => stairsDetector.IsOnStairs;

    private Coroutine invincibleCor;
    private bool canHurt = true;

    public bool CanRoll { get; set; }
    private Coroutine litmitRollCor;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        groundDetector = GetComponentInChildren<PlayerGroundDetector>();
        hangDetector = GetComponentInChildren<PlayerHangDetector>();
        stairsDetector = GetComponentInChildren<PlayerOnStairsDetector>();

        lastShootTime = Time.time - shootInterval;
    }

    private void OnEnable()
    {
        playerData.InitializeHealth();

        CanRoll = true;
    }

    private void Start()
    {
        playerInput.EnableGameplayInput();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackPoint01.position,attackRange01);
        
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackPoint02.position,attackRange02);
    }

    #region RbVelocity
    
    public void Move(float speed)
    {
        if (playerInput.MoveXInputX != 0)
        {
            transform.localScale = new Vector3(playerInput.MoveXInputX, 1, 1);
        }
        
        SetRbVelocityX(speed * playerInput.MoveXInputX);
    }
    
    public void SetRbVelocity(Vector2 velocity)
    {
        rb.velocity = velocity;
    }

    public void SetRbVelocityX(float moveX)
    {
        rb.velocity = new Vector2(moveX, rb.velocity.y);
    }

    public void SetRbVelocityY(float velocityY)
    {
        rb.velocity = new Vector2(rb.velocity.x, velocityY);
    }
    

    #endregion

    #region StatesMethod
    
    public void SetGravity(float gravity)
    {
        rb.gravityScale = gravity;
    }
    
    public void SetPosWithStairs(Vector3 pos)
    {
        transform.Translate(pos);
    }
    
    public void ReturnHangDetectorDefault()
    {
       hangDetector.ReturnDefaultDis();
    }

    public void SetHangDetector()
    {
        hangDetector.DisableDis();
    }
    
    public void EnableHangColl()
    {
        normalColl.enabled = false;
        hangColl.enabled = true;
    }
    public void DisableHangColl()
    {
        hangColl.enabled = false;
        normalColl.enabled = true;
    }

    public void CollStartRoll()
    {
        normalColl.isTrigger = false;
        SetGravity(1f);
    }
    public void CollEndRoll()
    {
        normalColl.isTrigger = true;
        SetGravity(0f);
    }

    #endregion

    IEnumerator InvincibleFrameCor()
    {
        canHurt = false;

        yield return new WaitForSeconds(invincibleInterval);

        canHurt = true;
    }
    
    public override void TakenDamage(float value)
    {
        if(playerData.baseData.CurHealth <= 0 || !canHurt) return;

        if (invincibleCor != null)
        {
            StopCoroutine(invincibleCor);
        }
        invincibleCor = StartCoroutine(nameof(InvincibleFrameCor));
        
        base.TakenDamage(value);
        
        playerData.baseData.CurHealth = Mathf.Max(playerData.baseData.CurHealth - value, 0f);
        
        if (playerData.baseData.CurHealth == 0)
        {
            Death();
        }
    }

    public void Attack1()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint01.position, attackRange01, enemyLayer);

        if (hitEnemies.Length > 0)
        {
            foreach (var enemy in hitEnemies)
            {
                enemy.GetComponent<EnemyController>().TakenDamage(playerData.baseData.attackDamage);
            }
        }
    }
    public void Attack2()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint01.position, attackRange01, enemyLayer);

        if (hitEnemies.Length > 0)
        {
            foreach (var enemy in hitEnemies)
            {
                enemy.GetComponent<ITakenDamage>().TakenDamage(playerData.baseData.attackDamage);
            }
        }
    }
    public void Shoot()
    {
        lastShootTime = Time.time;
        
        var playerBullet = 
            PoolManager.Instance.Release(bulletPrefab, shootPoint.position,shootPoint.rotation).GetComponent<PlayerBullet>();
        playerBullet.FlyDir = transform.localScale.x;
        playerBullet.FlySpeed = bulletSpeed;
        playerBullet.Damage = playerData.selfData.shootDamage;
    }

    public void LimitRoll(float limitDuration)
    {
        if (litmitRollCor != null)
        {
            StopCoroutine(litmitRollCor);
        }

        litmitRollCor = StartCoroutine(LimitRollCor(limitDuration));
    }

    IEnumerator LimitRollCor(float duration)
    {
        CanRoll = false;

        yield return new WaitForSeconds(duration);

        CanRoll = true;
    }
}
