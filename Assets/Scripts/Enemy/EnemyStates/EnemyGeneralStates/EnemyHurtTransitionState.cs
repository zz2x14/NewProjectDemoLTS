using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyState/EnemyHurtTransitionState",fileName = "EnemyHurtTransitionState")]
public class EnemyHurtTransitionState : EnemyGeneralStateBase
{
    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();

        switch (enemy.enemyData.enemyType)
        {
            case EnemyType.TrollLike:
                stateMachine.SwitchState(typeof(BossMeleeTransitionalState));
                break;
            default:
                if (enemy.FoundPlayer)
                {
                    stateMachine.SwitchState( enemy.CloseToPlayer() ? typeof(EnemyAttack1State) : typeof(EnemyGeneralChaseState));
                }
                else
                {
                    stateMachine.SwitchState(typeof(EnemyHomingState));
                }
                break;
        }
        
       
    }

   
    
}
