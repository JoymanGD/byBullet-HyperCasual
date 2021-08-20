// GENERATED AUTOMATICALLY FROM 'Assets/Settings/Input/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Main"",
            ""id"": ""a05f8152-29b8-4663-8985-4e99be95dcb6"",
            ""actions"": [
                {
                    ""name"": ""Tap"",
                    ""type"": ""Button"",
                    ""id"": ""a95c7c26-86fe-4fda-83b6-7b5d4b49a0ba"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Position"",
                    ""type"": ""Value"",
                    ""id"": ""5fef888e-798d-4235-b45c-886e4f682a86"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""9b2d2950-1e97-449a-86e9-6ec6cadb392e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""330c3537-074f-42b9-8e7e-041170fa9063"",
                    ""path"": ""<Pointer>/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Tap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f78167fc-31e4-4a5b-a4ee-b2b0c5814c7b"",
                    ""path"": ""<Pointer>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Position"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0f6cdf87-9e11-4cc6-a1f7-6fa2cd92a89a"",
                    ""path"": ""<Pointer>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""2c2ecc6a-c040-46f4-9ef3-0f198d260b8b"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""7f051b4c-f5cc-4ce0-b765-09824b966431"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""3ebac2a0-b026-4866-ae74-ffd6af96962f"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""134a709f-4500-4c75-ba89-b033ed811936"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""aa94105c-f3d4-45ba-b67a-653fe41a365e"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Main
        m_Main = asset.FindActionMap("Main", throwIfNotFound: true);
        m_Main_Tap = m_Main.FindAction("Tap", throwIfNotFound: true);
        m_Main_Position = m_Main.FindAction("Position", throwIfNotFound: true);
        m_Main_Move = m_Main.FindAction("Move", throwIfNotFound: true);
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

    // Main
    private readonly InputActionMap m_Main;
    private IMainActions m_MainActionsCallbackInterface;
    private readonly InputAction m_Main_Tap;
    private readonly InputAction m_Main_Position;
    private readonly InputAction m_Main_Move;
    public struct MainActions
    {
        private @PlayerControls m_Wrapper;
        public MainActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Tap => m_Wrapper.m_Main_Tap;
        public InputAction @Position => m_Wrapper.m_Main_Position;
        public InputAction @Move => m_Wrapper.m_Main_Move;
        public InputActionMap Get() { return m_Wrapper.m_Main; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MainActions set) { return set.Get(); }
        public void SetCallbacks(IMainActions instance)
        {
            if (m_Wrapper.m_MainActionsCallbackInterface != null)
            {
                @Tap.started -= m_Wrapper.m_MainActionsCallbackInterface.OnTap;
                @Tap.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnTap;
                @Tap.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnTap;
                @Position.started -= m_Wrapper.m_MainActionsCallbackInterface.OnPosition;
                @Position.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnPosition;
                @Position.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnPosition;
                @Move.started -= m_Wrapper.m_MainActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnMove;
            }
            m_Wrapper.m_MainActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Tap.started += instance.OnTap;
                @Tap.performed += instance.OnTap;
                @Tap.canceled += instance.OnTap;
                @Position.started += instance.OnPosition;
                @Position.performed += instance.OnPosition;
                @Position.canceled += instance.OnPosition;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
            }
        }
    }
    public MainActions @Main => new MainActions(this);
    public interface IMainActions
    {
        void OnTap(InputAction.CallbackContext context);
        void OnPosition(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
    }
}
