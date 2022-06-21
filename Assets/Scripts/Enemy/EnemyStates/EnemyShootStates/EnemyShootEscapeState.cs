using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyState/EnemyShootEscapeState",fileName = "EnemyShootEscapeState")]
public class EnemyShootEscapeState : EnemyShootBaseState
{
    private Vector2 escapePoint;
    
    public override void OnEnter()
    {
        base.OnEnter();
        
        //enemyShoot.DontCollidePlayer();
        escapePoint = enemyShoot.GetRandomEscapaPoint();
    }

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();
        
        enemyShoot.FaceToTarget(escapePoint);

        if (Vector2.Distance(enemyShoot.transform.position, escapePoint) <= 0.1f)
        {
            stateMachine.SwitchState(typeof(EnemyShootIdleState));
        }
    }

    public override void OnPhysicalLogicUpdate()
    {
        base.OnPhysicalLogicUpdate();
        
        enemyShoot.EscapePlayer(escapePoint);
    }

    public override void OnExit()
    {
        base.OnExit();
        
        enemy.FaceToPlayer();
        
       // enemyShoot.RecoverNormalLayer();
    }
    
}
