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
            switch (enemy.enemyData.enemyType)
            {
                case EnemyType.SlimeLike:
                    stateMachine.SwitchState(typeof(EnemyGeneralChaseState));
                    break;
                case EnemyType.WaspLike:
                    stateMachine.SwitchState(typeof(EnemyFlyToPointState));
                    break;
            }
        }
    }
}
