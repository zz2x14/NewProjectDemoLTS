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

    private float animStartTime;
    private float animDuration => Time.time - animStartTime;

    protected bool isAnimOver => animDuration >= stateMachine.Anim.GetCurrentAnimatorStateInfo(0).length;
    
    public void InitializeState(EnemyController enemyController,EnemyStateMachine enemyStateMachine)
    {
        enemy = enemyController;
        stateMachine = enemyStateMachine;
    }

    private void OnEnable()
    {
        animNameID = Animator.StringToHash(animName);
        animStartTime = Time.time;
    }

    public virtual void OnEnter()
    {
        stateMachine.Anim.CrossFade(animNameID,0.1f);
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
