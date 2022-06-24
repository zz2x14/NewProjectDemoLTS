using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class PlayerStateMachine : StateMachine
{
    private PlayerController playerController;
    private PlayerInput playerInput;
    
    [SerializeField] private List<PlayerStateBase> playerStates = new List<PlayerStateBase>();

    private bool debugCurState;

    protected override void Awake()
    {
        base.Awake();//NOTE:使用继承关系的获取组件 复写生命周期函数才能生效

        playerController = GetComponent<PlayerController>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        playerController.OnHurt += ToHurtState;
        playerController.OnDeath += ToDeathState;
    }

    private void OnDisable()
    {
        playerController.OnHurt -= ToHurtState;
        playerController.OnDeath -= ToDeathState;
    }

    private void Start()
    {
        InitializePlayerStates();
        
        SwitchOn(stateTable[typeof(PlayerIdleState)]);
    }

    protected override void Update()
    {
        base.Update();

        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            debugCurState = !debugCurState;
        }

        if (debugCurState)
        {
            Debug.Log(curState);
        }
    }


    private void InitializePlayerStates()
    {
        stateTable = new Dictionary<Type, IState>(playerStates.Count);
        
        for (int i = 0; i < playerStates.Count; i++)
        {
            playerStates[i].InitializeState(this,playerController,playerInput);
            stateTable.Add(playerStates[i].GetType(),playerStates[i]);
        }
    }

    private void ToHurtState()
    {
        SwitchState(typeof(PlayerHurtState));
    }

    private void ToDeathState()
    {
        SwitchState(typeof(PlayerDeathState));
    }
    
}
