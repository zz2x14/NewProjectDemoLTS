using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSummon : BossController
{
   [Header("召唤物")] 
   [SerializeField] private GameObject summonedPrefab;
   [SerializeField] private int summonedCount;
   [SerializeField] private Transform summonedPoint;

   public int SummonedCount => summonedCount;

   public void Summon()
   {
      PoolManager.Instance.Release(summonedPrefab, summonedPoint.position);
   }

   public override void Attack2()
   {
      Collider2D player = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);

      if (player != null)
      {
         player.GetComponent<ITakenDamage>().TakenDamage(enemyData.bossData.attack2Damage);
      }
   }

   public void EnableDebuff_LimitRoll()
   {
      Collider2D player = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);

      if (player != null)
      {
         player.GetComponent<IPlayerDebuff>().LimitRoll(enemyData.bossData.spareValue);
      }
   }
}
