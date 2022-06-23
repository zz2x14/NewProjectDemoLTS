using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private PlayerInputActions playerInputActions;

    private Vector2 moveXInput => playerInputActions.Gameplay.AxisXMove.ReadValue<Vector2>();
    public float MoveXInputX => moveXInput.x;
    public bool IsRunning => Mathf.Abs(moveXInput.x) > 0;

    public bool IsRunKey => playerInputActions.Gameplay.AxisXMove.WasPerformedThisFrame();
    public bool IsJumpKeyPressed => playerInputActions.Gameplay.Jump.WasPressedThisFrame();
    public bool IsAttackKeyPressed => playerInputActions.Gameplay.Attack.WasPressedThisFrame();
    public bool IsShootKeyPressed => playerInputActions.Gameplay.Shoot.WasPressedThisFrame();
    public bool IsRollKeyPressed => playerInputActions.Gameplay.Roll.WasPressedThisFrame();
    public bool IsFallKeyPressed => playerInputActions.Gameplay.Fall.WasPressedThisFrame();
    public bool IsFallKey => playerInputActions.Gameplay.Fall.WasPerformedThisFrame();
    public bool IsFallKeyReleased => playerInputActions.Gameplay.Fall.WasReleasedThisFrame();
    public bool IsClimbKeyPressed => playerInputActions.Gameplay.Climb.WasPressedThisFrame();
    public bool IsClimbKey => playerInputActions.Gameplay.Climb.WasPerformedThisFrame();
    public bool IsClimbKeyReleased => playerInputActions.Gameplay.Climb.WasReleasedThisFrame();
    
   private void Awake()
   {
       playerInputActions = new PlayerInputActions();//是要初始化的
   }

   public void EnableGameplayInput()
   {
        playerInputActions.Gameplay.Enable(); 
   }

   public void DisableGamePlayeInput()
   {
       playerInputActions.Gameplay.Disable();
   }
    
}
