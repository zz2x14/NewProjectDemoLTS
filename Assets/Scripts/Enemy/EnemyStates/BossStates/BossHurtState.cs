using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyState/BossState/BossHurtState",fileName = "BossHurtState")]
public class BossHurtState : BossStateBase
{
    public override void OnEnter()
    {
        base.OnEnter();
        
        boss.DontCollidePlayer();
        
        boss.SetRbVelocity(Vector2.zero);
    }

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();

        if (isAnimOver)
        {
            if (boss.bossData.enemyType == EnemyType.TrollLike)
            {
                stateMachine.SwitchState(typeof(BossMeleeTransitionalState));
            }
        }
       
    }

    public override void OnExit()
    {
        base.OnExit();
        
        boss.RecoverNormalLayer();
    }
    
}
