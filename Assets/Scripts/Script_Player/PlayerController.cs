using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Object = UnityEngine.Object;


public class PlayerController : CharacterBase
{
    [SerializeField] private CharacterData playerData;
    
    [Header("无敌时间")]
    [SerializeField] private float invincibleInterval;
    
    [Header("碰撞体")]
    [SerializeField] private Collider2D normalColl;
    [SerializeField] private Collider2D hangColl;

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
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        groundDetector = GetComponentInChildren<PlayerGroundDetector>();
        hangDetector = GetComponentInChildren<PlayerHangDetector>();
        stairsDetector = GetComponentInChildren<PlayerOnStairsDetector>();
    }

    private void OnEnable()
    {
        playerData.InitializeHealth();
    }

    private void Start()
    {
        playerInput.EnableGameplayInput();
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

    #region MovementStatesMethod
    
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
        gameObject.layer = 0;

        yield return new WaitForSeconds(invincibleInterval);

        gameObject.layer = 6;
    }
    
    public override void TakenDamage(float value)
    {
        base.TakenDamage(value);
        
        if(playerData.baseData.CurHealth <= 0) return;

        StartCoroutine(nameof(InvincibleFrameCor));

        playerData.baseData.CurHealth = Mathf.Max(playerData.baseData.CurHealth - value, 0f);
    }


}
