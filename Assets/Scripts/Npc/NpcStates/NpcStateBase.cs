using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NpcStateBase :  ScriptableObject,IState
{
    protected NpcController npc;
    protected NpcStateMachine stateMachine;

    protected NpcControllerIdle idle;
    protected NpcControllerMove move;

    [SerializeField] private string animName;
    private int animID;

    public void Initialize(NpcController npcController,NpcStateMachine npcStateMachine)
    {
        npc = npcController;
        stateMachine = npcStateMachine;

        if (npc.IsMover)
        {
            move = npcController as NpcControllerMove;
        }
        else
        {
            idle = npcController as NpcControllerIdle;
        }
    }

    private void OnEnable()
    {
        animID = Animator.StringToHash(animName);
    }
    
    public virtual void OnEnter()
    {
        stateMachine.Anim.CrossFade(animID,0.1f);
    }

    public virtual void OnGameLogicUpdate()
    {
        
    }

    public virtual void  OnPhysicalLogicUpdate()
    {
        
    }

    public virtual void OnExit()
    {
        
    }
}
