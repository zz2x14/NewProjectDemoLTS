using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerState/PlayerClimbState",fileName = "PlayerClimbState")]
public class PlayerClimbState : PlayerStateBase
{
    public override void OnEnter()
    {
        base.OnEnter();
        
        player.SetGravity(0f);
        player.SetRbVelocity(new Vector2(0,0));
        
        Physics2D.IgnoreLayerCollision(6,9,true);
    }

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();
        
        if (input.IsJumpKeyPressed)
        {
            player.SetGravity(1f);
            playerStateMachine.SwitchState(typeof(PlayerJumpState));
        }
        
        if (input.IsClimbKey)
        {
            playerStateMachine.SwitchState(typeof(PlayerClimbUpState));
        }
        
        if (input.IsFallKey)
        {
            playerStateMachine.SwitchState(typeof(PlayerClimbFallState));
        }
        
    }

    public override void OnExit()
    {
        base.OnExit();
        
        Physics2D.IgnoreLayerCollision(6,9,false);
    }
}
