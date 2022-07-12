using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapLoop : TrapBase
{
    [SerializeField] private string idleAnimName;
    
    [Header("检测范围")]
    [SerializeField] protected float triggerRange;
    [SerializeField] protected Transform triggerPoint;

    private int idleAnimID;

    protected override void Awake()
    {
        base.Awake();

        idleAnimID = Animator.StringToHash(idleAnimName);
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        anim.Play(animID);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        anim.Play(idleAnimID);
    }

    public override void HitPlayer01()
    {
        var player = Physics2D.OverlapCircle(triggerPoint.position, triggerRange, playerLayer);
        
        if (player !=null)
        {
            player.GetComponent<ITakenDamage>().TakenDamage(trapDamage);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(triggerPoint.position,triggerRange);
    }
    
}
