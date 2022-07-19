using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyState/EnemyThrowedState",fileName = "EnemyThrowedState")]
public class EnemyThrowedState : EnemyStateBase
{
    [SerializeField] private float stateTime;
    private float throwedTimer;
    
    public override void OnEnter()
    {
        base.OnEnter();
        
        throwedTimer = 0;
        enemy.SetRbVelocity(enemy.ThrowedForce);
    }

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();

        throwedTimer += Time.deltaTime;
        if (throwedTimer >= stateTime)
        {
            stateMachine.SwitchState(stateMachine.LastState);
        }
    }

    public override void OnExit()
    {
        base.OnExit();

        throwedTimer = 0;
    }
    
}
