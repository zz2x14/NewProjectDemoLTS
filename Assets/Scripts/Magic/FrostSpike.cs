using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostSpike : Magic
{
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Transform damagePoint;
    [SerializeField] private float damageRange;

    [Header("减缓速度持续时间")] 
    [SerializeField] private float speedDownDuration;

    private DamageMagic damageMagic;

    protected override void Awake()
    {
        base.Awake();
        
        damageMagic = magic as DamageMagic;
    }

    
    public void TakeDamageAnimEvent()
    {
        var enemies = Physics2D.OverlapCircleAll(damagePoint.position, damageRange,enemyLayer);

        if (enemies.Length > 0)
        {
            foreach (var enemy in enemies)
            {
                enemy.GetComponent<ITakenDamage>().TakenDamage(damageMagic.Damage);
                enemy.GetComponent<ISpeedDown>().StartSpeedDownCor
                    (enemy.GetComponent<EnemyController>().enemyData.IsBoss ? speedDownDuration / 2f:speedDownDuration);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(damagePoint.position,damageRange);
    }
}
