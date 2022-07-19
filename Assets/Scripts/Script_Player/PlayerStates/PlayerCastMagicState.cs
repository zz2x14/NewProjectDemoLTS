using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerState/PlayerCastMagicState",fileName = "PlayerCastMagicState")]
public class PlayerCastMagicState : PlayerStateBase
{
    // [SerializeField] private float animProgress;
    // private bool canCastAnim => Time.time - stateStarTime >= animator.GetCurrentAnimatorStateInfo(0).length * animProgress;
    
    public override void OnEnter()
    {
        base.OnEnter();
        
        player.SetRbVelocity(Vector2.zero);
    }

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();


        // if (canCastAnim)
        // {
        //     player.IsCastMagicAnimOver = true;
        // }
 
        if (isAnimFinished)
        {
            playerStateMachine.SwitchState(typeof(PlayerLandState));
        }
    }

    public override void OnExit()
    {
        base.OnExit();

        player.IsCastMagic = false;
    }
    
}
