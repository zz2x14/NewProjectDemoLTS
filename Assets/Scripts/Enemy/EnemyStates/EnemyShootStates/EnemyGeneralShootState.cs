using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyState/EnemyGeneralShootState",fileName = "EnemyGeneralShootState")]
public class EnemyGeneralShootState : EnemyShootBaseState
{
    public override void OnEnter()
    {
        base.OnEnter();
        
        enemyShoot.FaceToPlayer();
        
        GameManager.Instance.AddIntoBattleList(enemy);
    }

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();

        if (isAnimOver)
        {
            stateMachine.SwitchState(typeof(EnemyShootToIdleState));
        }
    }

    
}
