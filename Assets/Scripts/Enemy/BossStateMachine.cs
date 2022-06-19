using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateMachine : StateMachine
{
    [SerializeField] private List<BossStateBase> statesList = new List<BossStateBase>();

    private BossController bossController;

    protected override void Awake()
    {
        base.Awake();

        bossController = GetComponent<BossController>();
    }

    private void OnEnable()
    {
        bossController.OnHurt += ToHurtState;
        bossController.OnDeath += ToDeathState;
    }

    private void OnDisable()
    {
        bossController.OnHurt -= ToHurtState;
        bossController.OnDeath -= ToDeathState;
    }

    private void Start()
    {
        InitializeBossStates();
        
        SwitchOn(statesList[0]);
    }

    private void InitializeBossStates()
    {
        stateTable = new Dictionary<Type, IState>(statesList.Count);
        
        for (int i = 0; i < statesList.Count; i++)
        {
            statesList[i].InitializeState(bossController,this);
            stateTable.Add(statesList[i].GetType(),statesList[i]);
        }
    }

    public void ToHurtState()
    {
        SwitchState(typeof(BossHurtState));
    }

    public void ToDeathState()
    {
        SwitchState(typeof(BossDeathState));
    }
}
