using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapFire : TrapLoop,IGiveDamageOverTime
{
    [Header("持续伤害时间")]
    [SerializeField] private float duration;
    
    public float Duration { get; set; }

    protected override void Awake()
    {
        base.Awake();

        Duration = duration;
    }

    public void GiveDamageOverTime(ITakenDamageOverTime target)
    {
        target.TakeDamageOverTime(Duration,trapDamage);
    }

    public override void HitPlayer01()
    {
        var player = Physics2D.OverlapCircle(triggerPoint.position, triggerRange, playerLayer);
        
        if (player !=null)
        {
            GiveDamageOverTime(player.GetComponent<ITakenDamageOverTime>());
        }
    }
}
