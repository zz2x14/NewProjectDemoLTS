using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyState/EnemyFlyAttackState",fileName = "EnemyFlyAttackState")]
public class EnemyFlyAttackState : EnemyStateBase
{
    [SerializeField] private Vector2 attackPointOffset;
    [SerializeField] private float attackStopDis;
    
    public override void OnEnter()
    {
        base.OnEnter();
        
        enemy.DontCollidePlayer();

        enemy.FlyAttackedPlayer = false;
    }

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();

        if (enemy.FlyAttackedPlayer)
        {
            stateMachine.SwitchState(typeof(EnemyFlyAttackTransitionState));
        }
    }

    public override void OnPhysicalLogicUpdate()
    {
        base.OnPhysicalLogicUpdate();

        if (enemy.CloseToDestination(enemy.PlayerPos.position,attackStopDis))
        {
           enemy.SetRbVelocity(Vector2.zero);
           enemy.FlyAttackedPlayer = true;
        }
        else
        {
            enemy.MoveToDestination
                (enemy.ChaseSpeed, new Vector3(enemy.PlayerPos.position.x + attackPointOffset.x  * enemy.transform.localScale.x,
                    enemy.PlayerPos.position.y + attackPointOffset.y));
            
            enemy.FaceToPlayer();
        }
      
    }

    public override void OnExit()
    {
        base.OnExit();
        
        enemy.RecoverNormalLayer();
    }
    
}
