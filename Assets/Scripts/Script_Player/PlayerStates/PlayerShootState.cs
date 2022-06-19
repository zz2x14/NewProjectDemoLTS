using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerState/PlayerShootState",fileName = "PlayerShootState")]
public class PlayerShootState : PlayerStateBase
{
    [SerializeField] private float animWaitTime;

    public bool isReadyToShoot =>
        animDuration >= playerStateMachine.Anim.GetCurrentAnimatorStateInfo(0).length + animWaitTime;
    
    public override void OnEnter()
    {
        base.OnEnter();
        
        player.SetRbVelocityX(0f);
    }

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();

        if (isReadyToShoot)
        {
            player.Shoot();
            playerStateMachine.SwitchState(typeof(PlayerLandState));
        }
    }
    
}
