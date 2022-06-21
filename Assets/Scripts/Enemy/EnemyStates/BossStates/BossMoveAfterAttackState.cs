using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyState/BossState/BossMoveAfterAttackState",fileName = "BossMoveAfterAttackState")]
public class BossMoveAfterAttackState : BossStateBase
{
    [SerializeField] private float moveOffsetX;

    private Vector2 moveDestination;
    
    public override void OnEnter()
    {
        base.OnEnter();

        //Sign:正确逻辑：移动目标不根据玩家的位置向前移动，而是根据boss攻击完成时boss的位置
        moveDestination = new Vector2(boss.CurPos.x + moveOffsetX * enemy.transform.localScale.x,
         boss.transform.position.y);
    }

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();

        if (enemy.CloseToTarget(moveDestination,0.1f))
        {
            stateMachine.SwitchState(typeof(BossMoveTransitionalState));
        }
    }

    public override void OnPhysicalLogicUpdate()
    {
        base.OnPhysicalLogicUpdate();

        enemy.MoveToTargetHorizontal(boss.MoveSpeed, moveDestination);
        enemy.FaceToTarget(moveDestination);
    }
 
}
