using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyState/EnemyHurtState",fileName = "EnemyHurtState")]
public class EnemyHurtState : EnemyStateBase
{
    public override void OnEnter()
    {
        base.OnEnter();
        
        //enemy.DontCollidePlayer();
        
        enemy.SetRbVelocity(Vector2.zero);
    }

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();

        if (isAnimOver)
        {
            switch (enemy.enemyData.enemyType)
            {
                case EnemyType.SlimeLike:
                    stateMachine.SwitchState(typeof(EnemyHurtTransitionState));
                    break;
                case EnemyType.GolblinMeleeLike:
                    stateMachine.SwitchState(typeof(EnemyHurtTransitionState));
                    break;
                case EnemyType.GoblinRangeLike:
                    stateMachine.SwitchState(typeof(EnemyShootEscapeState));
                    break;
                case EnemyType.WaspLike:
                    stateMachine.SwitchState(typeof(EnemyFlyToPointState));
                    break;
                case EnemyType.TrollLike:
                    stateMachine.SwitchState(typeof(EnemyHurtTransitionState));
                    break;
            }
         
        }
    }
    
    // public override void OnExit()
    // {
    //     base.OnExit();
    //     
    //     enemy.RecoverNormalLayer();
    // }
}
