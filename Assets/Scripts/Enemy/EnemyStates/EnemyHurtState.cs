using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyState/EnemyHurtState",fileName = "EnemyHurtState")]
public class EnemyHurtState : EnemyStateBase
{
    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();

        if (isAnimOver)
        {
            switch (enemy.enemyData.enemyType)
            {
                case EnemyType.SlimeLike:
                    stateMachine.SwitchState(typeof(EnemyHurtIdleState));
                    break;
                case EnemyType.GolblinMeleeLike:
                    stateMachine.SwitchState(typeof(EnemyHurtIdleState));
                    break;
                case EnemyType.GoblinRangeLike:
                    stateMachine.SwitchState(typeof(EnemyShootEscapeState));
                    break;
            }
         
        }
    }
    
}
