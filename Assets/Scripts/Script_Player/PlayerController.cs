using System;
using System.Collections;
using System.Collections.Generic;
using MyEventSpace;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : CharacterBase,IPlayerDebuff,ITalk,ICureOverTime
{
    private static PlayerController instance;
    
    private Rigidbody2D rb;
    
    private PlayerInput playerInput;
    private PlayerGroundDetector groundDetector;
    private PlayerHangDetector hangDetector;
    private PlayerOnStairsDetector stairsDetector;
    
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

    [Header("法术")] 
    [SerializeField] private Transform castMagicPoint;
    [SerializeField] private Transform magicEffectiveOnPlayerPos; 
    
    public float MoveSpeed => rb.velocity.x;

    public int JumpCount{get; set;}
    
    public bool IsGrounded => groundDetector.IsOnGround;
    public bool CanHang => hangDetector.CanHang;
    public bool IsFalling => rb.velocity.y < 0f;
    public bool IsInStairs => stairsDetector.IsInStairs;
    public bool IsOnStairs => stairsDetector.IsOnStairs;

    private PlayerHealthBar healthBar;
    
    private Coroutine invincibleCor;
    private Coroutine takeDamageOverTimeCor;
    private WaitForSeconds invincibleCDWFS;
    private bool canHurt = true;
    
    public float CurHealth => playerData.baseData.curHealth;
    public float MaxHealth => playerData.baseData.maxHealth;

    public bool CanRoll { get; set; }
    public bool RollCDOver { get; private set; }
    private Coroutine litmitRollCor;
    private WaitForSeconds rollCDWFS;

    public event Action OnForced = delegate {  };
    public Vector2 ForcedForce { get; set; }
    
    public event Action OnTalk = delegate {  };
    private Canvas talkingCanvas;
    
    private float lastShootTime;
    public bool CanShoot => lastShootTime <= Time.time - shootInterval;

    public bool IsCastMagic { get; set; }
    //public bool IsCastMagicAnimOver { get; set; }
    public Vector3 CastMagicPoint => castMagicPoint.position;
    public Vector3 MagicEffectivePos => magicEffectiveOnPlayerPos.position;
    
    private Amulet amuletMagic;
    public int CurAmueltTimeNum { get; set; }

    private Coroutine cureOverTimeCor;
    
    public bool InLevitation { get; private set; }
    
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

        InLevitation = false;
        CurAmueltTimeNum = 0;
        amuletMagic = GetComponentInChildren<Amulet>(true);
    }

    private void Start()
    {
        playerInput.EnableAllInput();

        talkingCanvas = FindObjectOfType<TalkCenter>().GetComponent<Canvas>();
    }

    private void OnDisable()
    {
        GameManager.Instance.ClearBattleList();
        
        StopAllCoroutines();
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

    #region AboutStatesMethod
    
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
        if (CurAmueltTimeNum > 0 && amuletMagic != null)
        {
            CurAmueltTimeNum = Mathf.Max(CurAmueltTimeNum - 1, 0);
            amuletMagic.UpdateAmuletLayer(CurAmueltTimeNum);
            return;
        }
        
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
    
    public void FillHealth()
    {
        playerData.InitializeHealth();
        healthBar.UpdateHealthBarImmediately(playerData.baseData.curHealth,playerData.baseData.maxHealth);
    }
    
    public void TakenDamageOverTime(float duration,float damage)
    {
        if (takeDamageOverTimeCor != null)
        {
            StopCoroutine(takeDamageOverTimeCor);
        }
        takeDamageOverTimeCor = StartCoroutine(TakenDamageOverTimeCor(duration,damage));
    }
    public IEnumerator TakenDamageOverTimeCor(float duration,float damage)
    {
        float t = 0;
        while (t < duration)
        {
            t += invincibleInterval;
            TakenDamage(damage);
            yield return invincibleCDWFS;
        }
    }

    public override void RecoverHealth(float value)
    {
        playerData.baseData.curHealth = Mathf.Min(playerData.baseData.curHealth + value, playerData.baseData.maxHealth);
        healthBar.UpdateHealthBarImmediately(playerData.baseData.curHealth,playerData.baseData.maxHealth);
    }
    public void StopPlayerDebuff()
    {
        CanRoll = true;

        if (litmitRollCor != null)
        {
            StopCoroutine(litmitRollCor);
        }

        if (takeDamageOverTimeCor != null)
        {
            StopCoroutine(takeDamageOverTimeCor);
        }
    }

    public void StartCureOverTimeCor(float interval, float duration,float cureValue)
    {
        if(playerData.baseData.curHealth == playerData.baseData.maxHealth) return;
        
        if (cureOverTimeCor != null)
        {
            StopCoroutine(cureOverTimeCor);
        }

        cureOverTimeCor = StartCoroutine(CureOverTimeCor(interval,duration,cureValue));
    }
    public IEnumerator CureOverTimeCor(float interval, float duration,float cureValue)
    {
        float t = 0;
        var intervalWFS = new WaitForSeconds(interval);
        
        while (t < duration && playerData.baseData.curHealth < playerData.baseData.maxHealth)
        {
            t += interval;
            playerData.baseData.curHealth =
                Mathf.Min(playerData.baseData.curHealth + cureValue, playerData.baseData.maxHealth);
            
            healthBar.UpdateHealthBar();
            
            yield return intervalWFS;
        }
    }

    public void StartLevitationCor(float duration)
    {
        StartCoroutine(LevitationCor(duration));
    }

    IEnumerator LevitationCor(float duration)
    {
        InLevitation = true;

        yield return new WaitForSeconds(duration);

        InLevitation = false;
    }
}
