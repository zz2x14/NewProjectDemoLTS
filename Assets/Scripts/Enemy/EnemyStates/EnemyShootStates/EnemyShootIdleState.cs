using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyState/EnemyShootIdleState",fileName = "EnemyShootIdleState")]
public class EnemyShootIdleState : EnemyShootBaseState
{
    public override void OnEnter()
    {
        base.OnEnter();
        
        enemy.SetRbVelocity(Vector2.zero);
    }

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();

        if (enemyShoot.FoundPlayer)
        {
            stateMachine.SwitchState(typeof(EnemyGeneralShootState));
        }
    }

  
}
