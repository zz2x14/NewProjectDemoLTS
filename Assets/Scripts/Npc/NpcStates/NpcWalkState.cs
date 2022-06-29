using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "NpcState/NpcWalkState",fileName = "NewNpcWalkState")]
public class NpcWalkState : NpcStateBase
{
    [SerializeField] private float walkSpeed;

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
        
        move.MoveToNextPoint(walkSpeed);
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
