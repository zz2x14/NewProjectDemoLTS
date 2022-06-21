using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyStateBase : EnemyStateBase
{
    protected EnemyFly enemyFly;

    public override void InitializeState(EnemyController enemyController, EnemyStateMachine enemyStateMachine)
    {
        base.InitializeState(enemyController, enemyStateMachine);
        
        enemyFly = enemyController as EnemyFly;
    }
}
