using System;
using System.Collections.Generic;
using System.Linq;
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
        public List<UnityEvent<object>> Outputs = new();
    }

    public class InputRouter : MonoBehaviour
    {
        public InputActionMap ActionMap;
        public bool IsGrabbed = false;

        public List<InputRoute> Routes = new();

        public void OnSelectEnter(SelectEnterEventArgs _)
        {
            IsGrabbed = true;
        }
        public void OnSelectExit(SelectExitEventArgs _)
        {
            IsGrabbed = false;
        }

        public void Update()
        {
            if (IsGrabbed)
            {
                // Take list of routes and get the list of actions we actually care about
                Routes.Select(route => route.ActionRef.action)
               .ToList()
               .ForEach(action =>
               {
                   // Check if each of those action was performed the frame
                   if (action.WasPerformedThisFrame())
                   {
                       // Filter routes by the action we're currently checking
                       Routes.FindAll(route => route.ActionRef.action == action)
                           .ToList()
                           // For each route, invoke each output with action value
                           .ForEach(route => route.Outputs.ForEach(output => output.Invoke(action.ReadValueAsObject())));
                   }
               });
            }
        }
    }
}