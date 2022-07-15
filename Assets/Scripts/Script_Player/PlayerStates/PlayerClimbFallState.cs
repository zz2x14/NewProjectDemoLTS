using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerState/PlayerClimbFallState",fileName = "PlayerClimbFallState")]
public class PlayerClimbFallState : PlayerStateBase
{
    [SerializeField] private float fallForce;
    [SerializeField] private Vector3 climbFallFallDistance;
    
    public override void OnEnter()
    {
        base.OnEnter();

        if (playerStateMachine.LastState.GetType() == typeof(PlayerIdleState))
        {
            player.SetPosWithStairs(climbFallFallDistance);
            //Sign:因使用的是Composite碰撞体，若Tilemap完整一格为单位1的正方形，所以下楼梯的瞬下移距离应略大于1
            //TODO:↑感觉还可以优化...
        }
        
        Physics2D.IgnoreLayerCollision(6,9,true);
    }

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();

        if (input.IsClimbKey)
        {
            playerStateMachine.SwitchState(typeof(PlayerClimbUpState));
        }

        if (input.IsFallKeyReleased)
        {
            playerStateMachine.SwitchState(typeof(PlayerClimbState));
        }

        if (player.IsGrounded)
        {
            playerStateMachine.SwitchState(typeof(PlayerIdleState));
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
