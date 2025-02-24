using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Tetris
{
    public class InputHandler : MonoBehaviour
    {
        public UnityEvent<InputAction> Move = new();
        public UnityEvent<InputAction> Rotate = new();

        public Vector2Int MoveInput;
        public int RotateInput;

        public void OnMove(InputAction inputAction)
        {
            var moveInput = inputAction.ReadValue<Vector2>();
            MoveInput.x = Helpers.Math.RoundNearestNonZeroInt(moveInput.x);
            MoveInput.y = Helpers.Math.RoundNearestNonZeroInt(moveInput.y);
        }
        public void OnRotate(InputAction inputAction)
        {
            var rotateInput = inputAction.ReadValue<float>();
            RotateInput = Helpers.Math.RoundNearestNonZeroInt(rotateInput);
        }

        public void Start()
        {
            Move.AddListener(OnMove);
            Rotate.AddListener(OnRotate);
        }
    }
}