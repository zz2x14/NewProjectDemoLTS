using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerStateBase : ScriptableObject,IState
{
    protected PlayerStateMachine playerStateMachine;
    protected PlayerController player;
    protected PlayerAblityManager playerAblity;
    protected PlayerInput input;

    protected Animator animator => playerStateMachine.Anim;
    protected int animID;
    [SerializeField] protected string animName;

    protected float curSpeed;

    protected bool isAnimFinished => stateDuration >= animator.GetCurrentAnimatorStateInfo(0).length;
    protected float stateDuration => Time.time - stateStarTime;
    protected float stateStarTime;
    
    protected virtual void OnEnable()//SO是可以使用生命周期函数的
    {
        animID = Animator.StringToHash(animName);
    }

    public void InitializeState(PlayerStateMachine pSM,PlayerController playerController,PlayerAblityManager playerAblityManager,PlayerInput playerInput)
    {
        playerStateMachine = pSM;
        player = playerController;
        playerAblity = playerAblityManager;
        input = playerInput;
    }
    
    public virtual void OnEnter()
    {
        //使用crossFade播放动画 过渡到另一个动画更平滑 第二个参数即为动画跳转中的transitionDuration 0~1的百分比
        animator.CrossFade(animID,0.1f);

        stateStarTime = Time.time;
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
