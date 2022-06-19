using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossThreeMelee : BossController
{
    [Header("玩家前停止距离")] 
    [SerializeField] private float stopDis;

    [Header("玩家检测")] 
    [SerializeField] private Transform detectorPoint;
    [SerializeField] private float detectorRange;

    [Header("伤害判定")] 
    [SerializeField] private Transform melee1Point;
    [SerializeField] private Transform melee1Stage2Point;
    [SerializeField] private Transform melee3Point;
    [SerializeField] private float melee1Range;

    [Header("推动玩家效果(仅使用于有推动玩家的敌人)")] 
    [SerializeField] private Vector2 pushForce;
    [SerializeField] private Vector2 pushForce1;

    [Header("攻击轮回")]
    [SerializeField] private int attackMaxCycle;
  
    // public override bool FoundPlayer => 
    //     Physics2D.OverlapCircle(detectorPoint.position, detectorRange, playerLayer);

    public bool IsCloseToPlayer => CloseToPlayer(stopDis);

    public bool IsPlayerOnGround => PlayerPos.GetComponent<PlayerController>().IsGrounded;

    public int AttackMaxCycle
    {
        get => attackMaxCycle;
        set => attackMaxCycle = value;
    }
    
    public int AttackCycle { get; set; }
    
    protected override void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(detectorPoint.position,- transform.right * transform.localScale.x * stopDis);
        
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(melee1Point.position,melee1Range);
        
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(melee1Stage2Point.position,melee1Range);
        
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(melee3Point.position,melee1Range);
    }

    public void InstantaneousMoveWithOffset(Vector3 targetPos,float offsetX)
    {
        transform.position = new Vector2(targetPos.x + offsetX * transform.localScale.x, transform.position.y);
    }

    public void Melee1()
    {
        Collider2D player = Physics2D.OverlapCircle(melee1Point.position, melee1Range, playerLayer);

        if (player != null)
        {
            player.GetComponent<ITakenDamage>().TakenDamage(bossData.baseData.attackDamage);
        }
    }
    public void Melee1Stage2()
    {
        Collider2D player = Physics2D.OverlapCircle(melee1Stage2Point.position, melee1Range, playerLayer);

        if (player != null)
        {
            player.GetComponent<ITakenDamage>().TakenDamage(bossData.baseData.attackDamage);
            player.GetComponent<Rigidbody2D>().AddForce(pushForce * transform.localScale.x,ForceMode2D.Impulse);
        }
    }
    public void Melee2PopPlayer()
    {
        if (IsPlayerOnGround)
        {
            PlayerPos.GetComponent<Rigidbody2D>().AddForce(pushForce1,ForceMode2D.Impulse);
        }
    }

    public void Melee3()
    {
        Collider2D player = Physics2D.OverlapCircle(melee3Point.position, melee1Range, playerLayer);

        if (player != null)
        {
            player.GetComponent<ITakenDamage>().TakenDamage(bossData.bossSelfData.attack3Damage);
        }
    }
}
