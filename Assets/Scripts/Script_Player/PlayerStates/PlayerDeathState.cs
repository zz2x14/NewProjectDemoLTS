using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerState/PlayerDeathState",fileName = "PlayerDeathState")]
public class PlayerDeathState : PlayerStateBase
{
    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();

        if (isAnimFinished)
        {
            player.gameObject.SetActive(false);
        }
    }

   
    
}
