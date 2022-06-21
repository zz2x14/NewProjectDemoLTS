using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyState/BossState/BossMoveToPlayerState",fileName = "BossMoveToPlayerState")]
public class BossMoveToPlayerState : BossStateBase
{
    private Vector3 destination;

    public override void OnEnter()
    {
        base.OnEnter();

        destination = enemy.PlayerPos.position;
    }

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();

        if (enemy.CloseToPlayer())
        {
            stateMachine.SwitchState(typeof(BossMelee1State));
        }
    }

    public override void OnPhysicalLogicUpdate()
    {
        base.OnPhysicalLogicUpdate();

        enemy.MoveToTargetHorizontal(boss.MoveSpeed,destination);
        enemy.FaceToTarget(destination);
    }

    public override void OnExit()
    {
        base.OnExit();
        
        destination = Vector3.zero;
    }
}
