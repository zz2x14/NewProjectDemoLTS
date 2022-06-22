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

        //Sign:因为敌人向着根据玩家产生的目标点移动，而不是玩家本身，所以给予两个条件能够正确地转换到攻击状态
        
        if (enemy.CloseToPlayer() || enemy.CloseToTarget(destination,0.1f))
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
        
        //Sign：同样符合"有开就有关的"定理，退出状态时目标位置清空，避免再进入状态时目标位置不正确出现偏移等
        destination = Vector3.zero;
    }
}
