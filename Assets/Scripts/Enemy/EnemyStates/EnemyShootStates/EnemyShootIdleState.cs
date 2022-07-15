using System;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyState/EnemyShootIdleState",fileName = "EnemyShootIdleState")]
public class EnemyShootIdleState : EnemyShootBaseState
{
    public override void OnEnter()
    {
        base.OnEnter();
        
        enemy.SetRbVelocity(Vector2.zero);
        
        GameManager.Instance.DepartFromBattleList(enemy);
    }

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();

        if (enemyShoot.FoundPlayer)
        {
            stateMachine.SwitchState(typeof(EnemyGeneralShootState));
        }
    }

  
}
