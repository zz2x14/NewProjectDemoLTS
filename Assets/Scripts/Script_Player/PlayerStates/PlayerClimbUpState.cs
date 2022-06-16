using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerState/PlayerClimbMoveState",fileName = "PlayerClimbMoveState")]
public class PlayerClimbUpState : PlayerStateBase
{
    [SerializeField] private float climbSpeed;
    
    public override void OnEnter()
    {
        base.OnEnter();
        
        player.SetGravity(0f);
        
        Physics2D.IgnoreLayerCollision(6,9,true);
    }

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();
        
        if (!player.IsInStairs)
        {
            player.SetGravity(1f);
            player.SetPosWithStairs(Vector3.up);
            playerStateMachine.SwitchState(typeof(PlayerIdleState));
            return;
        }

        if (input.IsFallKey)
        {
            playerStateMachine.SwitchState(typeof(PlayerClimbFallState));
        }

        if (input.IsJumpKeyPressed)
        {
            player.SetGravity(1f);
            playerStateMachine.SwitchState(typeof(PlayerJumpState));
        }

        if (input.IsClimbKeyReleased)
        {
            playerStateMachine.SwitchState(typeof(PlayerClimbState));
        }
    }

    public override void OnPhysicalLogicUpdate()
    {
        base.OnPhysicalLogicUpdate();
       
        player.SetRbVelocityY(climbSpeed);
    }
    
    public override void OnExit()
    {
        base.OnExit();
        
        Physics2D.IgnoreLayerCollision(6,9,false);
    }
    
}
