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

    protected override void Awake()
    {
        base.Awake();//NOTE:使用继承关系的获取组件 复写生命周期函数才能生效

        playerController = GetComponent<PlayerController>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        playerController.OnHurt += ToHurtState;
    }

    private void OnDisable()
    {
        playerController.OnHurt -= ToHurtState;
    }

    private void Start()
    {
        InitializePlayerStates();
        
        SwitchOn(stateTable[typeof(PlayerIdleState)]);
    }

    // protected override void Update()
    // {
    //     base.Update();
    //     
    //     Debug.Log(curState);
    // }


    private void InitializePlayerStates()
    {
        stateTable = new Dictionary<Type, IState>(playerStates.Count);
        
        for (int i = 0; i < playerStates.Count; i++)
        {
            playerStates[i].InitializeState(this,playerController,playerInput);
            stateTable.Add(playerStates[i].GetType(),playerStates[i]);
        }
    }

    public void ToHurtState()
    {
        SwitchState(typeof(PlayerHurtState));
    }
    
}
