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
        
        enemy.SetRbVelocity(Vector2.zero);
        
        GameManager.Instance.AddIntoBattleList(enemy);
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
