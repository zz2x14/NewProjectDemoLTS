using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateBase : ScriptableObject,IState
{
    protected BossStateMachine stateMachine;
    
    protected BossController boss;
    protected BossThreeMelee bossThreeMelee;
        
    private int animNameID;

    [SerializeField] private string animName;

    private float stateStartTime;
    protected float stateDuration => Time.time - stateStartTime;

    protected bool isAnimOver => stateDuration >= stateMachine.Anim.GetCurrentAnimatorStateInfo(0).length;
    
    public void InitializeState(BossController bossController,BossStateMachine bossStateMachine)
    {
        boss = bossController;
        stateMachine = bossStateMachine;

        switch (boss.bossData.enemyType)
        {
            case EnemyType.TrollLike:
                bossThreeMelee = bossController as  BossThreeMelee;
                break;
        }
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
