using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerState/PlayerIdleState",fileName = "PlayerIdleState")]
public class PlayerIdleState : PlayerStateBase
{
    [SerializeField] private float deceleration;
    
    public override void OnEnter()
    {
        base.OnEnter();

        player.JumpCount = 2;

        curSpeed = player.MoveSpeed;
    }

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();
        
        if (input.IsAttackKeyPressed)
        {
            playerStateMachine.SwitchState(typeof(PlayerAttackState));
        }
        
        if (input.IsRunning)//并没有同时加上判断毕竟按下按键的情况 - 否则会出现其它状态无法正常转换到跑步状态
        {
            playerStateMachine.SwitchState(typeof(PlayerRunState));
        }

        if (input.IsRollKeyPressed)
        {
            playerStateMachine.SwitchState(typeof(PlayerRollState));
        }

        if (player.JumpCount > 0 && input.IsJumpKeyPressed)
        {
            playerStateMachine.SwitchState(typeof(PlayerJumpState));
        }
        
        if (player.IsFalling && !player.IsGrounded)
        {
            playerStateMachine.SwitchState(typeof(PlayerFallState));
        }

        if (player.IsOnStairs)
        {
            if (input.IsFallKey)
            {
                player.SetPosWithStairs(Vector3.down);
                playerStateMachine.SwitchState(typeof(PlayerClimbFallState));
                return;
            }
            if (input.IsFallKeyPressed)
            {
                player.SetPosWithStairs(Vector3.down);
                playerStateMachine.SwitchState(typeof(PlayerClimbState));
            }
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

        curSpeed = Mathf.MoveTowards(curSpeed, 0f, deceleration * Time.deltaTime);
    }

    public override void OnPhysicalLogicUpdate()
    {
        base.OnPhysicalLogicUpdate();

        player.SetRbVelocityX(curSpeed);
    }

}
