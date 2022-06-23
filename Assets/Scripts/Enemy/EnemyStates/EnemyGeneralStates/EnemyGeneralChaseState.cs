using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyState/EnemyGeneralChaseState",fileName = "EnemyGeneralChaseState")]
public class EnemyGeneralChaseState : EnemyGeneralStateBase
{
    [SerializeField] private float waitAttackTime;
    [SerializeField] private string idleAnimName;

    private int idleAnimID;

    private float startTime;
    private bool readyAttack => Time.time - startTime >= waitAttackTime;

    protected override void OnEnable()
    {
        base.OnEnable();

        idleAnimID = Animator.StringToHash(idleAnimName);
    }

    public override void OnEnter()
    {
        base.OnEnter();
        
        startTime = Time.time;
    }

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();
       
        //Sign:追击到玩家后等到短暂时间再开始攻击玩家 - 目的是为了避免连续攻击后玩家被击退出攻击范围马上追击到玩家又立刻攻击(不符合攻击频率)
        //追击到玩家后立即切换到站立动画 - 避免出现仍在移动的视觉效果
        
        if (enemy.CloseToPlayer()) //Sign:enemyGeneral.PlayerInAttackRange && 
        {
            stateMachine.Anim.CrossFade(idleAnimID,0.1f);
            if (readyAttack)
            {
                enemy.SetRbVelocity(Vector2.zero);
                stateMachine.SwitchState(typeof(EnemyAttack1State));
            }
        }
        
        if(enemy.enemyData.enemyType == EnemyType.ToadLike)
            return;
        
        if (!enemy.FoundPlayer )
        {
            stateMachine.SwitchState(typeof(EnemyHomingState));
        }
    }

    public override void OnPhysicalLogicUpdate()
    {
        base.OnPhysicalLogicUpdate();

        if (enemy.CloseToPlayer())
        {
            enemy.SetRbVelocity(Vector2.zero);
        }
        else
        {
            enemy.ChasePlayerHorizontal();
        }
    }

   
}
