using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyState/EnemyAttackIdleState",fileName = "EnemyAttackIdleState")]
public class EnemyAttackIdleState : EnemyStateBase
{
    private float attackTimer;

    public bool canAttack => (Time.time - attackTimer) >= enemy.AttackInterval;
    
    public override void OnEnter()
    {
        base.OnEnter();
        attackTimer = Time.time;
    }

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();

        if (!enemy.FoundPlayer)
        {
            stateMachine.SwitchState(typeof(EnemyHomingState));
        }
        else
        {
            if (enemy.PlayerInAttackRange)
            {
                if (canAttack)
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
        attackTimer = 0;
    }
    
}
