using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerState/PlayerHangState",fileName = "PlayerHangState")]
public class PlayerHangState : PlayerStateBase
{
    public override void OnEnter()
    {
        base.OnEnter();

        player.JumpCount = 2;
        
        player.SetGravity(0f);
        player.SetRbVelocityY(0f);
    }

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();

        if (input.IsJumpKeyPressed)
        {
            player.DisableHangColl();
            playerStateMachine.SwitchState(typeof(PlayerJumpState));
        }
        
        if (input.IsFallKeyPressed)
        {
            playerStateMachine.SwitchState(typeof(PlayerHangFallState));
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        
        player.SetHangDetector();
        player.DisableHangColl();
        player.SetGravity(1f);
    }
    
}
