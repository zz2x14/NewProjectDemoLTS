using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyState/BossState/BossSummonState",fileName = "BossSummonState")]
public class BossSummonState : BossSummonedStateBase
{
    private bool summonOver =>
        stateDuration >= stateMachine.Anim.GetCurrentAnimatorStateInfo(0).length * bossSummon.SummonedCount;

    public override void OnEnter()
    {
        base.OnEnter();
        
        GameManager.Instance.AddIntoBattleList(enemy);
    }

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();

        if (summonOver)
        {
            stateMachine.SwitchState(typeof(BossMoveToPlayerState));
        }
        
    }
    
}
