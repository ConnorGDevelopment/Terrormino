using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace TerrorminoControls
{
    [Serializable]
    public class InputRoute
    {
        public InputActionReference m_inputAction;
        public List<UnityEvent<object>> Outputs = new();
    }

    public class InputRouter : MonoBehaviour
    {
        public GameObject watchGrabTarget;
        public bool IsGrabbed { get; private set; }

        public UnityEvent GrabStateChanged;

        public void ToggleGrab(bool grabbed)
        {
            IsGrabbed = grabbed;
            GrabStateChanged.Invoke();
        }

        public List<InputRoute> routes = new();
        public InputActionMap m_actionMap;

        public void Start()
        {
            DebugHelpers.CheckIfSetInInspector(gameObject, watchGrabTarget, "Watch Grab Target");
        }

        public void Update()
        {
            // Prefilter this to only care about actions that are featured on a route
            foreach (var action in m_actionMap)
            {
                if (action.WasPerformedThisFrame())
                {
                    var matchedOutputs = routes.FindAll((route) => route.m_inputAction.action == action).SelectMany((route) => route.Outputs);

                    foreach (var output in matchedOutputs)
                    {
                        output.Invoke(action.ReadValueAsObject());
                    }
                }
            }

        }

    }
}