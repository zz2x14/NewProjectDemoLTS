using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyState/EnemyGeneralChaseState",fileName = "EnemyGeneralChaseState")]
public class EnemyGeneralChaseState : EnemyStateBase
{
    private float startTime;
    private bool readyAttack => Time.time - startTime >= enemy.AttackInterval - 0.1f;
    
    public override void OnEnter()
    {
        base.OnEnter();
        
        startTime = Time.time;
    }

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();
        
        //Sign:追击到玩家后等到短暂时间再开始攻击玩家 - 目的是为了避免连续攻击后玩家被击退出攻击范围马上追击到玩家又立刻攻击(不符合攻击频率)
        if (readyAttack && enemy.WillTouchPlayer() && enemy.PlayerInAttackRange)
        {
            stateMachine.SwitchState(typeof(EnemyAttack1State));
            return;
        }

        if (!enemy.FoundPlayer)
        {
            stateMachine.SwitchState(typeof(EnemyHomingState));
        }
    }

    public override void OnPhysicalLogicUpdate()
    {
        base.OnPhysicalLogicUpdate();

        if (enemy.WillTouchPlayer())
        {
            enemy.SetRbVelocity(Vector2.zero);//达到既定距离后停止移动
        }
        else
        {
            enemy.ChasePlayer();
        }
       
    }

    public override void OnExit()
    {
        base.OnExit();
    }
    
}
