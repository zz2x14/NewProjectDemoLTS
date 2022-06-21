using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateBase : EnemyStateBase
{
    protected BossController boss;
    protected BossThreeMelee bossThreeMelee;
        
    public override void InitializeState(EnemyController bossController,EnemyStateMachine bossStateMachine)
    {
        base.InitializeState(bossController,bossStateMachine);
        
        boss = enemy as BossController;

        switch (enemy.enemyData.enemyType)
        {
            case EnemyType.TrollLike:
                bossThreeMelee = bossController as BossThreeMelee;
                break;
        }
    }
    
    public override  void OnEnter()
    {
        stateMachine.Anim.CrossFade(animNameID,0.1f);
        stateStartTime = Time.time;
    }

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();
    }

    public override void OnPhysicalLogicUpdate()
    {
        base.OnPhysicalLogicUpdate();
    }

    public override  void OnExit()
    {
        base.OnExit();
    }
    
    
}
