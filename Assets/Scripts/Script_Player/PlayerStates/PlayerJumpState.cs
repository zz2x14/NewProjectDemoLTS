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

    private float finalJumpForce;
    
    public override void OnEnter()
    {
        base.OnEnter();

        finalJumpForce = player.InLevitation ? jumpForce * 1.5f : jumpForce ;

        player.JumpCount--;
        
        if (playerStateMachine.LastState.GetType() == typeof(PlayerClimbState) ||
            playerStateMachine.LastState.GetType() == typeof(PlayerClimbUpState))
        {
            
            Physics2D.IgnoreLayerCollision(6,9,true);
            //从楼梯上跳起时忽略和目标地面的碰撞 - 能够正常跳到地面上
            player.SetRbVelocityY(climbJumpForce);
            return;
        }

        player.SetRbVelocityY(playerStateMachine.LastState.GetType() == typeof(PlayerHangState) ? hangJumpForce: finalJumpForce);
    }

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();
        
        if (player.CanShoot && input.IsShootKeyPressed && playerAblity.ShootUnloced)
        {
            playerStateMachine.SwitchState(typeof(PlayerShootState));
        }

        if (input.IsAttackKeyPressed)
        {
            playerStateMachine.SwitchState(typeof(PlayerJumpAttackState));
            return;
        }

        if (player.JumpCount == 1 && input.IsJumpKeyPressed && playerAblity.DoubleJumpUnlocked)
        {
            playerStateMachine.SwitchState(typeof(PlayerDoubleJumpState));
            return;
        }
        
        if (player.IsFalling)
        {
            playerStateMachine.SwitchState(typeof(PlayerFallState));
        }
        
        if (player.CanHang && !player.IsGrounded)
        {
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
