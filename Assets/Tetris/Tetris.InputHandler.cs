using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace Tetris {
    public class InputHandler : MonoBehaviour, DefaultInputs.ITetrisActions
    {
        // XR Input Controls
        // https://docs.unity3d.com/Manual/xr_input.html

        // Weapon File from SnakeEyes
        // https://github.com/ConnorGDevelopment/OU-SnakeEyes-2024/blob/main/Assets/Combat/Combat.Weapon.cs

        // Weapon Event Handler from SnakeEyes
        // https://github.com/ConnorGDevelopment/OU-SnakeEyes-2024/blob/main/Assets/Combat/Combat.ControllerEventHandler.cs

        public List<GameObject> HeldObjects = new();
        

        public void OnDrop(InputAction.CallbackContext ctx)
        {
            throw new System.NotImplementedException();
        }

        public void OnMove(InputAction.CallbackContext ctx)
        {
            throw new System.NotImplementedException();
        }

        public void OnRotate(InputAction.CallbackContext ctx)
        {
            throw new System.NotImplementedException();
        }
    }
}

