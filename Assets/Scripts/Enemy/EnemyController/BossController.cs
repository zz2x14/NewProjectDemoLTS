using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : EnemyController
{
    [Header("攻击轮回")]
    [SerializeField] private int attackMaxCycle;

    [Header("Boss愤怒状态血量阈值")] 
    [SerializeField] private float healthInAngryRate;

    public Vector3 CurPos { get; set; }

    public int AttackMaxCycle => attackMaxCycle;
   
    public int AttackCycle { get; set; }
    
    public int HitTimeNum { get; set; }
    
    public bool BossAngryByHealth => enemyData.baseData.CurHealth <= enemyData.baseData.MaxHealth * healthInAngryRate 
                                     && enemyData.baseData.CurHealth > 0f;
  

    protected override void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(attackPoint.position,attackRange);
    }

    public void IncreaseAttackMaxCycle(int increaseRate)
    {
        attackMaxCycle *= increaseRate;
    }

    public virtual void Attack2()
    {
       
    }
    
}
