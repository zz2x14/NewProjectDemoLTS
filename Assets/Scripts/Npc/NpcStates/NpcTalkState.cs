using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "NpcState/NpcTalkState",fileName = "NewNpcTalkState")]
public class NpcTalkState : NpcStateBase
{
    public override void OnEnter()
    {
        base.OnEnter();

        if (npc.IsMover)
        {
            move.SetRbVelocity(Vector2.zero);
        }
       
    }
}
