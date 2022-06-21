using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BossThreeMelee : BossController
{
    [Header("Boss额外攻击")] 
    [SerializeField] private Transform attack2Point;
    [SerializeField] private Transform attack3Point;
    
    [Header("推动玩家效果(仅使用于有推动玩家的敌人)")] 
    [SerializeField] private Vector2 pushForce;
    [SerializeField] private Vector2 pushForce1;

    public bool IsPlayerOnGround => PlayerPos.GetComponent<PlayerController>().IsGrounded;
    
    protected override void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(attackPoint.position,attackRange);
        
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(attack2Point.position,attackRange);
        
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(attack3Point.position,attackRange);
    }

    public void InstantaneousMoveWithOffset(Vector3 targetPos,float offsetX)
    {
        transform.position = new Vector2(targetPos.x + offsetX * transform.localScale.x, transform.position.y);
    }

    public void Melee1()
    {
        Collider2D player = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);

        if (player != null)
        {
            player.GetComponent<ITakenDamage>().TakenDamage(enemyData.baseData.attackDamage);
        }
    }
    public void Melee2()
    {
        Collider2D player = Physics2D.OverlapCircle(attack2Point.position, attackRange, playerLayer);

        if (player != null)
        {
            player.GetComponent<ITakenDamage>().TakenDamage(enemyData.bossData.attack2Damage);
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
        Collider2D player = Physics2D.OverlapCircle(attack3Point.position, attackRange);
        
        if (player != null)
        {
            player.GetComponent<ITakenDamage>().TakenDamage(enemyData.bossData.attack2Damage);
        }
    }
}
