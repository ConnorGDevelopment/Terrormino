using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace TerrorminoControls
{
    [Serializable]
    public class InputRoute
    {
        public InputActionReference ActionRef;
        public List<UnityEvent<InputAction>> Outputs = new();
    }

    public class InputRouter : MonoBehaviour
    {
        public InputActionAsset ActionMap;
        public List<InputRoute> Routes = new();
        private List<InputActionReference> _aggregatedActionRefs
        {
            get
            {
                List<InputActionReference> aggregatedActionRefs = new();

                Routes.ForEach(route =>
                {
                    if (!aggregatedActionRefs.Exists(actionRef => actionRef.action.id == route.ActionRef.action.id))
                    {
                        aggregatedActionRefs.Add(route.ActionRef);
                    }
                });

                return aggregatedActionRefs;
            }
        }
        private Dictionary<Guid, List<UnityEvent<InputAction>>> _aggregatedOutputs
        {
            get
            {
                // This flattens the Routes property
                // Uses each action's unique id as key and has list of all outputs attached to that action
                Dictionary<Guid, List<UnityEvent<InputAction>>> aggregatedRoutes = new();

                Routes.ForEach(route =>
                {
                    // Typical add these values to the list if there is a key, otherwise add new key
                    if (aggregatedRoutes.ContainsKey(route.ActionRef.action.id))
                    {
                        aggregatedRoutes[route.ActionRef.action.id].AddRange(route.Outputs);
                    }
                    else
                    {
                        aggregatedRoutes.Add(route.ActionRef.action.id, route.Outputs);
                    }
                });

                return aggregatedRoutes;
            }
        }

        private bool _isGrabbed = false;
        public void OnSelectEnter(SelectEnterEventArgs _)
        {
            _isGrabbed = true;
        }
        public void OnSelectExit(SelectExitEventArgs _)
        {
            _isGrabbed = false;
        }

        public void Update()
        {
            if (_isGrabbed)
            {
                _aggregatedActionRefs.ForEach(actionRef =>
                {
                    if (actionRef.action.WasPerformedThisFrame())
                    {
                        _aggregatedOutputs[actionRef.action.id].ForEach(output => output.Invoke(actionRef.action));
                    }
                });
            }
        }
    }
}