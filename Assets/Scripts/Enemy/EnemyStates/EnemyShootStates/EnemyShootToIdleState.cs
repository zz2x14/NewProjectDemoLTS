using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyState/EnemyShootToIdleState",fileName = "EnemyShootToIdleState")]
public class EnemyShootToIdleState : EnemyShootBaseState
{
    private float shootTimer;
    public bool canShoot => (Time.time - shootTimer) >= enemyShoot.ShootInterval;
    
    public override void OnEnter()
    {
        base.OnEnter();
        
        shootTimer = Time.time;
    }

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();

        if (enemyShoot.CloseToPlayer())
        {
            stateMachine.SwitchState(typeof(EnemyShootEscapeState));
            return;
        }

        if (!enemyShoot.FoundPlayer)
        {
            stateMachine.SwitchState(typeof(EnemyShootIdleState));
        }

        if (canShoot)
        {
            stateMachine.SwitchState(typeof(EnemyGeneralShootState));
        }
    }

}
