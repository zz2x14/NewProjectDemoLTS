using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "PlayerState/PlayerRunState",fileName = "PlayerRunState")]
public class PlayerRunState : PlayerStateBase
{
    [SerializeField] private string battleRunAnimName;
    [SerializeField] private float noBattleRunSpeed;
    [SerializeField] private float battleRunSpeed;
    [SerializeField] private float acceleration;

    private float runSpeed;
    private int battleRunAnimID;

    protected override void OnEnable()
    {
        base.OnEnable();

        battleRunAnimID = Animator.StringToHash(battleRunAnimName);
    }

    public override void OnEnter()
    {
        stateStarTime = Time.time;
        
        if (GameManager.Instance._BattleState == PlayerBattleState.InBattle)
        {
            runSpeed = battleRunSpeed;
            animator.CrossFade(battleRunAnimID,0.1f);
        }
        else
        {
            runSpeed = noBattleRunSpeed;
            animator.CrossFade(animID,0.1f);
        }
        
        player.JumpCount = 2;

        curSpeed = 0f;
        //此处没有使用 curSpeed ＝ 刚体速度 是因为 避免从其它有速度的状态进入时发生不同情况 TODO:换个思路解决一下？
    }

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();
        
        if (input.IsAttackKeyPressed)
        {
            playerStateMachine.SwitchState(typeof(PlayerAttackState));
        }
        
        if (player.CanShoot && input.IsShootKeyPressed && playerAblity.ShootUnloced)
        {
            playerStateMachine.SwitchState(typeof(PlayerShootState));
        }
        
        if (player.CanRoll && player.RollCDOver && input.IsRollKeyPressed && playerAblity.RollUnlocked)
        {
            playerStateMachine.SwitchState(typeof(PlayerRollState));
        }

        if (!input.IsRunning)
        {
            playerStateMachine.SwitchState(typeof(PlayerIdleState));
        }
        
        if (input.IsJumpKeyPressed)
        {
            playerStateMachine.SwitchState(typeof(PlayerJumpState));
        }

        if (player.IsFalling && !player.IsGrounded)
        {
            playerStateMachine.SwitchState(typeof(PlayerFallState));
        }
        
        
        curSpeed = Mathf.MoveTowards(curSpeed, runSpeed, acceleration * Time.deltaTime);
    }

    public override void OnPhysicalLogicUpdate()
    {
        base.OnPhysicalLogicUpdate();

        player.Move(curSpeed);
    }
    
}
