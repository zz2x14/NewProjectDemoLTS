using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyState/EnemyFlyAttackState",fileName = "EnemyFlyAttackState")]
public class EnemyFlyAttackState : EnemyFlyStateBase
{
    [SerializeField] private Vector2 attackPointOffset;
 
    public override void OnEnter()
    {
        base.OnEnter();
        
       // enemy.DontCollidePlayer();

        enemyFly.FlyAttackedPlayer = false;
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

        if (enemy.CloseToPlayer())
        {
           enemy.SetRbVelocity(Vector2.zero);
           enemyFly.FlyAttackedPlayer = true;
        }
        else
        {
            enemy.MoveToTarget
                (enemy.ChaseSpeed, new Vector3(enemy.PlayerPos.position.x + attackPointOffset.x  * enemy.transform.localScale.x,
                    enemy.PlayerPos.position.y + attackPointOffset.y));
            
            enemy.FaceToPlayer();
        }
      
    }

    // public override void OnExit()
    // {
    //     base.OnExit();
    //     
    //     enemy.RecoverNormalLayer();
    // }
}
