using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "PlayerState/PlayerAttack3State",fileName = "PlayerAttack3State")]
public class PlayerAttack3State : PlayerStateBase
{
     [SerializeField] private float attackMoveDis;
     [SerializeField] private float attackWaitTime;
     private bool isAnimLoopTwice => stateDuration >= animator.GetCurrentAnimatorStateInfo(0).length * 2 + attackWaitTime;

    public override void OnEnter()
    {
        base.OnEnter();
        
        player.SetRbVelocityX(attackMoveDis * player.transform.localScale.x);
    }

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();
        
        if (input.IsRunning)
        {
            playerStateMachine.SwitchState(typeof(PlayerRunState));
            
            return;
        }
        
        if (isAnimLoopTwice)
        {
            playerStateMachine.SwitchState(typeof(PlayerIdleState));
        }
    }
}
