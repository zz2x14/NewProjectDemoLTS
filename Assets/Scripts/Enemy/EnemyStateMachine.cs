using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyStateMachine : StateMachine
{
    [SerializeField] private List<EnemyStateBase> statesList = new List<EnemyStateBase>();

    private EnemyController enemyController;

    protected override void Awake()
    {
        base.Awake();

        enemyController = GetComponent<EnemyController>();
    }

    private void OnEnable()
    {
        enemyController.OnHurt += ToHurtState;
        enemyController.OnDeath += ToDeathState;
    }

    private void OnDisable()
    {
        enemyController.OnHurt -= ToHurtState;
        enemyController.OnDeath -= ToDeathState;
    }

    private void Start()
    {
        InitializeEnemeyStates();
        
        SwitchOn(statesList[0]);
    }

    private void InitializeEnemeyStates()
    {
        stateTable = new Dictionary<Type, IState>(statesList.Count);
        
        for (int i = 0; i < statesList.Count; i++)
        {
            statesList[i].InitializeState(enemyController,this);
            stateTable.Add(statesList[i].GetType(),statesList[i]);
        }
    }

    private void ToHurtState()
    {
        SwitchState(typeof(EnemyHurtState));
    }

    private void ToDeathState()
    {
        SwitchState(typeof(EnemyDeathState));
    }

}
