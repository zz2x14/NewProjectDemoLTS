using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerState/PlayerForcedState",fileName = "PlayerForcedState")]
public class PlayerForcedState : PlayerStateBase
{
    public override void OnEnter()
    {
        base.OnEnter();
        
        player.SetRbVelocity(player.ForcedForce * player.transform.localScale.x);
    }

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();

        if (isAnimFinished)
        {
            playerStateMachine.SwitchState(typeof(PlayerIdleState));
        }
    }
    
}
