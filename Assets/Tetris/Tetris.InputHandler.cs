using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace Tetris
{
    public class InputHandler : MonoBehaviour, DefaultInputs.ITetrisActions
    {
        private DefaultInputs _tetrisActions;
        // These should be hooked into the grab interactable on either side
        // Handlers basically check if their side is grabbed before passing the inputs
        public bool IsDpadSideGrabbed;
        public bool IsButtonSideGrabbed;

        public ActivePieceController ActivePiece;

        public void Start()
        {
            if (ActivePiece == null)
            {
                if (GameObject.Find("Board").TryGetComponent(out ActivePieceController activePiece))
                {
                    ActivePiece = activePiece;
                }
                else
                {
                    Debug.Log("The Tetris Input Handler could not locate the the ActivePieceController Board");
                }
            }
        }

        public static int RoundToNearestNonZero(float val)
        {
            // Round any positive value to 1, any negative value to -1
            return val > 0 ? 1 : val < 0 ? -1 : 0;
        }

        public void OnDpadSideGrabbed(SelectEnterEventArgs ctx)
        {
            IsDpadSideGrabbed = true;
        }
        public void OnDpadSideRelease(SelectExitEventArgs ctx)
        {
            IsDpadSideGrabbed = false;
        }

        public void OnButtonSideGrabbed(SelectEnterEventArgs ctx)
        {
            IsButtonSideGrabbed = true;
        }
        public void OnButtonSideRelease(SelectExitEventArgs ctx)
        {
            IsButtonSideGrabbed = false;
        }

        // ctx.action.WasPerformedThisFrame() checks if the input given is new or stale
        public void OnMove(InputAction.CallbackContext ctx)
        {
            if (ctx.action.WasPerformedThisFrame() && IsDpadSideGrabbed)
            {
                ActivePiece.Move.Invoke(new(
                                RoundToNearestNonZero(ctx.ReadValue<Vector2>().x),
                                // Down should either be 0 or -1, never 1 (can't move pieces upward)
                                Mathf.Min(
                                    RoundToNearestNonZero(ctx.ReadValue<Vector2>().y),
                                    0
                                )
                            ));
            }
        }

        public void OnRotate(InputAction.CallbackContext ctx)
        {
            if (ctx.action.WasPerformedThisFrame() && IsButtonSideGrabbed)
            {
                ActivePiece.Rotate.Invoke(RoundToNearestNonZero(ctx.ReadValue<float>()));
            }
        }

        public void OnDrop(InputAction.CallbackContext context)
        {
            // TODO: Implement OnDrop
            Debug.Log("OnDrop Not Actually Implemented");
        }
    }
}