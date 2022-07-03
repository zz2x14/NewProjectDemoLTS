using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerState/PlayerAttackState",fileName = "PlayerAttackState")]
public class PlayerAttackState : PlayerStateBase
{
    [SerializeField] private float attackWaitTime;

    private bool canSwitchIdleState => 
        stateDuration >= animator.GetCurrentAnimatorStateInfo(0).length + attackWaitTime;
    
    public override void OnEnter()
    {
        base.OnEnter();
        
        player.SetRbVelocity(new Vector2(0,0));
    }

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();

        if (input.IsRunKey)
        {
            playerStateMachine.SwitchState(typeof(PlayerRunState));
            
            return;
        }
        
        if (isAnimFinished)
        {
            
            if (input.IsAttackKeyPressed)
            {
                playerStateMachine.SwitchState(typeof(PlayerAttack2State));
            }
            else
            {
                animator.Play("PlayerIdle");//攻击1动画播放完毕时再次按下攻击键进入攻击2，未按下播放站立动画
            }
        }

        if (canSwitchIdleState)//使在攻击1动画完毕后一定时间内按下攻击都可以展开攻击2
        {
            playerStateMachine.SwitchState(typeof(PlayerIdleState));
        }
    }

   
}
