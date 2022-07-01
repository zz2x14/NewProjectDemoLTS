using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "NpcState/NpcWalkState",fileName = "NewNpcWalkState")]
public class NpcWalkState : NpcStateBase
{
    public override void OnEnter()
    {
        base.OnEnter();
        
        move.FaceToTarget();
    }

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();
        
        if (move.CloseToTargetPoint)
        {
            stateMachine.SwitchState(typeof(NpcWalkWaitState));
        }
    }

    public override void  OnPhysicalLogicUpdate()
    {
        base.OnPhysicalLogicUpdate();
        
        move.MoveToNextPoint();
    }

    public override void OnExit()
    {
        base.OnExit();
        
        move.WalkIndex++;
        if (move.WalkIndex > 1)
        {
            move.WalkIndex = 0;
        }
    }
    
}
