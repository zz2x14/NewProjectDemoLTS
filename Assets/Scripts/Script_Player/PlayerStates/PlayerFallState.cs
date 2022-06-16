using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "PlayerState/PlayerFallState",fileName = "PlayerFallState")]
public class PlayerFallState : PlayerStateBase
{
    [SerializeField] private float fallForce;
    [SerializeField] private float moveFallForce;
    [SerializeField] private float climbFallForce;
    
    public override void OnEnter()
    {
        base.OnEnter();

        if (player.IsInStairs)
        {
            player.SetRbVelocity(Vector2.down * climbFallForce); 
        }
        else
        {
            if (playerStateMachine.LastState.GetType() == typeof(PlayerRunState) || 
                playerStateMachine.LastState.GetType() == typeof(PlayerIdleState) )
            {
                player.SetRbVelocity(Vector2.down * moveFallForce); 
            }
            else
            {
                player.SetRbVelocityY(fallForce);
            }
        }
    }

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();

        if (!player.IsGrounded && player.CanHang)
        {
            playerStateMachine.SwitchState(typeof(PlayerHangState));
        }

        if (player.JumpCount == 2)
        {
            if (input.IsJumpKeyPressed)
            {
                
                playerStateMachine.SwitchState(typeof(PlayerJumpState));
            }
        }
        if (player.JumpCount == 1)
        {
            if (input.IsJumpKeyPressed)
            {
                playerStateMachine.SwitchState(typeof(PlayerDoubleJumpState));
            }
        }
        
        if (player.IsGrounded)
        {
            playerStateMachine.SwitchState(typeof(PlayerLandState));
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


    public override void OnExit()
    {
        base.OnExit();
        
        Physics2D.IgnoreLayerCollision(6,9,false);
        //从爬楼梯状态到跳跃状态会开启此项碰撞忽略，在下落状态结束时关闭该忽略 - 避免在楼梯上跳跃行为流程未完成时碰撞到目标地面
    }
    
}
