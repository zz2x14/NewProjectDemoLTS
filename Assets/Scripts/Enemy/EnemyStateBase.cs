using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateBase : ScriptableObject,IState
{
    protected EnemyController enemy;
    protected EnemyStateMachine stateMachine;
    
    private int animNameID;

    [SerializeField] private string animName;

    private float stateStartTime;
    protected float stateDuration => Time.time - stateStartTime;

    protected bool isAnimOver => stateDuration >= stateMachine.Anim.GetCurrentAnimatorStateInfo(0).length;
    
    public virtual void InitializeState(EnemyController enemyController,EnemyStateMachine enemyStateMachine)
    {
        enemy = enemyController;
        stateMachine = enemyStateMachine;
    }

    protected virtual void OnEnable()
    {
        animNameID = Animator.StringToHash(animName);
    }

    public virtual void OnEnter()
    {
        stateMachine.Anim.CrossFade(animNameID,0.1f);
        stateStartTime = Time.time;
    }

    public virtual void OnGameLogicUpdate()
    {
        
    }

    public virtual void OnPhysicalLogicUpdate()
    {
        
    }

    public virtual void OnExit()
    {
        
    }
}
