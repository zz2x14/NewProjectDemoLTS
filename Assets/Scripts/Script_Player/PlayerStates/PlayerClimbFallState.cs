using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerState/PlayerClimbFallState",fileName = "PlayerClimbFallState")]
public class PlayerClimbFallState : PlayerStateBase
{
    [SerializeField] private float fallForce;
    
    public override void OnEnter()
    {
        base.OnEnter();
        
        Physics2D.IgnoreLayerCollision(6,9,true);
    }

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();

        if (player.IsGrounded)
        {
            playerStateMachine.SwitchState(typeof(PlayerIdleState));
        }
        
        if (input.IsClimbKey)
        {
            playerStateMachine.SwitchState(typeof(PlayerClimbUpState));
        }

        if (input.IsFallKeyReleased)
        {
            playerStateMachine.SwitchState(typeof(PlayerClimbState));
        }

        
    }

    public override void OnPhysicalLogicUpdate()
    {
        base.OnPhysicalLogicUpdate();
        
        player.SetRbVelocityY(fallForce);
    }


    public override void OnExit()
    {
        base.OnExit();
        
        player.SetGravity(1f);
        
        Physics2D.IgnoreLayerCollision(6,9,false);
    }
    
}
