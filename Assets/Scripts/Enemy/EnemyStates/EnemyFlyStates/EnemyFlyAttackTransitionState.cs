using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyState/EnemyFlyAttackTransitionState",fileName = "EnemyFlyAttackTransitionState")]
public class EnemyFlyAttackTransitionState : EnemyStateBase
{
    [SerializeField] private float waitToMoveTimeRate;
    
    private bool readyToMove=> stateDuration >= stateMachine.Anim.GetCurrentAnimatorStateInfo(0).length * waitToMoveTimeRate;
    
    public override void OnEnter()
    {
        base.OnEnter();
        
        enemy.DontCollidePlayer();

        
        enemy.SetRbVelocity(Vector2.zero);
    }

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();

        if (readyToMove)
        {
            stateMachine.SwitchState(typeof(EnemyFlyToPointState));
        }
    }
    
    public override void OnExit()
    {
        base.OnExit();
        
        enemy.RecoverNormalLayer();
    }
}
