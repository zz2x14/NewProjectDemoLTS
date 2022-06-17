using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerState/PlayerHurtState",fileName = "PlayerHurtState")]
public class PlayerHurtState : PlayerStateBase
{
    [SerializeField] private Vector2 playerHurtBackDir;
    private Vector2 hurtForce;

    public override void OnEnter()
    {
        base.OnEnter();

        //TODO：优化 击退方向根据目标攻击在角色方向进行判断
        hurtForce = new Vector2(playerHurtBackDir.x * player.transform.localScale.x, playerHurtBackDir.y);
        player.SetRbVelocity(hurtForce);
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
