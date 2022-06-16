using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerState/PlayerJumpAttackState",fileName = "PlayerJumpAttackState")]
public class PlayerJumpAttackState : PlayerStateBase
{
    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();
        
        if (input.IsJumpKeyPressed)
        {
            playerStateMachine.SwitchState(typeof(PlayerDoubleJumpState));
            return;
        }

        if (isAnimFinished)
        {
            playerStateMachine.SwitchState(typeof(PlayerFallState));
        }
    }
    
}
