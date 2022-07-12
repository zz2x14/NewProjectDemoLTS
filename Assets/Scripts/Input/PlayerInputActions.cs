//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Settings/InputActions/PlayerInputActions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInputActions : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""afdc19a5-8a84-42c4-a9c3-5ae503df6b45"",
            ""actions"": [
                {
                    ""name"": ""AxisXMove"",
                    ""type"": ""Value"",
                    ""id"": ""e6e1edc4-e64d-445c-980e-ffe256f97030"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""a32be68d-4a9a-4ae5-b4bf-53304927300d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""0f13614a-d3a4-45ef-ab1d-fff0be139fe0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Roll"",
                    ""type"": ""Button"",
                    ""id"": ""9b32179a-2769-46da-812f-bdef13a56d57"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Climb"",
                    ""type"": ""Button"",
                    ""id"": ""560d653a-f672-45f2-88aa-1c6027032662"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Fall"",
                    ""type"": ""Button"",
                    ""id"": ""d4bbc81e-7551-45a7-91e8-cc73a58cf765"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""9e85a44e-c8ce-4a2a-997e-ae0562eb2513"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""GameConfirm"",
                    ""type"": ""Button"",
                    ""id"": ""01231787-22ad-4d15-8a53-cb85105df4fb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""5eb27be6-b18b-42fd-afbe-0a3fda67fcca"",
                    ""path"": ""2DVector(mode=1)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AxisXMove"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""left"",
                    ""id"": ""9a5792a5-0206-46e1-8a60-b7b9fc796865"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""AxisXMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""a7e4e7bd-205c-464c-a486-1204aa397992"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""AxisXMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""b5f2e73f-b898-44b3-ab97-ecb3b9405117"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3de2a5fb-0bcd-454f-aa05-1c8c63380fd3"",
                    ""path"": ""<Keyboard>/j"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e2a20025-ef03-4581-8a40-9cc55e7bc477"",
                    ""path"": ""<Keyboard>/l"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3deb9a3a-6bca-4b6e-942b-15fb9ab2389a"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Climb"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""18fa9652-a9f4-4bf0-a8be-24dc431770ba"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Fall"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""afa54c6a-7be7-409a-8c7c-f6ba7e15f4b6"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""72214afe-3e36-4b56-bbc4-2b0c26117258"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""GameConfirm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""SceneTeleport"",
            ""id"": ""188658b1-e431-4c23-8fe1-5d8d0b47dfea"",
            ""actions"": [
                {
                    ""name"": ""Confirm"",
                    ""type"": ""Button"",
                    ""id"": ""fa086871-8a32-489c-9782-dacf24d898f3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""dfeded95-6030-471e-863b-9c6e74885868"",
                    ""path"": ""<Keyboard>/anyKey"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Confirm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Talk"",
            ""id"": ""8bb7be3e-8947-4917-ba8a-27835b75e7ae"",
            ""actions"": [
                {
                    ""name"": ""Skip"",
                    ""type"": ""Button"",
                    ""id"": ""f1505a0c-1d6d-4358-97e7-41c6b0365e27"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SpeedUp"",
                    ""type"": ""Button"",
                    ""id"": ""094ba4ce-a6fb-4702-86b3-c6b1af4536f6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""9a411835-b624-4489-840f-0981d4423c2f"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Skip"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""83eaff0b-c783-4461-802d-760d39d275f3"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""SpeedUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""PlayerMenu"",
            ""id"": ""ef351b74-2b47-4021-8b17-fb04119cd922"",
            ""actions"": [
                {
                    ""name"": ""Switch"",
                    ""type"": ""Button"",
                    ""id"": ""613c57dd-91ca-41ad-9656-b17ef859198f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""DropItem"",
                    ""type"": ""Button"",
                    ""id"": ""2d3d7f27-6e67-48f0-bb16-6e8c1d017903"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SwitchNext"",
                    ""type"": ""Button"",
                    ""id"": ""38cbe177-6a01-48e9-b024-82345fd538fb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SwithLast"",
                    ""type"": ""Button"",
                    ""id"": ""3f09657d-b77c-45dc-8952-197945ced154"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""30d849e3-7e32-4b8f-b5ba-a54ea9351112"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Switch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""43328212-261a-41ea-b5d5-feaa3b3aad57"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""DropItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0f5a0493-6ada-46d5-9793-8f0043ce1bcf"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""SwithLast"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""eae79e96-330c-45ea-a66f-95226ffe10a4"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""SwitchNext"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""PC"",
            ""bindingGroup"": ""PC"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_AxisXMove = m_Gameplay.FindAction("AxisXMove", throwIfNotFound: true);
        m_Gameplay_Jump = m_Gameplay.FindAction("Jump", throwIfNotFound: true);
        m_Gameplay_Attack = m_Gameplay.FindAction("Attack", throwIfNotFound: true);
        m_Gameplay_Roll = m_Gameplay.FindAction("Roll", throwIfNotFound: true);
        m_Gameplay_Climb = m_Gameplay.FindAction("Climb", throwIfNotFound: true);
        m_Gameplay_Fall = m_Gameplay.FindAction("Fall", throwIfNotFound: true);
        m_Gameplay_Shoot = m_Gameplay.FindAction("Shoot", throwIfNotFound: true);
        m_Gameplay_GameConfirm = m_Gameplay.FindAction("GameConfirm", throwIfNotFound: true);
        // SceneTeleport
        m_SceneTeleport = asset.FindActionMap("SceneTeleport", throwIfNotFound: true);
        m_SceneTeleport_Confirm = m_SceneTeleport.FindAction("Confirm", throwIfNotFound: true);
        // Talk
        m_Talk = asset.FindActionMap("Talk", throwIfNotFound: true);
        m_Talk_Skip = m_Talk.FindAction("Skip", throwIfNotFound: true);
        m_Talk_SpeedUp = m_Talk.FindAction("SpeedUp", throwIfNotFound: true);
        // PlayerMenu
        m_PlayerMenu = asset.FindActionMap("PlayerMenu", throwIfNotFound: true);
        m_PlayerMenu_Switch = m_PlayerMenu.FindAction("Switch", throwIfNotFound: true);
        m_PlayerMenu_DropItem = m_PlayerMenu.FindAction("DropItem", throwIfNotFound: true);
        m_PlayerMenu_SwitchNext = m_PlayerMenu.FindAction("SwitchNext", throwIfNotFound: true);
        m_PlayerMenu_SwithLast = m_PlayerMenu.FindAction("SwithLast", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_AxisXMove;
    private readonly InputAction m_Gameplay_Jump;
    private readonly InputAction m_Gameplay_Attack;
    private readonly InputAction m_Gameplay_Roll;
    private readonly InputAction m_Gameplay_Climb;
    private readonly InputAction m_Gameplay_Fall;
    private readonly InputAction m_Gameplay_Shoot;
    private readonly InputAction m_Gameplay_GameConfirm;
    public struct GameplayActions
    {
        private @PlayerInputActions m_Wrapper;
        public GameplayActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @AxisXMove => m_Wrapper.m_Gameplay_AxisXMove;
        public InputAction @Jump => m_Wrapper.m_Gameplay_Jump;
        public InputAction @Attack => m_Wrapper.m_Gameplay_Attack;
        public InputAction @Roll => m_Wrapper.m_Gameplay_Roll;
        public InputAction @Climb => m_Wrapper.m_Gameplay_Climb;
        public InputAction @Fall => m_Wrapper.m_Gameplay_Fall;
        public InputAction @Shoot => m_Wrapper.m_Gameplay_Shoot;
        public InputAction @GameConfirm => m_Wrapper.m_Gameplay_GameConfirm;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @AxisXMove.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAxisXMove;
                @AxisXMove.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAxisXMove;
                @AxisXMove.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAxisXMove;
                @Jump.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @Attack.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAttack;
                @Roll.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRoll;
                @Roll.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRoll;
                @Roll.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRoll;
                @Climb.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnClimb;
                @Climb.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnClimb;
                @Climb.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnClimb;
                @Fall.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnFall;
                @Fall.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnFall;
                @Fall.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnFall;
                @Shoot.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShoot;
                @GameConfirm.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnGameConfirm;
                @GameConfirm.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnGameConfirm;
                @GameConfirm.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnGameConfirm;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @AxisXMove.started += instance.OnAxisXMove;
                @AxisXMove.performed += instance.OnAxisXMove;
                @AxisXMove.canceled += instance.OnAxisXMove;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @Roll.started += instance.OnRoll;
                @Roll.performed += instance.OnRoll;
                @Roll.canceled += instance.OnRoll;
                @Climb.started += instance.OnClimb;
                @Climb.performed += instance.OnClimb;
                @Climb.canceled += instance.OnClimb;
                @Fall.started += instance.OnFall;
                @Fall.performed += instance.OnFall;
                @Fall.canceled += instance.OnFall;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @GameConfirm.started += instance.OnGameConfirm;
                @GameConfirm.performed += instance.OnGameConfirm;
                @GameConfirm.canceled += instance.OnGameConfirm;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);

    // SceneTeleport
    private readonly InputActionMap m_SceneTeleport;
    private ISceneTeleportActions m_SceneTeleportActionsCallbackInterface;
    private readonly InputAction m_SceneTeleport_Confirm;
    public struct SceneTeleportActions
    {
        private @PlayerInputActions m_Wrapper;
        public SceneTeleportActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Confirm => m_Wrapper.m_SceneTeleport_Confirm;
        public InputActionMap Get() { return m_Wrapper.m_SceneTeleport; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(SceneTeleportActions set) { return set.Get(); }
        public void SetCallbacks(ISceneTeleportActions instance)
        {
            if (m_Wrapper.m_SceneTeleportActionsCallbackInterface != null)
            {
                @Confirm.started -= m_Wrapper.m_SceneTeleportActionsCallbackInterface.OnConfirm;
                @Confirm.performed -= m_Wrapper.m_SceneTeleportActionsCallbackInterface.OnConfirm;
                @Confirm.canceled -= m_Wrapper.m_SceneTeleportActionsCallbackInterface.OnConfirm;
            }
            m_Wrapper.m_SceneTeleportActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Confirm.started += instance.OnConfirm;
                @Confirm.performed += instance.OnConfirm;
                @Confirm.canceled += instance.OnConfirm;
            }
        }
    }
    public SceneTeleportActions @SceneTeleport => new SceneTeleportActions(this);

    // Talk
    private readonly InputActionMap m_Talk;
    private ITalkActions m_TalkActionsCallbackInterface;
    private readonly InputAction m_Talk_Skip;
    private readonly InputAction m_Talk_SpeedUp;
    public struct TalkActions
    {
        private @PlayerInputActions m_Wrapper;
        public TalkActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Skip => m_Wrapper.m_Talk_Skip;
        public InputAction @SpeedUp => m_Wrapper.m_Talk_SpeedUp;
        public InputActionMap Get() { return m_Wrapper.m_Talk; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TalkActions set) { return set.Get(); }
        public void SetCallbacks(ITalkActions instance)
        {
            if (m_Wrapper.m_TalkActionsCallbackInterface != null)
            {
                @Skip.started -= m_Wrapper.m_TalkActionsCallbackInterface.OnSkip;
                @Skip.performed -= m_Wrapper.m_TalkActionsCallbackInterface.OnSkip;
                @Skip.canceled -= m_Wrapper.m_TalkActionsCallbackInterface.OnSkip;
                @SpeedUp.started -= m_Wrapper.m_TalkActionsCallbackInterface.OnSpeedUp;
                @SpeedUp.performed -= m_Wrapper.m_TalkActionsCallbackInterface.OnSpeedUp;
                @SpeedUp.canceled -= m_Wrapper.m_TalkActionsCallbackInterface.OnSpeedUp;
            }
            m_Wrapper.m_TalkActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Skip.started += instance.OnSkip;
                @Skip.performed += instance.OnSkip;
                @Skip.canceled += instance.OnSkip;
                @SpeedUp.started += instance.OnSpeedUp;
                @SpeedUp.performed += instance.OnSpeedUp;
                @SpeedUp.canceled += instance.OnSpeedUp;
            }
        }
    }
    public TalkActions @Talk => new TalkActions(this);

    // PlayerMenu
    private readonly InputActionMap m_PlayerMenu;
    private IPlayerMenuActions m_PlayerMenuActionsCallbackInterface;
    private readonly InputAction m_PlayerMenu_Switch;
    private readonly InputAction m_PlayerMenu_DropItem;
    private readonly InputAction m_PlayerMenu_SwitchNext;
    private readonly InputAction m_PlayerMenu_SwithLast;
    public struct PlayerMenuActions
    {
        private @PlayerInputActions m_Wrapper;
        public PlayerMenuActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Switch => m_Wrapper.m_PlayerMenu_Switch;
        public InputAction @DropItem => m_Wrapper.m_PlayerMenu_DropItem;
        public InputAction @SwitchNext => m_Wrapper.m_PlayerMenu_SwitchNext;
        public InputAction @SwithLast => m_Wrapper.m_PlayerMenu_SwithLast;
        public InputActionMap Get() { return m_Wrapper.m_PlayerMenu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerMenuActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerMenuActions instance)
        {
            if (m_Wrapper.m_PlayerMenuActionsCallbackInterface != null)
            {
                @Switch.started -= m_Wrapper.m_PlayerMenuActionsCallbackInterface.OnSwitch;
                @Switch.performed -= m_Wrapper.m_PlayerMenuActionsCallbackInterface.OnSwitch;
                @Switch.canceled -= m_Wrapper.m_PlayerMenuActionsCallbackInterface.OnSwitch;
                @DropItem.started -= m_Wrapper.m_PlayerMenuActionsCallbackInterface.OnDropItem;
                @DropItem.performed -= m_Wrapper.m_PlayerMenuActionsCallbackInterface.OnDropItem;
                @DropItem.canceled -= m_Wrapper.m_PlayerMenuActionsCallbackInterface.OnDropItem;
                @SwitchNext.started -= m_Wrapper.m_PlayerMenuActionsCallbackInterface.OnSwitchNext;
                @SwitchNext.performed -= m_Wrapper.m_PlayerMenuActionsCallbackInterface.OnSwitchNext;
                @SwitchNext.canceled -= m_Wrapper.m_PlayerMenuActionsCallbackInterface.OnSwitchNext;
                @SwithLast.started -= m_Wrapper.m_PlayerMenuActionsCallbackInterface.OnSwithLast;
                @SwithLast.performed -= m_Wrapper.m_PlayerMenuActionsCallbackInterface.OnSwithLast;
                @SwithLast.canceled -= m_Wrapper.m_PlayerMenuActionsCallbackInterface.OnSwithLast;
            }
            m_Wrapper.m_PlayerMenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Switch.started += instance.OnSwitch;
                @Switch.performed += instance.OnSwitch;
                @Switch.canceled += instance.OnSwitch;
                @DropItem.started += instance.OnDropItem;
                @DropItem.performed += instance.OnDropItem;
                @DropItem.canceled += instance.OnDropItem;
                @SwitchNext.started += instance.OnSwitchNext;
                @SwitchNext.performed += instance.OnSwitchNext;
                @SwitchNext.canceled += instance.OnSwitchNext;
                @SwithLast.started += instance.OnSwithLast;
                @SwithLast.performed += instance.OnSwithLast;
                @SwithLast.canceled += instance.OnSwithLast;
            }
        }
    }
    public PlayerMenuActions @PlayerMenu => new PlayerMenuActions(this);
    private int m_PCSchemeIndex = -1;
    public InputControlScheme PCScheme
    {
        get
        {
            if (m_PCSchemeIndex == -1) m_PCSchemeIndex = asset.FindControlSchemeIndex("PC");
            return asset.controlSchemes[m_PCSchemeIndex];
        }
    }
    public interface IGameplayActions
    {
        void OnAxisXMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnRoll(InputAction.CallbackContext context);
        void OnClimb(InputAction.CallbackContext context);
        void OnFall(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnGameConfirm(InputAction.CallbackContext context);
    }
    public interface ISceneTeleportActions
    {
        void OnConfirm(InputAction.CallbackContext context);
    }
    public interface ITalkActions
    {
        void OnSkip(InputAction.CallbackContext context);
        void OnSpeedUp(InputAction.CallbackContext context);
    }
    public interface IPlayerMenuActions
    {
        void OnSwitch(InputAction.CallbackContext context);
        void OnDropItem(InputAction.CallbackContext context);
        void OnSwitchNext(InputAction.CallbackContext context);
        void OnSwithLast(InputAction.CallbackContext context);
    }
}
