using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyState/BossState/BossRushAttackState",fileName = "BossRushAttackState")]
public class BossRushAttackState : BossStateBase
{
    [SerializeField] private float rushOffsetX;
    
    private Vector2 rushDestination;
    public override void OnEnter()
    {
        base.OnEnter();

        boss.AttackCycle = 0;
        
        rushDestination = new Vector2(enemy.transform.position.x + rushOffsetX * enemy.transform.localScale.x,
            enemy.PlayerPos.position.y);
    }

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();
        
        if (enemy.CloseToTarget(rushDestination,0.1f))
        {
            stateMachine.SwitchState(typeof(BossMoveTransitionalState));
        }
    }

    public override void OnPhysicalLogicUpdate()
    {
        base.OnPhysicalLogicUpdate();
        
        boss.MoveToTargetHorizontal(boss.ChaseSpeed, rushDestination);
        enemy.FaceToTarget(rushDestination);
    }

    public override void OnExit()
    {
        base.OnExit();
        
        rushDestination = Vector2.zero;
    }
    
}
