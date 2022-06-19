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

        if (enemy.CloseToDestination(enemy.OriginalPos,0.1f))
        {
            switch (enemy.enemyData.enemyType)
            {
                case EnemyType.SlimeLike:
                    stateMachine.SwitchState(typeof(EnemyGeneralIdleState));
                    break;
                case EnemyType.GolblinMeleeLike:
                    stateMachine.SwitchState(typeof(EnemyGeneralPatrolState));
                    break;
            }
        }
    }

    public override void OnPhysicalLogicUpdate()
    {
        base.OnPhysicalLogicUpdate();
        
        if (!enemy.CloseToDestination(enemy.OriginalPos,0.1f))
        {
            enemy.MoveToDestination(enemy.MoveSpeed,enemy.OriginalPos);
            enemy.FaceToPlayer();
        }
    }

}
