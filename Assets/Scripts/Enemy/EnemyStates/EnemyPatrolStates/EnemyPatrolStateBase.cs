using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolStateBase : EnemyStateBase
{
    protected EnemyPatrol enemyPatrol;
    
    public override void InitializeState(EnemyController enemyController, EnemyStateMachine enemyStateMachine)
    {
        base.InitializeState(enemyController, enemyStateMachine);
        
        enemyPatrol = enemyController as EnemyPatrol;
    }
    
}
