using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerState/PlayerAttack2State",fileName = "PlayerAttack2State")]
public class PlayerAttack2State : PlayerStateBase
{
    [SerializeField] private float attackWaitTime;

    private bool canSwitchIdleState => 
        animDuration >= animator.GetCurrentAnimatorStateInfo(0).length  + attackWaitTime;

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();
        
        if (input.IsRunning)
        {
            playerStateMachine.SwitchState(typeof(PlayerRunState));
            
            return;
        }
       
        if (isAnimFinished)
        {
            player.SetRbVelocity(new Vector2(0,0));
            
            if (input.IsAttackKeyPressed && playerAblity.ThirdAttackUnlocked)
            {
                playerStateMachine.SwitchState(typeof(PlayerAttack3State));
            }
            else
            {
                animator.Play("PlayerIdle");
            }
        }

        if (canSwitchIdleState)
        {
            playerStateMachine.SwitchState(typeof(PlayerIdleState));
        }
    }
}
