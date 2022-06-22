using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyState/BossState/BossBeetleState/BossTakeOffState",fileName = "BossTakeOffState")]
public class BossTakeOffState : BossBeetleStateBase
{
    public override void OnEnter()
    {
        base.OnEnter();

        enemy.FaceToPlayer();
        
        boss.AttackCycle = 0;
        boss.IncreaseAttackMaxCycle(2);

        bossBeetle.ThrowCount = 0;
    }

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();

        if (enemy.CloseToTarget(bossBeetle.SkyTargetPoint, 0.1f))
        {
            enemy.SetRbVelocity(Vector2.zero);
            stateMachine.SwitchState(typeof(BossFlyThrowState));
        }
    }

    public override void OnPhysicalLogicUpdate()
    {
        base.OnPhysicalLogicUpdate();

        if (!enemy.CloseToTarget(bossBeetle.SkyTargetPoint, 0.1f))
        {
            enemy.MoveToTarget(bossBeetle.FlySpeed,bossBeetle.SkyTargetPoint);
        }
        else
        {
            enemy.SetRbVelocity(Vector2.zero);
        }
    }
    
}
