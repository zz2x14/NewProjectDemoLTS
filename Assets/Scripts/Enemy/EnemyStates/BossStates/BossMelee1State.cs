using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyState/BossState/BossMelee1State",fileName = "BossMelee1State")]
public class BossMelee1State : BossStateBase
{
    public override void OnEnter()
    {
        base.OnEnter();

        switch (boss.bossData.enemyType)
        {
            case EnemyType.TrollLike:
                bossThreeMelee.AttackCycle += 1;
                break;
        }
        
        boss.SetRbVelocity(Vector2.zero);
    }

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();

        if (isAnimOver)
        {
            stateMachine.SwitchState(typeof(BossMeleeTransitionalState));
        }
    }

}
