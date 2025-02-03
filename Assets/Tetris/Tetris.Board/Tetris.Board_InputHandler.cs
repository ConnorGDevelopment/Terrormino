using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace Tetris
{
    public partial class Board
    {
        public static int RoundToNearestNonZero(float val)
        {
            // Round any positive value to 1, any negative value to -1
            return val > 0 ? 1 : val < 0 ? -1 : 0;
        }

        // These should be hooked into the grab interactable on either side
        // Handlers basically check if their side is grabbed before passing the inputs
        public bool IsDpadSideGrabbed;
        public bool IsButtonSideGrabbed;

        public void OnDpadSideGrabbed(SelectEnterEvent ctx)
        {
            IsDpadSideGrabbed = true;
        }
        public void OnDpadSideRelease(SelectExitEventArgs ctx)
        {
            IsDpadSideGrabbed = false;
        }

        public void OnButtonSideGrabbed(SelectEnterEvent ctx)
        {
            IsButtonSideGrabbed = true;
        }
        public void OnButtonSideRelease(SelectExitEventArgs ctx)
        {
            IsButtonSideGrabbed = false;
        }

        // ctx.action.WasPerformedThisFrame() checks if the input given is new or stale
        public void HandleMove(InputAction.CallbackContext ctx)
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
            // TODO: Handle fast drop via holding down input
        }

        public void HandleRotate(InputAction.CallbackContext ctx)
        {
            if (ctx.action.WasPerformedThisFrame() && IsButtonSideGrabbed)
            {
                ActivePiece.Rotate.Invoke(RoundToNearestNonZero(ctx.ReadValue<float>()));
            }
        }
    }
}

