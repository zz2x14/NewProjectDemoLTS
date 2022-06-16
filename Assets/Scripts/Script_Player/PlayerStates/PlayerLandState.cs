using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerState/PlayerLandState",fileName = "PlayerLandState")]
public class PlayerLandState : PlayerStateBase//这是一个承上启下过渡的状态，避免落地后过于僵硬,能够平滑过渡到其它状态,分散状态之间的耦合
{
    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();

        if (input.IsRunning)
        {
            playerStateMachine.SwitchState(typeof(PlayerRunState));
        }
        else if (input.IsJumpKeyPressed)
        {
            playerStateMachine.SwitchState(typeof(PlayerRunState));
        }
        else
        {
            playerStateMachine.SwitchState(typeof(PlayerIdleState));
        }
    }

  
    
}
