using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerState/PlayerTalkState",fileName = "PlayerTalkState")]
public class PlayerTalkState : PlayerStateBase
{
    public override void OnEnter()
    {
        base.OnEnter();
        
        player.SetRbVelocity(Vector2.zero);
    }

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();

        if (!TalkCenter.Instance.IsTalking)
        {
            playerStateMachine.SwitchState(typeof(PlayerIdleState));
        }
    }

  
    public override void OnExit()
    {
        base.OnExit();
        
        player.TalkOver();
    }
    
}
