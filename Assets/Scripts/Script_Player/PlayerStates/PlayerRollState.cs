using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerState/PlayerRollState",fileName = "PlayerRollState")]
public class PlayerRollState : PlayerStateBase
{
    [SerializeField] private float rollSpeed;
    
    public override void OnEnter()
    {
        base.OnEnter();
        
        player.CollEndRoll();
        player.SetRbVelocityX(rollSpeed * player.transform.localScale.x);
    }

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();

        if (isAnimFinished)
        {
            playerStateMachine.SwitchState(typeof(PlayerIdleState));
        }
    }

    public override void OnPhysicalLogicUpdate()
    {
        base.OnPhysicalLogicUpdate();
    }

    public override void OnExit()
    {
        base.OnExit();
        
        player.CollStartRoll();
        player.SetRbVelocityX(0f);
    }
    
}
