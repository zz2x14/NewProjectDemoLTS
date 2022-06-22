using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyState/BossState/BossMelee2State",fileName = "BossMelee2State")]
public class BossMelee2State : BossStateBase
{
    [SerializeField] private float trollMoveOffestX;

    public override void OnEnter()
    {
        base.OnEnter();
        
        boss.FaceToPlayer();
    }

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();

        if (isAnimOver)
        {
            switch (boss.enemyData.enemyType)
            {
                case EnemyType.TrollLike:
                    bossThreeMelee.InstantaneousMoveWithOffset(boss.PlayerPos.position,trollMoveOffestX);
                    stateMachine.SwitchState(typeof(BossMelee3State));
                    break;
                case EnemyType.ToadKingLike:
                    stateMachine.SwitchState(typeof(BossMoveToPlayerState));
                    break;
            }
          
        }
    }

  
    
}
