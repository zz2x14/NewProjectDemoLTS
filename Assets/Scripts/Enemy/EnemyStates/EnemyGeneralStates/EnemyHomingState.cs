using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyState/EnemyHomingState",fileName = "EnemyHomingState")]
public class EnemyHomingState : EnemyStateBase
{
    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();

        if (enemy.FoundPlayer)
        {
            switch (enemy.enemyData.enemyType)
            {
                case EnemyType.WaspLike:
                    stateMachine.SwitchState(typeof(EnemyFlyToPointState));
                    break;
                default:
                    stateMachine.SwitchState(typeof(EnemyGeneralChaseState));
                    break;
            }
            
            return;
        }

        if (enemy.CloseToTarget(enemy.OriginalPos,0.1f))
        {
            switch (enemy.enemyData.enemyType)
            {
                case EnemyType.SlimeLike:
                    stateMachine.SwitchState(typeof(EnemyGeneralIdleState));
                    break;
                case EnemyType.GolblinMeleeLike:
                    stateMachine.SwitchState(typeof(EnemyGeneralPatrolState));
                    break;
                case EnemyType.WaspLike:
                    enemy.SetRbVelocity(Vector2.zero);
                    stateMachine.SwitchState(typeof(EnemyGeneralIdleState));
                    break;
            }
        }
    }

    public override void OnPhysicalLogicUpdate()
    {
        base.OnPhysicalLogicUpdate();
        
        enemy.MoveToTargetHorizontal(enemy.MoveSpeed,enemy.OriginalPos);
        enemy.FaceToTarget(enemy.OriginalPos);
    }

}
