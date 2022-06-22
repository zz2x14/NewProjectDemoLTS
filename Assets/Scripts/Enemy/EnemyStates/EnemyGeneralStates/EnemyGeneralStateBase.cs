using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneralStateBase : EnemyStateBase //TODO:该子级分层是否有必要 需要进一步优化分层？
{
    protected EnemyGeneral enemyGeneral;

    public override void InitializeState(EnemyController enemyController, EnemyStateMachine enemyStateMachine)
    {
        base.InitializeState(enemyController, enemyStateMachine);
        
        enemyGeneral = enemyController as EnemyGeneral;
    }
}
