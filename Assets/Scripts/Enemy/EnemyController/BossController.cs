using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : EnemyController
{
    [Header("攻击轮回")]
    [SerializeField] private int attackMaxCycle;

    public Vector3 CurPos { get; set; }
    
    public int AttackMaxCycle
    {
        get => attackMaxCycle;
        set => attackMaxCycle = value;
    }
    public int AttackCycle { get; set; }

    protected override void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(attackPoint.position,attackRange);
    }
   
}
