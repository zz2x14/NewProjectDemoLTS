using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class EnemyStateMachine : StateMachine
{
    [SerializeField] private List<EnemyStateBase> statesList = new List<EnemyStateBase>();

    public List<EnemyStateBase> StatesList
    {
        get => statesList;
        set => statesList = value;
    }

    private EnemyController enemyController;

    private bool debugCurState = false;
    
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

    protected override void Update()
    {
        base.Update();

#if UNITY_EDITOR
        if (Keyboard.current.homeKey.wasPressedThisFrame)
        {
            debugCurState = !debugCurState;
        }
        if (debugCurState)
        {
            Debug.Log(curState);
        }
#endif
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
