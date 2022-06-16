using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerState/PlayerRunState",fileName = "PlayerRunState")]
public class PlayerRunState : PlayerStateBase
{
    [SerializeField] private float runSpeed;
    [SerializeField] private float acceleration;

    public override void OnEnter()
    {
        base.OnEnter();

        player.JumpCount = 2;

        curSpeed = 0f;
        //此处没有使用 curSpeed ＝ 刚体速度 是因为 避免从其它有速度的状态进入时发生不同情况 TODO:换个思路解决一下？
    }

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();
        
        if (input.IsAttackKeyPressed)
        {
            playerStateMachine.SwitchState(typeof(PlayerAttackState));
        }
        
        if (input.IsRollKeyPressed)
        {
            playerStateMachine.SwitchState(typeof(PlayerRollState));
        }

        if (!input.IsRunning)
        {
            playerStateMachine.SwitchState(typeof(PlayerIdleState));
        }
        
        if (input.IsJumpKeyPressed)
        {
            playerStateMachine.SwitchState(typeof(PlayerJumpState));
        }

        if (player.IsFalling && !player.IsGrounded)
        {
            playerStateMachine.SwitchState(typeof(PlayerFallState));
        }
        
        
        curSpeed = Mathf.MoveTowards(curSpeed, runSpeed, acceleration * Time.deltaTime);
    }

    public override void OnPhysicalLogicUpdate()
    {
        base.OnPhysicalLogicUpdate();

        player.Move(curSpeed);
    }

    
    
}
