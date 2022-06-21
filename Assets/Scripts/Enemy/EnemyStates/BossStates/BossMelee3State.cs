using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyState/BossState/BossMelee3State",fileName = "BossMelee3State")]
public class BossMelee3State : BossStateBase
{
    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();

        if (isAnimOver)
        {
            if (enemy.enemyData.enemyType == EnemyType.TrollLike)
            {
                stateMachine.SwitchState(typeof(BossMeleeTransitionalState));
            }
        }
    }

 
    
}
