using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerState/PlayerJumpState",fileName = "PlayerJumpState")]
public class PlayerJumpState : PlayerStateBase
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float hangJumpForce;
    [SerializeField] private float climbJumpForce;
    [SerializeField] private float jumpMoveSpeed;
    
    public override void OnEnter()
    {
        base.OnEnter();

        player.JumpCount--;
        
        if (playerStateMachine.LastState.GetType() == typeof(PlayerClimbState) ||
            playerStateMachine.LastState.GetType() == typeof(PlayerClimbUpState))
        {
            
            Physics2D.IgnoreLayerCollision(6,9,true);
            //从楼梯上跳起时忽略和目标地面的碰撞 - 能够正常跳到地面上
            player.SetRbVelocityY(climbJumpForce);
            return;
        }

        player.SetRbVelocityY(playerStateMachine.LastState.GetType() == typeof(PlayerHangState) ? hangJumpForce: jumpForce);
    }

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();

        if (input.IsAttackKeyPressed)
        {
            playerStateMachine.SwitchState(typeof(PlayerJumpAttackState));
            return;
        }

        if (player.JumpCount == 1 && input.IsJumpKeyPressed)
        {
            playerStateMachine.SwitchState(typeof(PlayerDoubleJumpState));
            return;
        }
        
        if (!player.IsGrounded && player.IsFalling)
        {
            playerStateMachine.SwitchState(typeof(PlayerFallState));
        }
        
        if (player.CanHang && !player.IsGrounded)
        {
            player.EnableHangColl();
            playerStateMachine.SwitchState(typeof(PlayerHangState));
            
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
        
        player.Move(jumpMoveSpeed);//跳跃时可移动
    }

    public override void OnExit()
    {
        base.OnExit();
        
        player.ReturnHangDetectorDefault();//还原悬挂检测 - TODO:更好的方法
    }
    
}
