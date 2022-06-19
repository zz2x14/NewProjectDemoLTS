using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyState/EnemyShootIdleState",fileName = "EnemyShootIdleState")]
public class EnemyShootIdleState : EnemyStateBase
{
    private EnemyShoot enemyShoot;
    
    public override void InitializeState(EnemyController enemyController, EnemyStateMachine enemyStateMachine)
    {
        base.InitializeState(enemyController, enemyStateMachine);
        
        enemyShoot = enemyController as EnemyShoot;
    }

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
