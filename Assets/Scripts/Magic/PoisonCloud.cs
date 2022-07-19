using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonCloud : Magic
{
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Transform damagePoint;
    [SerializeField] private float damageRange;

    [Header("中毒持续时间")] 
    [SerializeField] private float poisoningDuration;
    [SerializeField] private float poisoningInterval;

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
                enemy.GetComponent<IEnemy>().StartOverTimeDamageCor(poisoningInterval,poisoningDuration,damageMagic.Damage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(damagePoint.position,damageRange);
    }
    
}
