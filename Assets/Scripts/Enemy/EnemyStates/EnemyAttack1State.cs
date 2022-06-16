using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyState/EnemyAttack1State",fileName = "EnemyAttack1State")]
public class EnemyAttack1State : EnemyStateBase
{
    public override void OnEnter()
    {
        base.OnEnter();
        
        enemy.SetRbVelocity(new Vector2(0f,0f));
    }

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();

        if (!enemy.WillTouchPlayer())
        {
            if (enemy.FoundPlayer)
            {
                stateMachine.SwitchState(typeof(EnemyGeneralChaseState));
            }
            else
            {
                stateMachine.SwitchState(typeof(EnemyHomingState));
            }
        }
    }

    public override void OnPhysicalLogicUpdate()
    {
        base.OnPhysicalLogicUpdate();
    }

    public override void OnExit()
    {
        base.OnExit();
    }
    
}
