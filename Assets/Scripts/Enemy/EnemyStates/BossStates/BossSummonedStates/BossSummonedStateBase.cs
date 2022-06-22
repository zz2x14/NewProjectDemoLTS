using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSummonedStateBase : BossStateBase
{
    protected BossSummon bossSummon;

    public override void InitializeState(EnemyController bossController, EnemyStateMachine bossStateMachine)
    {
        base.InitializeState(bossController, bossStateMachine);

        bossSummon = bossController as BossSummon;
    }
}
