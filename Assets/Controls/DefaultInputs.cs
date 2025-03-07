//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.11.2
//     from Assets/Controls/DefaultInputs.inputactions
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

public partial class @DefaultInputs: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @DefaultInputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""DefaultInputs"",
    ""maps"": [
        {
            ""name"": ""Tetris"",
            ""id"": ""1bfac93f-c329-4ab9-a5fc-2f1a1ef8a76a"",
            ""actions"": [
                {
                    ""name"": ""Move Left"",
                    ""type"": ""Button"",
                    ""id"": ""911676f6-3b85-48b8-99df-e58e7caf1808"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Move Right"",
                    ""type"": ""Button"",
                    ""id"": ""b1e6729c-4996-4012-aee2-b90f2980caf8"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Move Down"",
                    ""type"": ""Button"",
                    ""id"": ""33ac0fdb-108d-40a9-82d4-ba54ff143c1d"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Rotate Clockwise"",
                    ""type"": ""Button"",
                    ""id"": ""c3c3fc9b-3adf-416d-91c9-12b9b2401609"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Rotate Counterclockwise"",
                    ""type"": ""Button"",
                    ""id"": ""7692b7cb-98df-443b-b495-debd75a37849"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Drop"",
                    ""type"": ""Button"",
                    ""id"": ""024b1aac-4d0e-48b7-ade9-244c4e310daf"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""5ddd792b-17ed-4b97-b17d-a19e128baa23"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""Move Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9cfd72e0-f0e8-4546-ae3a-79f6567b2dad"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""Move Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""023017fc-9b56-4054-b6d1-db0f478e3357"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""Move Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""85c863bc-bc4a-47bd-983e-72cce0c8df75"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""Rotate Counterclockwise"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d0c4309e-3928-44f3-8ddc-02b166ea6ead"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""Rotate Clockwise"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2b57324c-8172-46d9-b480-29aa8f5bd90d"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""Drop"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard&Mouse"",
            ""bindingGroup"": ""Keyboard&Mouse"",
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
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Touch"",
            ""bindingGroup"": ""Touch"",
            ""devices"": [
                {
                    ""devicePath"": ""<Touchscreen>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Joystick"",
            ""bindingGroup"": ""Joystick"",
            ""devices"": [
                {
                    ""devicePath"": ""<Joystick>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""XR"",
            ""bindingGroup"": ""XR"",
            ""devices"": [
                {
                    ""devicePath"": ""<XRController>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Tetris
        m_Tetris = asset.FindActionMap("Tetris", throwIfNotFound: true);
        m_Tetris_MoveLeft = m_Tetris.FindAction("Move Left", throwIfNotFound: true);
        m_Tetris_MoveRight = m_Tetris.FindAction("Move Right", throwIfNotFound: true);
        m_Tetris_MoveDown = m_Tetris.FindAction("Move Down", throwIfNotFound: true);
        m_Tetris_RotateClockwise = m_Tetris.FindAction("Rotate Clockwise", throwIfNotFound: true);
        m_Tetris_RotateCounterclockwise = m_Tetris.FindAction("Rotate Counterclockwise", throwIfNotFound: true);
        m_Tetris_Drop = m_Tetris.FindAction("Drop", throwIfNotFound: true);
    }

    ~@DefaultInputs()
    {
        UnityEngine.Debug.Assert(!m_Tetris.enabled, "This will cause a leak and performance issues, DefaultInputs.Tetris.Disable() has not been called.");
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

    // Tetris
    private readonly InputActionMap m_Tetris;
    private List<ITetrisActions> m_TetrisActionsCallbackInterfaces = new List<ITetrisActions>();
    private readonly InputAction m_Tetris_MoveLeft;
    private readonly InputAction m_Tetris_MoveRight;
    private readonly InputAction m_Tetris_MoveDown;
    private readonly InputAction m_Tetris_RotateClockwise;
    private readonly InputAction m_Tetris_RotateCounterclockwise;
    private readonly InputAction m_Tetris_Drop;
    public struct TetrisActions
    {
        private @DefaultInputs m_Wrapper;
        public TetrisActions(@DefaultInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveLeft => m_Wrapper.m_Tetris_MoveLeft;
        public InputAction @MoveRight => m_Wrapper.m_Tetris_MoveRight;
        public InputAction @MoveDown => m_Wrapper.m_Tetris_MoveDown;
        public InputAction @RotateClockwise => m_Wrapper.m_Tetris_RotateClockwise;
        public InputAction @RotateCounterclockwise => m_Wrapper.m_Tetris_RotateCounterclockwise;
        public InputAction @Drop => m_Wrapper.m_Tetris_Drop;
        public InputActionMap Get() { return m_Wrapper.m_Tetris; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TetrisActions set) { return set.Get(); }
        public void AddCallbacks(ITetrisActions instance)
        {
            if (instance == null || m_Wrapper.m_TetrisActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_TetrisActionsCallbackInterfaces.Add(instance);
            @MoveLeft.started += instance.OnMoveLeft;
            @MoveLeft.performed += instance.OnMoveLeft;
            @MoveLeft.canceled += instance.OnMoveLeft;
            @MoveRight.started += instance.OnMoveRight;
            @MoveRight.performed += instance.OnMoveRight;
            @MoveRight.canceled += instance.OnMoveRight;
            @MoveDown.started += instance.OnMoveDown;
            @MoveDown.performed += instance.OnMoveDown;
            @MoveDown.canceled += instance.OnMoveDown;
            @RotateClockwise.started += instance.OnRotateClockwise;
            @RotateClockwise.performed += instance.OnRotateClockwise;
            @RotateClockwise.canceled += instance.OnRotateClockwise;
            @RotateCounterclockwise.started += instance.OnRotateCounterclockwise;
            @RotateCounterclockwise.performed += instance.OnRotateCounterclockwise;
            @RotateCounterclockwise.canceled += instance.OnRotateCounterclockwise;
            @Drop.started += instance.OnDrop;
            @Drop.performed += instance.OnDrop;
            @Drop.canceled += instance.OnDrop;
        }

        private void UnregisterCallbacks(ITetrisActions instance)
        {
            @MoveLeft.started -= instance.OnMoveLeft;
            @MoveLeft.performed -= instance.OnMoveLeft;
            @MoveLeft.canceled -= instance.OnMoveLeft;
            @MoveRight.started -= instance.OnMoveRight;
            @MoveRight.performed -= instance.OnMoveRight;
            @MoveRight.canceled -= instance.OnMoveRight;
            @MoveDown.started -= instance.OnMoveDown;
            @MoveDown.performed -= instance.OnMoveDown;
            @MoveDown.canceled -= instance.OnMoveDown;
            @RotateClockwise.started -= instance.OnRotateClockwise;
            @RotateClockwise.performed -= instance.OnRotateClockwise;
            @RotateClockwise.canceled -= instance.OnRotateClockwise;
            @RotateCounterclockwise.started -= instance.OnRotateCounterclockwise;
            @RotateCounterclockwise.performed -= instance.OnRotateCounterclockwise;
            @RotateCounterclockwise.canceled -= instance.OnRotateCounterclockwise;
            @Drop.started -= instance.OnDrop;
            @Drop.performed -= instance.OnDrop;
            @Drop.canceled -= instance.OnDrop;
        }

        public void RemoveCallbacks(ITetrisActions instance)
        {
            if (m_Wrapper.m_TetrisActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ITetrisActions instance)
        {
            foreach (var item in m_Wrapper.m_TetrisActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_TetrisActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public TetrisActions @Tetris => new TetrisActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard&Mouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    private int m_TouchSchemeIndex = -1;
    public InputControlScheme TouchScheme
    {
        get
        {
            if (m_TouchSchemeIndex == -1) m_TouchSchemeIndex = asset.FindControlSchemeIndex("Touch");
            return asset.controlSchemes[m_TouchSchemeIndex];
        }
    }
    private int m_JoystickSchemeIndex = -1;
    public InputControlScheme JoystickScheme
    {
        get
        {
            if (m_JoystickSchemeIndex == -1) m_JoystickSchemeIndex = asset.FindControlSchemeIndex("Joystick");
            return asset.controlSchemes[m_JoystickSchemeIndex];
        }
    }
    private int m_XRSchemeIndex = -1;
    public InputControlScheme XRScheme
    {
        get
        {
            if (m_XRSchemeIndex == -1) m_XRSchemeIndex = asset.FindControlSchemeIndex("XR");
            return asset.controlSchemes[m_XRSchemeIndex];
        }
    }
    public interface ITetrisActions
    {
        void OnMoveLeft(InputAction.CallbackContext context);
        void OnMoveRight(InputAction.CallbackContext context);
        void OnMoveDown(InputAction.CallbackContext context);
        void OnRotateClockwise(InputAction.CallbackContext context);
        void OnRotateCounterclockwise(InputAction.CallbackContext context);
        void OnDrop(InputAction.CallbackContext context);
    }
}
