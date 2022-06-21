using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyState/BossState/BossMeleeTransitionalState",fileName = "BossMeleeTransitionalState")]
public class BossMeleeTransitionalState : BossStateBase
{
    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();

        if (isAnimOver)
        {
            if (enemy.enemyData.enemyType == EnemyType.TrollLike)
            {
                if (bossThreeMelee.AttackCycle >= bossThreeMelee.AttackMaxCycle)
                {
                    bossThreeMelee.AttackCycle = 0;
                    stateMachine.SwitchState(typeof(BossMelee2State));
                }
                else
                {
                    stateMachine.SwitchState(typeof(BossMoveToPlayerState));
                }
            }
          
        }
    }
    
}
