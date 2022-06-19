using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyState/EnemyFlyToPointState",fileName = "EnemyFlyToPointState")]
public class EnemyFlyToPointState : EnemyStateBase
{
    private Vector2 destination;
    [SerializeField] private Vector2 min;
    [SerializeField] private Vector2 max;
    
    public override void OnEnter()
    {
        base.OnEnter();
        
        enemy.DontCollidePlayer();

        destination = enemy.GetRandomPointAroundSth(enemy.PlayerPos,min,max);
    }

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();
        
        if (enemy.CloseToDestination(destination,0.1f))
        {
            stateMachine.SwitchState(typeof(EnemyFlyTransitionState));
        }
    }

    public override void OnPhysicalLogicUpdate()
    {
        base.OnPhysicalLogicUpdate();
        
        enemy.MoveToDestination(enemy.MoveSpeed,destination);
        
        enemy.FaceToPlayer();
    }

    public override void OnExit()
    {
        base.OnExit();
        
        enemy.RecoverNormalLayer();
    }
}
