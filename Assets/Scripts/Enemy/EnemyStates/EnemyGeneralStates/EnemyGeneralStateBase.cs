using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneralStateBase : EnemyStateBase
{
    protected EnemyGeneral enemyGeneral;

    public override void InitializeState(EnemyController enemyController, EnemyStateMachine enemyStateMachine)
    {
        base.InitializeState(enemyController, enemyStateMachine);
        
        enemyGeneral = enemyController as EnemyGeneral;
    }
}
