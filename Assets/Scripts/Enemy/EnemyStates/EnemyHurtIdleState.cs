using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyState/EnemyHurtIdleState",fileName = "EnemyHurtIdleState")]
public class EnemyHurtIdleState : EnemyStateBase
{
    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();
        
        if (enemy.FoundPlayer)
        {
            if (enemy.PlayerInAttackRange && enemy.WillTouchPlayer())
            {
                stateMachine.SwitchState(typeof(EnemyAttack1State));
            }
            else
            {
                stateMachine.SwitchState(typeof(EnemyGeneralChaseState));
            }
        }
        else
        {
            stateMachine.SwitchState(typeof(EnemyHomingState));
        }
    }

   
    
}
