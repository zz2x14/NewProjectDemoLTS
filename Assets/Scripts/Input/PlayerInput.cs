using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour//Sign:更新模式会影响到按键判定！
{
    private PlayerInputActions playerInputActions;
    
    /// <summary>
    /// 暂时不使用该方法进行消失检测
    /// </summary>
    //public Dictionary<InputType,Action> inputRespondTable;
    
    //public List<GuideUIRespondInput> guideUIList;

    // public event Action OnPlayerMove = delegate {  };
    // public event Action OnPlayerJump = delegate {  };
    // public event Action OnPlayerClimbUp = delegate {  };
    // public event Action OnPlayerClimbDown = delegate {  };
    // public event Action OnPlayerAttack = delegate {  };
    // #region InputAction事件
    //
    // public void OnAxisXMove(InputAction.CallbackContext context)
    // {
    //     if (Mathf.Abs(context.ReadValue<Vector2>().x) > 0f)
    //     {
    //         OnPlayerMove?.Invoke();
    //     }
    // }
    //
    // public void OnJump(InputAction.CallbackContext context)
    // {
    //     if (context.started)
    //     {
    //         OnPlayerJump?.Invoke();
    //     }
    // }
    //
    // public void OnAttack(InputAction.CallbackContext context)
    // {
    //     OnPlayerAttack?.Invoke();
    // }
    //
    // public void OnRoll(InputAction.CallbackContext context)
    // {
    //     
    // }
    //
    // public void OnClimb(InputAction.CallbackContext context)
    // {
    //     OnPlayerClimbUp.Invoke();
    // }
    //
    // public void OnFall(InputAction.CallbackContext context)
    // {
    //     OnPlayerClimbDown.Invoke();
    // }
    //
    // public void OnShoot(InputAction.CallbackContext context)
    // {
    //    
    // }
    //
    // public void OnGameConfirm(InputAction.CallbackContext context)
    // {
    //     
    // }
    //
    //
    // #endregion
    
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

    public bool IsGameConfirmKeyPressed => playerInputActions.Gameplay.GameConfirm.WasPressedThisFrame();

    //SystemKeys
    public bool IsSceneTeleportConfirmKeyPressed => playerInputActions.SceneTeleport.Confirm.WasPressedThisFrame();
    
    //Talk
    public bool IsSkipTalkKeyPressed => playerInputActions.Talk.Skip.WasPressedThisFrame();
    public bool IsSpeedUpKeyPerformed => playerInputActions.Talk.SpeedUp.WasPerformedThisFrame();
    public bool IsSpeedUpKeyReleased => playerInputActions.Talk.SpeedUp.WasReleasedThisFrame();
    
    //PlayerMenu
    public bool IsMenuSwitchKeyPressed => playerInputActions.PlayerMenu.Switch.WasPressedThisFrame();
    public bool IsRightMousePressed => playerInputActions.PlayerMenu.RightMouse.WasPressedThisFrame();

    private void OnEnable()
   {
       playerInputActions = new PlayerInputActions();//是要初始化的

       //playerInputActions.Gameplay.SetCallbacks(this);//Sign：使用接口注册事件是要登记的
   }

    private void OnDisable()
   {
       DisableAllInput();
       
       // inputRespondTable.Clear();
       // guideUIList.Clear();
   }

     
     public void EnableOneInput(InputAction inputAction) => inputAction.Enable();
     public void DisableOneInput(InputAction inputAction) => inputAction.Disable();
    
     public void EnableJumpInput() => EnableOneInput(playerInputActions.Gameplay.Jump);
     public void EnableClimbUpInput() => EnableOneInput(playerInputActions.Gameplay.Climb);
     public void EnableFallInput() => EnableOneInput(playerInputActions.Gameplay.Fall);
     public void EnableAttackInput() => EnableOneInput(playerInputActions.Gameplay.Attack);


     public void DisableAllInput()
     {
         playerInputActions.Disable();
     }

     public void EnableAllInput()
     {
         playerInputActions.Enable();
     }

     public void EnableGameplayInput()
     {
         playerInputActions.Gameplay.Enable(); 
     }
     public void DisableGamePlayInput()
     {
         playerInputActions.Gameplay.Disable();
     }

     public void EnableSceneTeleportInput()
     {
         DisableAllInput();
         playerInputActions.SceneTeleport.Enable();
     }
     public void DisableSceneTeleportInput()
     {
         playerInputActions.SceneTeleport.Disable();
     }
     
     public void EnableOnlyMoveInput()
     {
         DisableAllInput();
         playerInputActions.Gameplay.AxisXMove.Enable();
     }

     public void EnbaleOnlyTalkInput()
     {
         DisableAllInput();
         playerInputActions.Talk.Enable();
     }

     public void DisableTalkInput()
     {
         playerInputActions.Talk.Disable();
         EnableGameplayInput();
     }


     
     
}
