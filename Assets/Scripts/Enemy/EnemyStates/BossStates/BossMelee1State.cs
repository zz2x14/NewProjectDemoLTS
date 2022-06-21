using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyState/BossState/BossMelee1State",fileName = "BossMelee1State")]
public class BossMelee1State : BossStateBase
{
    public override void OnEnter()
    {
        base.OnEnter();
        
        boss.SetRbVelocity(Vector2.zero);

        switch (enemy.enemyData.enemyType)
        {
            case EnemyType.TrollLike:
                boss.AttackCycle += 1;
                break;
            case EnemyType.BeetleLike:
                boss.AttackCycle += 1;
                break;
        }
    }

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();

        if (isAnimOver)
        {
            switch (enemy.enemyData.enemyType)
            {
                case EnemyType.TrollLike:
                    stateMachine.SwitchState(typeof(BossMeleeTransitionalState));
                    break;
                case EnemyType.BeetleLike:
                    stateMachine.SwitchState(typeof(BossMoveAfterAttackState));
                    break;
            }
        }
    }

    public override void OnExit()
    {
        base.OnExit();

        if (boss.enemyData.enemyType == EnemyType.BeetleLike)
        {
            boss.CurPos = boss.transform.position;
        }
    }
}
