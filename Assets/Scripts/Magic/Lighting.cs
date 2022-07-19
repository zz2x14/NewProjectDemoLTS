using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighting : Magic
{
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Transform damagePoint;
    [SerializeField] private Vector2 damageRange;

    [SerializeField] private float offsetY;
    
    private DamageMagic damageMagic;

    protected override void Awake()
    {
        base.Awake();
        
        damageMagic = magic as DamageMagic;
    }

    private void OnEnable()
    {
        StartCoroutine(nameof(FollowCor));
    }

    IEnumerator FollowCor()
    {
        while (gameObject.activeSelf)
        {
            transform.position = new Vector3(GameManager.Instance.FindOneTargetPos().x,
                GameManager.Instance.FindOneTargetPos().y + offsetY, 0f);
            
            yield return null;
        }
    }

    public void TakeDamageAnimEvent()
    {
        var enemies = Physics2D.OverlapBoxAll(damagePoint.position, damageRange,0f,enemyLayer);

        if (enemies.Length > 0)
        {
            foreach (var enemy in enemies)
            {
                enemy.GetComponent<ITakenDamage>().TakenDamage(damageMagic.Damage);
            }
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(damagePoint.position,damageRange);
    }
    
}
