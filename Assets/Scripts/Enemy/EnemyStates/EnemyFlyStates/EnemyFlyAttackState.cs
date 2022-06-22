using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyState/EnemyFlyAttackState",fileName = "EnemyFlyAttackState")]
public class EnemyFlyAttackState : EnemyFlyStateBase
{
    [SerializeField] private Vector2 attackPointOffset;

    private Vector3 attackTarget;
 
    public override void OnEnter()
    {
        base.OnEnter();
        
       // enemy.DontCollidePlayer();

        enemyFly.FlyAttackedPlayer = false;

        attackTarget = new Vector3(enemy.PlayerPos.position.x + attackPointOffset.x * enemy.transform.localScale.x,
            enemy.PlayerPos.position.y + attackPointOffset.y);
    }

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();

        if (enemyFly.FlyAttackedPlayer)
        {
            stateMachine.SwitchState(typeof(EnemyFlyAttackTransitionState));
        }
    }

    public override void OnPhysicalLogicUpdate()
    {
        base.OnPhysicalLogicUpdate();

        if (!enemyFly.FlyAttackedPlayer)
        {
            enemy.MoveToTarget
                (enemy.ChaseSpeed, attackTarget);
            
            enemy.FaceToPlayer();
        }
    }

    public override void OnExit()
    {
        base.OnExit();

        attackTarget = Vector3.zero;
        
        //enemy.RecoverNormalLayer();
    }
}
