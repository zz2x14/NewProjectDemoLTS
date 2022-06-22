using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyState/BossState/BossBeetleState/BossFlyTransitionState",fileName = "BossFlyTransitionState")]
public class BossFlyTransitionState : BossBeetleStateBase
{
    public override void OnEnter()
    {
        base.OnEnter();
        
        enemy.SetRbVelocity(Vector2.zero);
        enemy.FaceToTarget(bossBeetle.SkyTargetPoint);
    }

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();

        if (isAnimOver)
        {
            if (!bossBeetle.ThrowOver)
            {
                stateMachine.SwitchState(typeof(BossFlyThrowState));
            }
            else
            {
                stateMachine.SwitchState(typeof(BossLandState));
            }
        }
    }
}
