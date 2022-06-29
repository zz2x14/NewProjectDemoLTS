using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class PlayerStateMachine : StateMachine
{
    private PlayerController playerController;
    private PlayerAblityManager playerAblityManager;
    private PlayerInput playerInput;
    
    [SerializeField] private List<PlayerStateBase> playerStates = new List<PlayerStateBase>();

    private bool debugCurState;

    protected override void Awake()
    {
        base.Awake();//NOTE:使用继承关系的获取组件 复写生命周期函数才能生效

        playerController = GetComponent<PlayerController>();
        playerAblityManager = GetComponent<PlayerAblityManager>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        playerController.OnHurt += ToHurtState;
        playerController.OnDeath += ToDeathState;
        playerController.OnForced += ToForcedState;
        playerController.OnTalk += ToTalkState;
    }

    private void OnDisable()
    {
        playerController.OnHurt -= ToHurtState;
        playerController.OnDeath -= ToDeathState; 
        playerController.OnForced -= ToForcedState;
        playerController.OnTalk -= ToTalkState;
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
            playerStates[i].InitializeState(this,playerController,playerAblityManager,playerInput);
            stateTable.Add(playerStates[i].GetType(),playerStates[i]);
        }
    }

    private void ToForcedState()
    {
        SwitchState(typeof(PlayerForcedState));
    }

    private void ToTalkState()
    {
        SwitchState(typeof(PlayerTalkState));
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
