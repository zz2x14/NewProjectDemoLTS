using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyState/EnemyTransmutationState",fileName = "EnemyTransmutationState")]
public class EnemyTransmutationState : EnemyStateBase
{
    private float timer;
    
    public override void OnEnter()
    {
        base.OnEnter();
        
        timer = 0;
    }

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();

        timer += Time.deltaTime;
        if (timer >= enemy.TransmutationDuration)
        {
            stateMachine.SwitchState(stateMachine.LastState);
        }
        
    }

    public override void OnExit()
    {
        base.OnExit();

        timer = 0;
        
        enemy.DefartFromTransmutation();
    }
    
}
