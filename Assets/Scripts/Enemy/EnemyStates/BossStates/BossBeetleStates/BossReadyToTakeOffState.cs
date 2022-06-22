using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyState/BossState/BossBeetleState/BossReadyToTakeOffState",fileName = "BossReadyToTakeOffState")]
public class BossReadyToTakeOffState : BossBeetleStateBase
{
    
    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();

        if (enemy.CloseToTarget(enemy.OriginalPos, 0.1f))
        {
            enemy.SetRbVelocity(Vector2.zero);
            stateMachine.SwitchState(typeof(BossTakeOffState));
        }
    }

    public override void OnPhysicalLogicUpdate()
    {
        base.OnPhysicalLogicUpdate();
        
        enemy.MoveToTargetHorizontal(enemy.MoveSpeed,enemy.OriginalPos);
        enemy.FaceToTarget(enemy.OriginalPos);
    }

    
}
