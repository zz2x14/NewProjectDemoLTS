using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyState/EnemyGeneralIdleState",fileName = "EnemyGeneralIdleState")]
public class EnemyGeneralIdleState : EnemyStateBase
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
            stateMachine.SwitchState(typeof(EnemyGeneralIdleState));
        }
    }

    public override void OnPhysicalLogicUpdate()
    {
        base.OnPhysicalLogicUpdate();
    }

    public override void OnExit()
    {
        base.OnExit();
    }
    
}
