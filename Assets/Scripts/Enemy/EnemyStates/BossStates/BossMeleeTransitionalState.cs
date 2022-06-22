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
            switch (boss.enemyData.enemyType)
            {
                case EnemyType.TrollLike:
                    if (bossThreeMelee.AttackCycle >= bossThreeMelee.AttackMaxCycle)
                    {
                        boss.AttackCycle = 0;
                        stateMachine.SwitchState(typeof(BossMelee2State));
                    }
                    else
                    {
                        stateMachine.SwitchState(typeof(BossMoveToPlayerState));
                    }
                    break;
                case EnemyType.ToadKingLike:
                    if (boss.AttackCycle >= boss.AttackMaxCycle)
                    {
                        boss.AttackCycle = 0;
                        stateMachine.SwitchState(typeof(BossMelee2State));
                    }
                    else
                    {
                        stateMachine.SwitchState(typeof(BossMoveToPlayerState));
                    }
                    break;
            }
        }
    }
    
}
