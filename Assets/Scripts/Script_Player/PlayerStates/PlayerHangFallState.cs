using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerState/PlayerHangFallState",fileName = "PlayerHangFallState")]
public class PlayerHangFallState : PlayerStateBase
{
    [SerializeField] private float fallForce;
    
    public override void OnEnter()
    {
        base.OnEnter();
        
        player.SetGravity(1f);
        player.SetRbVelocityY(fallForce);
    }

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();
        
        if (input.IsJumpKeyPressed)
        {
            playerStateMachine.SwitchState(typeof(PlayerJumpState));
          
        }

        if (player.IsGrounded)
        {
            playerStateMachine.SwitchState(typeof(PlayerLandState));
        }
    }

  
    
}
