using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyState/EnemyGeneralChaseState",fileName = "EnemyGeneralChaseState")]
public class EnemyGeneralChaseState : EnemyStateBase
{
    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();
        
        if (enemy.WillTouchPlayer())
        {
            stateMachine.SwitchState(typeof(EnemyAttack1State));
            return;
        }

        if (!enemy.FoundPlayer)
        {
            stateMachine.SwitchState(typeof(EnemyHomingState));
        }
    }

    public override void OnPhysicalLogicUpdate()
    {
        base.OnPhysicalLogicUpdate();
        
        enemy.ChasePlayerOnlyX(enemy.ChaseSpeed);
    }

    public override void OnExit()
    {
        base.OnExit();
    }
    
}
