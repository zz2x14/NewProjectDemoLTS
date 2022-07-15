using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyState/EnemyAttackTransitionState",fileName = "EnemyAttackTransitionState")]
public class EnemyAttackTransitionState : EnemyGeneralStateBase
{
    [SerializeField] private float watiTime;
    public bool readyAttack => stateDuration >= watiTime;
  
    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();

        if (enemy.enemyData.enemyType == EnemyType.ToadLike)
        {
            if (readyAttack)
            {
                stateMachine.SwitchState(enemy.PlayerInAttackRange
                    ? typeof(EnemyAttack1State)
                    : typeof(EnemyGeneralChaseState));
            }
            
            return;
        }
        
        if (!enemy.FoundPlayer)
        {
            stateMachine.SwitchState(typeof(EnemyHomingState));
        }
        else
        {
            if (readyAttack)
            {
                if (enemy.PlayerInAttackRange)
                {
                    stateMachine.SwitchState(typeof(EnemyAttack1State));
                }
                else
                {
                    stateMachine.SwitchState(typeof(EnemyGeneralChaseState));
                }
            }
        }
    }
}
