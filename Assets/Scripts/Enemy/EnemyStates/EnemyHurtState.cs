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
                case EnemyType.ToadLike:
                    stateMachine.SwitchState(typeof(EnemyAttackTransitionState));
                    break;
                case EnemyType.BeetleLike:
                    //Sign 甲虫Boss飞行状态中回到之前的飞行状态而不是通常的状态
                    //TODO: 是否耦合过高 或者说过于繁杂代码逻辑 优化？
                    if (stateMachine.LastState.GetType() == typeof(BossFlyThrowState) 
                        ||stateMachine.LastState.GetType() == typeof(BossFlyTransitionState) )
                    {
                        stateMachine.SwitchState(stateMachine.LastState.GetType());
                    }
                    else
                    {
                        stateMachine.SwitchState(typeof(BossMoveAfterAttackState));
                    }
                    
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
