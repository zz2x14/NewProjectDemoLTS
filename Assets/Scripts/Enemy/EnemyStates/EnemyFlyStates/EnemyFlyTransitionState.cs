using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "EnemyState/EnemyFlyTransitionState",fileName = "EnemyFlyTransitionState")]
public class EnemyFlyTransitionState : EnemyFlyStateBase
{
    [SerializeField] private float waitToAttackTimeRate;
    private bool readyToAttack => stateDuration >= stateMachine.Anim.GetCurrentAnimatorStateInfo(0).length * waitToAttackTimeRate;
    
    public override void OnEnter()
    {
        base.OnEnter();
        
        enemy.SetRbVelocity(Vector2.zero);
    }
    
    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();
        
        if (!enemy.FoundPlayer)
        {
            stateMachine.SwitchState(typeof(EnemyHomingState));
        }

        if (readyToAttack)
        {
            stateMachine.SwitchState(typeof(EnemyFlyAttackState));
        }
    }
}
