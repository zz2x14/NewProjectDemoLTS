using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyState/EnemyHomingState",fileName = "EnemyHomingState")]
public class EnemyHomingState : EnemyStateBase
{
    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();

        if (enemy.FoundPlayer)
        {
            stateMachine.SwitchState(typeof(EnemyGeneralChaseState));
        }

        if (enemy.ArrivedDestination(enemy.OriginalPos))
        {
            stateMachine.SwitchState(typeof(EnemyGeneralIdleState));
        }
    }

    public override void OnPhysicalLogicUpdate()
    {
        base.OnPhysicalLogicUpdate();
        
        if (!enemy.ArrivedDestination(enemy.OriginalPos))
        {
            enemy.MoveToDestination(enemy.MoveSpeed,enemy.OriginalPos);
        }
    }

}
