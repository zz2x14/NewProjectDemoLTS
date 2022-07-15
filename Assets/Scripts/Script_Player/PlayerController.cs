using System;
using System.Collections;
using System.Collections.Generic;
using MyEventSpace;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : CharacterBase,IPlayerDebuff,ITalk,ITakenDamageOverTime
{
    private static PlayerController instance;
    
    [SerializeField] private PlayerData playerData;
    
    [Header("无敌时间")]
    [SerializeField] private float invincibleInterval;
    
    [Header("翻滚CD")]
    [SerializeField] private float rollInterval;
    
    [Header("碰撞体")]
    [SerializeField] private Collider2D normalColl;
    [SerializeField] private Collider2D hangColl;

    [Header("攻击检测")] 
    [SerializeField] private Transform attackPoint01;
    [SerializeField] private Transform attackPoint02;
    [SerializeField] private Transform attackPoint03;
    [SerializeField] private Transform jumpAttackPoint;
    [SerializeField] private float attackRange01;
    [SerializeField] private float attackRange02;
    [SerializeField] private LayerMask targetLayer;

    [Header("射击")] 
    [SerializeField] private float shootInterval;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;

    private Canvas talkingCanvas;

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
    private WaitForSeconds invincibleCDWFS;
    private bool canHurt = true;

    public bool CanRoll { get; set; }
    public bool RollCDOver { get; private set; }
    private Coroutine litmitRollCor;
    private WaitForSeconds rollCDWFS;

    public Vector2 ForcedForce { get; set; }
    public event Action OnForced = delegate {  };
    public event Action OnTalk = delegate {  };

    public float CurHealth => playerData.baseData.curHealth;
    public float MaxHealth => playerData.baseData.maxHealth;

    private PlayerHealthBar healthBar;

    private Coroutine takeDamageOverTimeCor;
    
    private void Awake()
    {
        lastShootTime = Time.time - shootInterval;
        
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        groundDetector = GetComponentInChildren<PlayerGroundDetector>();
        hangDetector = GetComponentInChildren<PlayerHangDetector>();
        stairsDetector = GetComponentInChildren<PlayerOnStairsDetector>();

        healthBar = GetComponentInChildren<PlayerHealthBar>();

        playerData = Instantiate(playerData);
        //TODO:现在是为了方便调试 - 后期取消！！！

        rollCDWFS = new WaitForSeconds(rollInterval);
        invincibleCDWFS = new WaitForSeconds(invincibleInterval);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        playerData.InitializeHealth();

        CanRoll = true;
        RollCDOver = true;
    }

    private void Start()
    {
        playerInput.EnableAllInput();

        talkingCanvas = FindObjectOfType<TalkCenter>().GetComponent<Canvas>();
    }

    private void OnDisable()
    {
        GameManager.Instance.ClearBattleList();
    }

   
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(attackPoint01.position,attackRange01);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint02.position,attackRange02);
        
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(jumpAttackPoint.position,attackRange02);
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint03.position,attackRange02);
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
        //gameObject.layer = 19;
        normalColl.isTrigger = true;
        SetGravity(0f);
    }
    public void CollEndRoll()
    {
        //gameObject.layer = 6;
        normalColl.isTrigger = false;
        SetGravity(1f);
    }

    #endregion

    public void StartRollCDCor()
    {
        StartCoroutine(nameof(RollIntervalCor));
    }
    
    IEnumerator RollIntervalCor()
    {
        RollCDOver = false;

        yield return rollCDWFS;

        RollCDOver = true;
        
        StopCoroutine(nameof(RollIntervalCor));
    }

    IEnumerator InvincibleFrameCor()
    {
        canHurt = false;

        yield return invincibleCDWFS;

        canHurt = true;
    }
    
    public override void TakenDamage(float value)
    {
        if(playerData.baseData.curHealth <= 0 || !canHurt) return;

        if (invincibleCor != null)
        {
            StopCoroutine(invincibleCor);
        }
        invincibleCor = StartCoroutine(nameof(InvincibleFrameCor));
        
        playerData.baseData.curHealth = Mathf.Max(playerData.baseData.curHealth - value, 0f);
        
        if (playerData.baseData.curHealth == 0)
        {
            EventManager.Instance.EventHandlerTrigger(EventName.OnPlayerDeath,this);
        }
        
        base.TakenDamage(value);
    }

    public void Attack1()
    {
        var hits = Physics2D.OverlapCircleAll(attackPoint01.position, attackRange01, targetLayer);

        if (hits.Length > 0)
        {
            foreach (var hit in hits)
            {
                hit.GetComponent<EnemyController>().TakenDamage(playerData.baseData.attackDamage);
                UIManager.Instance.ShowDamageValue(hit.transform.position,playerData.baseData.attackDamage);
            }
        }
    }
    public void Attack2()
    {
        var hitEnemies = Physics2D.OverlapCircleAll(attackPoint02.position, attackRange02, targetLayer);

        if (hitEnemies.Length > 0)
        {
            foreach (var enemy in hitEnemies)
            {
                enemy.GetComponent<ITakenDamage>().TakenDamage(playerData.baseData.attackDamage);
                UIManager.Instance.ShowDamageValue(enemy.transform.position,playerData.baseData.attackDamage);
            }
        }
    }
    public void Attack3()
    {
        var hitEnemies = Physics2D.OverlapCircleAll(attackPoint03.position, attackRange02, targetLayer);

        if (hitEnemies.Length > 0)
        {
            foreach (var enemy in hitEnemies)
            {
                enemy.GetComponent<ITakenDamage>().TakenDamage(playerData.baseData.attackDamage);
                UIManager.Instance.ShowDamageValue(enemy.transform.position,playerData.baseData.attackDamage);
            }
        }
    }
    public void JumpAttack()
    {
        var hitEnemies = Physics2D.OverlapCircleAll(jumpAttackPoint.position, attackRange02, targetLayer);

        if (hitEnemies.Length > 0)
        {
            foreach (var enemy in hitEnemies)
            {
                enemy.GetComponent<ITakenDamage>().TakenDamage(playerData.baseData.attackDamage);
                UIManager.Instance.ShowDamageValue(enemy.transform.position,playerData.baseData.attackDamage);
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

    public void Forced(Vector2 force)
    {
        ForcedForce = force;
        OnForced.Invoke();
    }

    public void LoadPlayerData()
    {
        var baseData = SaveCenter.GetPlayerBaseData();
        var selfData = SaveCenter.GetPlayerSelfData();
        
        playerData.baseData = baseData;
        playerData.selfData = selfData;
    }

    public void SavePlayerData()
    {
        SaveCenter.SavePlayerData(playerData);
    }

    public void GoToTalk()
    {
        talkingCanvas.enabled = true;
        
        playerInput.EnbaleOnlyTalkInput();
        
        OnTalk.Invoke();
    }

    public void TalkOver()
    {
        talkingCanvas.enabled = false;
        
        playerInput.DisableTalkInput();
        
        RemoveHasTalkedContent();
    }
    
    public List<string> GetCurPlayerTalkingContents(int matchingID)
    {
        for (int i = 0; i < playerData.playerTalkDatas.Count; i++)
        {
            if (!playerData.playerTalkDatas[i].isTalked)
            {
                if (playerData.playerTalkDatas[i].talkID == matchingID)
                {
                    playerData.playerTalkDatas[i].isTalked = true;//匹配上后标记为已经触发过的对话，并设置该对话player对应的肖像
                    TalkCenter.Instance.SetPlayerTalkingPortrait(playerData.playerTalkDatas[i].playerTalkingPortrait);
                    
                    return playerData.playerTalkDatas[i].talkList;
                }
            }
        }
        return null;
    }

    public void RemoveHasTalkedContent()
    {
        for (int i = 0; i < playerData.playerTalkDatas.Count; i++)
        {
            if (playerData.playerTalkDatas[i].isTalked)
            {
                playerData.playerTalkDatas.Remove(playerData.playerTalkDatas[i]);
            }
        }
    }

    public void EnableHealthBar()
    {
        healthBar.EnableHealthHUDCanvas();
    }
    public void DisableHealthBar()
    {
        healthBar.DisableHeatlthHUDCanvas();
    }

    public void TakeDamageOverTime(float duration,float damage)
    {
        if (takeDamageOverTimeCor != null)
        {
            StopCoroutine(takeDamageOverTimeCor);
        }
        takeDamageOverTimeCor = StartCoroutine(TakeDamageOverTimeCor(duration,damage));
    }
    public IEnumerator TakeDamageOverTimeCor(float duration,float damage)
    {
        float t = 0;
        while (t < duration)
        {
            t += invincibleInterval;
            TakenDamage(damage);
            yield return invincibleCDWFS;
        }
       
    }

    public void FillHealth()
    {
        playerData.InitializeHealth();
        healthBar.FillHealthBarImmediately();
    }
}
