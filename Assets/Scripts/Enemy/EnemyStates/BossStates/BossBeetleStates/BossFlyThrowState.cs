using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyState/BossState/BossBeetleState/BossThrowFlyTState",fileName = "BossFlyState")]
public class BossFlyThrowState : BossBeetleStateBase
{
    public override void OnEnter()
    {
        base.OnEnter();
        
        bossBeetle.FlyIndex++;
        if (bossBeetle.FlyIndex > bossBeetle.PointsCount - 1)
        {
            bossBeetle.FlyIndex = 0;
        }
    }

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();

        if (bossBeetle.CloseToFlyPoint)
        {
            stateMachine.SwitchState(typeof(BossFlyTransitionState));
        }
    }

    public override void OnPhysicalLogicUpdate()
    {
        base.OnPhysicalLogicUpdate();

        if (!bossBeetle.CloseToFlyPoint)
        {
            bossBeetle.FlyToNextPoint();
        }
    }
}
