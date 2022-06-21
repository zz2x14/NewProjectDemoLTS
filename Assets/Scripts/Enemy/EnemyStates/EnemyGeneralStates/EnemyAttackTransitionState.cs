using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyState/EnemyAttackTransitionState",fileName = "EnemyAttackTransitionState")]
public class EnemyAttackTransitionState : EnemyGeneralStateBase
{
    public bool readyAttack => stateDuration >= stateMachine.Anim.GetCurrentAnimatorStateInfo(0).length;
  
    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();

        if (!enemy.FoundPlayer)
        {
            stateMachine.SwitchState(typeof(EnemyHomingState));
        }
        else
        {
            if (enemy.CloseToPlayer()) //Sign:enemyGeneral.PlayerInAttackRange && 
            {
                if (readyAttack)
                {
                    stateMachine.SwitchState(typeof(EnemyAttack1State));
                }
               
            }
            else
            {
                stateMachine.SwitchState(typeof(EnemyGeneralChaseState));
            }
        }
    }

    public override void OnExit()
    {
        base.OnExit();
     
    }
    
}
