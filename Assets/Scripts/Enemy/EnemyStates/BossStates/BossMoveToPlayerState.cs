using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyState/BossState/BossMoveToPlayerState",fileName = "BossMoveToPlayerState")]
public class BossMoveToPlayerState : BossStateBase
{
    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();

        if (bossThreeMelee.IsCloseToPlayer)
        {
            stateMachine.SwitchState(typeof(BossMelee1State));
        }
    }

    public override void OnPhysicalLogicUpdate()
    {
        base.OnPhysicalLogicUpdate();

        boss.MoveToDestination(boss.MoveSpeed,boss.PlayerPos.position);
    }

    public override void OnExit()
    {
        base.OnExit();
    }
    
}
