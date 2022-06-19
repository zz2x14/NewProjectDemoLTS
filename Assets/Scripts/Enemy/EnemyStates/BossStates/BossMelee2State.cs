using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyState/BossState/BossMelee2State",fileName = "BossMelee2State")]
public class BossMelee2State : BossStateBase
{
    [SerializeField] private float trollMoveOffestX;
    
    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();

        if (isAnimOver)
        {
            if (boss.bossData.enemyType == EnemyType.TrollLike)
            {
                if (!bossThreeMelee.IsPlayerOnGround)
                {
                    bossThreeMelee.InstantaneousMoveWithOffset(boss.PlayerPos.position,trollMoveOffestX);
                    stateMachine.SwitchState(typeof(BossMelee3State));
                }
            }
          
        }
    }

  
    
}
