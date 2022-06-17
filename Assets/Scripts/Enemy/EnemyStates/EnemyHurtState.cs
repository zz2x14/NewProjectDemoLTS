using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyState/EnemyHurtState",fileName = "EnemyHurtState")]
public class EnemyHurtState : EnemyStateBase
{
    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();

        if (isAnimOver)
        {
           stateMachine.SwitchState(typeof(EnemyHurtIdleState));
        }
    }

    
}
