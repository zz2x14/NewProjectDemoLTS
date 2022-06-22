using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBeetleStateBase : BossStateBase
{
   protected BossBeetle bossBeetle;
   
   public override void InitializeState(EnemyController bossController, EnemyStateMachine bossStateMachine)
   {
      base.InitializeState(bossController, bossStateMachine);
      
      bossBeetle = bossController as BossBeetle;
   }
}
