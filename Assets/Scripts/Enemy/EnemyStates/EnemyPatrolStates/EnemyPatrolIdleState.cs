using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyState/EnemyPatrolIdleState",fileName = "EnemyPatrolIdleState")]
public class EnemyPatrolIdleState : EnemyPatrolStateBase
{
    [SerializeField] private float waitTime;
    private float startTime;
    private bool idleOver => Time.time - startTime >= waitTime;
    
    public override void OnEnter()
    {
        base.OnEnter();
        
        enemy.SetRbVelocity(Vector2.zero);
        startTime = Time.time;
    }

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();
        
        if (enemy.FoundPlayer)
        {
            stateMachine.SwitchState(typeof(EnemyGeneralChaseState));
        }

        if (idleOver)
        {
            stateMachine.SwitchState(typeof(EnemyGeneralPatrolState));
        }
    }

}
