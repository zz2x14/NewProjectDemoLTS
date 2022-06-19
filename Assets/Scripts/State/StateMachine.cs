using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    protected IState curState;
    protected IState lastState;
    public IState LastState => lastState;
    
    private Animator anim;
    public Animator Anim => anim;
    
    protected Dictionary<Type, IState> stateTable;
    
    protected virtual void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    protected virtual void Update()
    {
        curState.OnGameLogicUpdate();
    }

    protected virtual void FixedUpdate()
    {
        curState.OnPhysicalLogicUpdate();
    }

    protected void SwitchOn(IState newState)
    {
        curState = newState;
        curState.OnEnter();
    }

    public void SwitchState(IState targetState)
    {
        if(curState == targetState) return;
        
        lastState = curState;
        curState.OnExit();
        SwitchOn(targetState);
    }

    public void SwitchState(Type stateType)
    {
        if(curState.GetType() == stateType) return;
        
        lastState = curState;
        curState.OnExit();
        SwitchOn(stateTable[stateType]);
    }

 
}
