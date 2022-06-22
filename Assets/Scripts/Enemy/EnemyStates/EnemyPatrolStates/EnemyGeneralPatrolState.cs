using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "EnemyState/EnemyGeneralPatrolState",fileName = "EnemyGeneralPatrolState")]
public class EnemyGeneralPatrolState : EnemyPatrolTranstionState
{
    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();
        
        if (enemy.FoundPlayer)
        {
            stateMachine.SwitchState(typeof(EnemyGeneralChaseState));
        }
        
        if (enemyPatrol.ArrivedPatrolPoint)
        {
            stateMachine.SwitchState(typeof(EnemyPatrolTranstionState));
        }
    }

    public override void OnPhysicalLogicUpdate()
    {
        base.OnPhysicalLogicUpdate();
        
        enemyPatrol.MoveToNextPoint();
    }

    public override void OnExit()
    {
        base.OnExit();

        enemyPatrol.PatrolIndex++;
        if (enemyPatrol.PatrolIndex >= enemyPatrol.PointsCount)
            enemyPatrol.PatrolIndex = 0;
    }
}

