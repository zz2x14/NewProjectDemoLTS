using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootBaseState : EnemyStateBase
{
    protected EnemyShoot enemyShoot;
    
    public override void InitializeState(EnemyController enemyController, EnemyStateMachine enemyStateMachine)
    {
        base.InitializeState(enemyController, enemyStateMachine);
        
        enemyShoot = enemyController as EnemyShoot;
    }
    
}
