using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerState/PlayerDoubleJumpState",fileName = "PlayerDoubleJumpState")]
public class PlayerDoubleJumpState : PlayerStateBase
{
    [SerializeField] private float doubleJumpForce;
    [SerializeField] private float jumpMoveSpeed;
    
    public override void OnEnter()
    {
        base.OnEnter();

        player.JumpCount--;
        
        player.SetRbVelocityY(doubleJumpForce);
    }

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();
        
        if (!player.IsGrounded && input.IsAttackKeyPressed)
        {
            playerStateMachine.SwitchState(typeof(PlayerJumpAttackState));
        }

        if (player.CanHang && !player.IsGrounded)
        {
            playerStateMachine.SwitchState(typeof(PlayerHangState));
        }
        
        if (!player.IsGrounded && player.IsFalling)
        {
            playerStateMachine.SwitchState(typeof(PlayerFallState));
        }
        
        if (player.IsInStairs)
        {
            if (input.IsClimbKey)
            {
                playerStateMachine.SwitchState(typeof(PlayerClimbUpState));
                
                return;
            }
            if (input.IsClimbKeyPressed)
            {
                playerStateMachine.SwitchState(typeof(PlayerClimbState));
            }
        }
    }

    public override void OnPhysicalLogicUpdate()
    {
        base.OnPhysicalLogicUpdate();
        
        player.Move(jumpMoveSpeed);
    }

   
}
