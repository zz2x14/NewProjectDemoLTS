using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyState/BossState/BossMoveTransitionalState",fileName = "BossMoveTransitionalState")]
public class BossMoveTransitionalState : BossStateBase
{
    private bool readyToAttack => stateDuration >= stateMachine.Anim.GetCurrentAnimatorStateInfo(0).length * 1.25f;
    
    private bool readyToRush => stateDuration >= stateMachine.Anim.GetCurrentAnimatorStateInfo(0).length * 2f;
    
    public override void OnEnter()
    {
        base.OnEnter();
        
        enemy.FaceToTarget(boss.PlayerPos.position);
        enemy.SetRbVelocity(Vector2.zero);
    }

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();

        if (boss.AttackCycle >= boss.AttackMaxCycle)
        {
            if (readyToRush)
            {
                if (!boss.BossAngryByHealth)
                {
                    stateMachine.SwitchState(typeof(BossRushAttackState));
                }
                else
                {
                    stateMachine.SwitchState(typeof(BossReadyToTakeOffState));
                }
            }  
        }
        else
        {
            if (readyToAttack)
            {
                stateMachine.SwitchState(typeof(BossMoveToPlayerState));
            }
        }
    }
    
    
}
