using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerState/PlayerHurtState",fileName = "PlayerHurtState")]
public class PlayerHurtState : PlayerStateBase
{
    [SerializeField] private Vector2 playerHurtBackDir;
    
    public override void OnEnter()
    {
        base.OnEnter();
        
        player.SetRbVelocity(playerHurtBackDir);
        player.SetGravity(1f);
    }

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();

        if (isAnimFinished)
        {
            playerStateMachine.SwitchState(typeof(PlayerLandState));
        }
    }

   
    
}
