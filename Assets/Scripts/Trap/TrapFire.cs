using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapFire : TrapLoop,IGivePlayerDamageOverTime
{
    [Header("持续伤害时间")]
    [SerializeField] private float duration;
    
    public void GiveDamageOverTime(IPlayerDebuff target)
    {
        target.TakenDamageOverTime(duration,trapDamage);
    }

    public override void HitPlayer01()
    {
        var player = Physics2D.OverlapCircle(triggerPoint.position, triggerRange, playerLayer);
        
        if (player !=null)
        {
            GiveDamageOverTime(player.GetComponent<IPlayerDebuff>());
        }
    }
}
