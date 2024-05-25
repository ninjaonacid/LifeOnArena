//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/_Project/Code/Runtime/Core/InputSystem/PlayerControls.inputactions
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

public partial class @PlayerControls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""a12c9452-f543-4737-b644-3951b75fda9b"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""a9e2a3f8-4121-4a69-988f-5946d705320e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""SkillSlot1"",
                    ""type"": ""Button"",
                    ""id"": ""178ac7ca-5ceb-4a8f-abec-9c6203ed2673"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SkillSlot2"",
                    ""type"": ""Button"",
                    ""id"": ""41ed16e5-5eff-45b1-a286-11702fecc104"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""a7779f5b-cf7c-48b3-bc8a-1675c43cf8f7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""d02f07f9-a2c5-4c0f-a9ec-503e3fb46ed4"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""68885fa7-ad3f-48cb-a94a-f0de377ee1d8"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""9e0fe09c-f90e-4f85-aa6a-6c6c22d91924"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""1eb68950-9220-4c05-af22-421c1727b041"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""fbbf9f91-f9f7-41a8-9f4d-c6f73a317b78"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""dc9439ab-f41c-4213-903f-6ef8803374b1"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""09e36d34-404f-40a5-9f71-9bb3710cec83"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""2b28997a-c54e-4caa-9635-6eda9ee49aa0"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""1d511d9b-9166-4fc9-bbe3-ead5a58096bf"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""cb8ff796-33d7-4707-9376-c1ff3829161c"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""dc98d6da-3e2e-4028-8c9d-64c78d8d247c"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""3f23d19b-3d3c-44a7-a316-1da3d8bf4d8a"",
                    ""path"": ""<AndroidJoystick>/stick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""ec898f8e-0994-4a21-8563-3ccab57c1c6c"",
                    ""path"": ""<AndroidJoystick>/stick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""5e1a712c-0a75-4552-bbe4-be5865cced32"",
                    ""path"": ""<AndroidJoystick>/stick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""5821a5cf-2691-4621-9a32-ca678eed7b2f"",
                    ""path"": ""<AndroidJoystick>/stick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""7ecec5a9-7d44-4482-a1f0-4594fe53d467"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SkillSlot1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e2314096-f8d4-478d-b0f3-150b16b88336"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""83743a3d-d80b-4e7c-b8ff-df81ebd911e1"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SkillSlot2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""fe003851-54fb-42bb-8181-e36e691ce4ea"",
            ""actions"": [
                {
                    ""name"": ""Click"",
                    ""type"": ""Button"",
                    ""id"": ""2fb91348-e0b8-4e1c-b066-9e0b700581df"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""1e534b4b-dcbc-4b88-a971-a5bc2eac9b27"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""eec8eddb-110d-47c8-a158-d839fef773ab"",
                    ""path"": ""<Touchscreen>/Press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""LevelControls"",
            ""id"": ""da32416d-fe66-44d8-8a25-a12354864b61"",
            ""actions"": [
                {
                    ""name"": ""Button"",
                    ""type"": ""Button"",
                    ""id"": ""9a3ba9c6-0e8c-48f7-9dbe-b8a3eadb33f1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Button1"",
                    ""type"": ""Button"",
                    ""id"": ""d4f678c9-ac4c-458f-8ead-aa7e1bdf5afa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Button2"",
                    ""type"": ""Button"",
                    ""id"": ""88ba256f-4b61-4395-9174-d392cd61a262"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RestartScene"",
                    ""type"": ""Button"",
                    ""id"": ""fa9180b4-a0d8-4935-9782-b6a73d902304"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""6c4b30f2-8341-46f7-b49f-5e7fe7a2f4c7"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Button"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2a417d1d-85da-433b-9dd0-a303fcc8afe6"",
                    ""path"": ""<Keyboard>/l"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RestartScene"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f0567902-7193-4107-aec5-1b85c6fd2510"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Button1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a31b3e7a-c1ad-4e19-83ff-0104f832f458"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Button2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
        m_Player_SkillSlot1 = m_Player.FindAction("SkillSlot1", throwIfNotFound: true);
        m_Player_SkillSlot2 = m_Player.FindAction("SkillSlot2", throwIfNotFound: true);
        m_Player_Attack = m_Player.FindAction("Attack", throwIfNotFound: true);
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_Click = m_UI.FindAction("Click", throwIfNotFound: true);
        // LevelControls
        m_LevelControls = asset.FindActionMap("LevelControls", throwIfNotFound: true);
        m_LevelControls_Button = m_LevelControls.FindAction("Button", throwIfNotFound: true);
        m_LevelControls_Button1 = m_LevelControls.FindAction("Button1", throwIfNotFound: true);
        m_LevelControls_Button2 = m_LevelControls.FindAction("Button2", throwIfNotFound: true);
        m_LevelControls_RestartScene = m_LevelControls.FindAction("RestartScene", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private List<IPlayerActions> m_PlayerActionsCallbackInterfaces = new List<IPlayerActions>();
    private readonly InputAction m_Player_Movement;
    private readonly InputAction m_Player_SkillSlot1;
    private readonly InputAction m_Player_SkillSlot2;
    private readonly InputAction m_Player_Attack;
    public struct PlayerActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @SkillSlot1 => m_Wrapper.m_Player_SkillSlot1;
        public InputAction @SkillSlot2 => m_Wrapper.m_Player_SkillSlot2;
        public InputAction @Attack => m_Wrapper.m_Player_Attack;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Add(instance);
            @Movement.started += instance.OnMovement;
            @Movement.performed += instance.OnMovement;
            @Movement.canceled += instance.OnMovement;
            @SkillSlot1.started += instance.OnSkillSlot1;
            @SkillSlot1.performed += instance.OnSkillSlot1;
            @SkillSlot1.canceled += instance.OnSkillSlot1;
            @SkillSlot2.started += instance.OnSkillSlot2;
            @SkillSlot2.performed += instance.OnSkillSlot2;
            @SkillSlot2.canceled += instance.OnSkillSlot2;
            @Attack.started += instance.OnAttack;
            @Attack.performed += instance.OnAttack;
            @Attack.canceled += instance.OnAttack;
        }

        private void UnregisterCallbacks(IPlayerActions instance)
        {
            @Movement.started -= instance.OnMovement;
            @Movement.performed -= instance.OnMovement;
            @Movement.canceled -= instance.OnMovement;
            @SkillSlot1.started -= instance.OnSkillSlot1;
            @SkillSlot1.performed -= instance.OnSkillSlot1;
            @SkillSlot1.canceled -= instance.OnSkillSlot1;
            @SkillSlot2.started -= instance.OnSkillSlot2;
            @SkillSlot2.performed -= instance.OnSkillSlot2;
            @SkillSlot2.canceled -= instance.OnSkillSlot2;
            @Attack.started -= instance.OnAttack;
            @Attack.performed -= instance.OnAttack;
            @Attack.canceled -= instance.OnAttack;
        }

        public void RemoveCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // UI
    private readonly InputActionMap m_UI;
    private List<IUIActions> m_UIActionsCallbackInterfaces = new List<IUIActions>();
    private readonly InputAction m_UI_Click;
    public struct UIActions
    {
        private @PlayerControls m_Wrapper;
        public UIActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Click => m_Wrapper.m_UI_Click;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void AddCallbacks(IUIActions instance)
        {
            if (instance == null || m_Wrapper.m_UIActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_UIActionsCallbackInterfaces.Add(instance);
            @Click.started += instance.OnClick;
            @Click.performed += instance.OnClick;
            @Click.canceled += instance.OnClick;
        }

        private void UnregisterCallbacks(IUIActions instance)
        {
            @Click.started -= instance.OnClick;
            @Click.performed -= instance.OnClick;
            @Click.canceled -= instance.OnClick;
        }

        public void RemoveCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IUIActions instance)
        {
            foreach (var item in m_Wrapper.m_UIActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_UIActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public UIActions @UI => new UIActions(this);

    // LevelControls
    private readonly InputActionMap m_LevelControls;
    private List<ILevelControlsActions> m_LevelControlsActionsCallbackInterfaces = new List<ILevelControlsActions>();
    private readonly InputAction m_LevelControls_Button;
    private readonly InputAction m_LevelControls_Button1;
    private readonly InputAction m_LevelControls_Button2;
    private readonly InputAction m_LevelControls_RestartScene;
    public struct LevelControlsActions
    {
        private @PlayerControls m_Wrapper;
        public LevelControlsActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Button => m_Wrapper.m_LevelControls_Button;
        public InputAction @Button1 => m_Wrapper.m_LevelControls_Button1;
        public InputAction @Button2 => m_Wrapper.m_LevelControls_Button2;
        public InputAction @RestartScene => m_Wrapper.m_LevelControls_RestartScene;
        public InputActionMap Get() { return m_Wrapper.m_LevelControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(LevelControlsActions set) { return set.Get(); }
        public void AddCallbacks(ILevelControlsActions instance)
        {
            if (instance == null || m_Wrapper.m_LevelControlsActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_LevelControlsActionsCallbackInterfaces.Add(instance);
            @Button.started += instance.OnButton;
            @Button.performed += instance.OnButton;
            @Button.canceled += instance.OnButton;
            @Button1.started += instance.OnButton1;
            @Button1.performed += instance.OnButton1;
            @Button1.canceled += instance.OnButton1;
            @Button2.started += instance.OnButton2;
            @Button2.performed += instance.OnButton2;
            @Button2.canceled += instance.OnButton2;
            @RestartScene.started += instance.OnRestartScene;
            @RestartScene.performed += instance.OnRestartScene;
            @RestartScene.canceled += instance.OnRestartScene;
        }

        private void UnregisterCallbacks(ILevelControlsActions instance)
        {
            @Button.started -= instance.OnButton;
            @Button.performed -= instance.OnButton;
            @Button.canceled -= instance.OnButton;
            @Button1.started -= instance.OnButton1;
            @Button1.performed -= instance.OnButton1;
            @Button1.canceled -= instance.OnButton1;
            @Button2.started -= instance.OnButton2;
            @Button2.performed -= instance.OnButton2;
            @Button2.canceled -= instance.OnButton2;
            @RestartScene.started -= instance.OnRestartScene;
            @RestartScene.performed -= instance.OnRestartScene;
            @RestartScene.canceled -= instance.OnRestartScene;
        }

        public void RemoveCallbacks(ILevelControlsActions instance)
        {
            if (m_Wrapper.m_LevelControlsActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ILevelControlsActions instance)
        {
            foreach (var item in m_Wrapper.m_LevelControlsActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_LevelControlsActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public LevelControlsActions @LevelControls => new LevelControlsActions(this);
    public interface IPlayerActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnSkillSlot1(InputAction.CallbackContext context);
        void OnSkillSlot2(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
    }
    public interface IUIActions
    {
        void OnClick(InputAction.CallbackContext context);
    }
    public interface ILevelControlsActions
    {
        void OnButton(InputAction.CallbackContext context);
        void OnButton1(InputAction.CallbackContext context);
        void OnButton2(InputAction.CallbackContext context);
        void OnRestartScene(InputAction.CallbackContext context);
    }
}
