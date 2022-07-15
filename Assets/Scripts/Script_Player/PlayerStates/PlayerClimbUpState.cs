using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerState/PlayerClimbMoveState",fileName = "PlayerClimbMoveState")]
public class PlayerClimbUpState : PlayerStateBase
{
    [SerializeField] private float climbSpeed;
    [SerializeField] private Vector3 moveDistanceFromStairs;
    
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
            player.SetPosWithStairs(moveDistanceFromStairs);
            playerStateMachine.SwitchState(typeof(PlayerLandState));
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
        
        player.SetRbVelocity(Vector2.zero);
        
        Physics2D.IgnoreLayerCollision(6,9,false);
    }
    
}
