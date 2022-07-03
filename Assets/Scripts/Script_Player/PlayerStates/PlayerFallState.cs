using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "PlayerState/PlayerFallState",fileName = "PlayerFallState")]
public class PlayerFallState : PlayerStateBase
{
    [SerializeField] private AnimationCurve fallCurve;
    [SerializeField] private AnimationCurve moveFallCurve;
    [SerializeField] private AnimationCurve climbFallCurve;
    
    public override void OnEnter()
    {
        base.OnEnter();
        
        player.ReturnHangDetectorDefault();
    }

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();

        if (!player.IsGrounded && player.CanHang)
        {
            playerStateMachine.SwitchState(typeof(PlayerHangState));
        }

        if (input.IsJumpKeyPressed)
        {
            if (player.JumpCount == 2)
            {
                //Sign:使用return避免按键判断时间过短平地落下等跳跃会进入到二段跳状态
                playerStateMachine.SwitchState(typeof(PlayerJumpState));
                return;
            }
            if (player.JumpCount == 1 && playerAblity.DoubleJumpUnlocked)
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

    public override void OnPhysicalLogicUpdate()
    {
        base.OnPhysicalLogicUpdate();

        if (player.IsInStairs)
        {
            player.SetRbVelocityY(climbFallCurve.Evaluate(stateDuration));
        }
        if (playerStateMachine.LastState.GetType() == typeof(PlayerRunState) || 
                playerStateMachine.LastState.GetType() == typeof(PlayerIdleState) )
        {
            player.SetRbVelocityY(moveFallCurve.Evaluate(stateDuration));
        }
        else
        {
            player.SetRbVelocityY(fallCurve.Evaluate(stateDuration));
        }
            
    }


    public override void OnExit()
    {
        base.OnExit();
        
        Physics2D.IgnoreLayerCollision(6,9,false);
        //从爬楼梯状态到跳跃状态会开启此项碰撞忽略，在下落状态结束时关闭该忽略 - 避免在楼梯上跳跃行为流程未完成时碰撞到目标地面
    }
    
}
