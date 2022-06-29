using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "NpcState/NpcWalkWaitState",fileName = "NpcWalkWaitState")]
public class NpcWalkWaitState : NpcStateBase
{
    private bool waitOver => Time.time - startTime >= move.WalkWaitTime;

    private float startTime;
    
    public override void OnEnter()
    {
        base.OnEnter();
        
        startTime = Time.time;

        move.SetRbVelocity(Vector2.zero);
        
       
    }

    public override void OnGameLogicUpdate()
    {
        base.OnGameLogicUpdate();
        

        if (waitOver)
        {
            
            stateMachine.SwitchState(typeof(NpcWalkState));
        }
    }
    
}
