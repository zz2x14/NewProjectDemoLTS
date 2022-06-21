using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyState/EnemyAttack1State",fileName = "EnemyAttack1State")]
public class EnemyAttack1State : EnemyGeneralStateBase
{
    private float attackStartTime;
    
    public override void OnEnter()
    {
        base.OnEnter();
        
        enemy.SetRbVelocity(new Vector2(0f,0f));
    }

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();

        if (isAnimOver)
        {
            stateMachine.SwitchState(typeof(EnemyAttackTransitionState));
        }
    }

    
}
