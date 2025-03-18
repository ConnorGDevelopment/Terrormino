using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Tetris
{
<<<<<<< HEAD
    public partial class ActivePieceController : MonoBehaviour
=======
    public class ActivePieceController : MonoBehaviour
>>>>>>> parent of 635a200 (Revert "Merge pull request #5 from ConnorGDevelopment/tetris-input-handler")
    {
        public Board Board;
        public Shape Shape;
        public Vector3Int[] Cells;
        public Vector3Int Position;
        public int RotationIndex;

        public float _stepTime;
        // TODO: Reimplement repeated movement handling, potentially use hold action on action map
        private float _moveTime;
        public float _lockTime;

        public UnityEvent<InputAction> Move = new();
        public UnityEvent<InputAction> Rotate = new();

        public void CommitPlayerTransform(Vector3Int position, Vector3Int[] cells)
        {
            Cells = cells;
            Position = position;
            _moveTime = Time.time;
            _lockTime = 0f;
        }

        public void OnMove(InputAction inputAction)
        {
            Vector2Int moveInput = new(
                Helpers.Math.RoundNearestNonZeroInt(inputAction.ReadValue<Vector2>().x),
                Helpers.Math.RoundNearestNonZeroInt(inputAction.ReadValue<Vector2>().y)
            );

<<<<<<< HEAD
            var newPosition = ValidateMove(moveInput, Cells);
=======
            var newPosition = TryMove(moveInput, Cells);
>>>>>>> parent of 635a200 (Revert "Merge pull request #5 from ConnorGDevelopment/tetris-input-handler")

            if (newPosition != null)
            {
                CommitPlayerTransform((Vector3Int)newPosition, Cells);
            }
        }
        public void OnRotate(InputAction inputAction)
        {
            int rotateInput = Helpers.Math.RoundNearestNonZeroInt(inputAction.ReadValue<float>());

            Vector3Int[] newCells = GenerateRotationCells(Helpers.Math.Wrap(RotationIndex + rotateInput, 0, 4));

<<<<<<< HEAD
            var newPosition = ValidateRotate(rotateInput, newCells);
=======
            var newPosition = TryRotate(rotateInput, newCells);
>>>>>>> parent of 635a200 (Revert "Merge pull request #5 from ConnorGDevelopment/tetris-input-handler")

            if (newPosition != null)
            {
                CommitPlayerTransform((Vector3Int)newPosition, newCells);
            }
        }


<<<<<<< HEAD
        private Vector3Int? ValidateMove(Vector2Int moveInput, Vector3Int[] cells)
=======
        private Vector3Int? TryMove(Vector2Int moveInput, Vector3Int[] cells)
>>>>>>> parent of 635a200 (Revert "Merge pull request #5 from ConnorGDevelopment/tetris-input-handler")
        {
            Vector3Int newPosition = Position;

            newPosition.x += moveInput.x;
            newPosition.y += moveInput.y;

            // Return the newPosition if valid, otherwise just pass back original
            // Removes the weird bool check in original and avoids a null return
            return Board.IsValidPosition(cells, newPosition) ? newPosition : null;
        }
<<<<<<< HEAD
        private Vector3Int? ValidateRotate(int rotateInput, Vector3Int[] cells)
=======
        private Vector3Int? TryRotate(int rotateInput, Vector3Int[] cells)
>>>>>>> parent of 635a200 (Revert "Merge pull request #5 from ConnorGDevelopment/tetris-input-handler")
        {
            // See Wall Kick: https://tetris.wiki/Super_Rotation_System#Wall_Kicks
            // Fetches an index to a presaved transformation of the shape vector

            int wallKickIndex = Helpers.Math.Wrap(
                // Add input to existing and multiply by 2, if rotateInput is negative then subtract 1
                // The array for wall kicks basically has 2 vector arrays for each orientation, clockwise and counterclockwise
                ((rotateInput + RotationIndex) * 2) - (rotateInput < 0 ? 1 : 0),
                0,
                Shape.WallKicks.GetLength(0)
            );

<<<<<<< HEAD
=======
            
>>>>>>> parent of 635a200 (Revert "Merge pull request #5 from ConnorGDevelopment/tetris-input-handler")
            for (int i = 0; i < Shape.WallKicks.GetLength(1); i++)
            {
                Vector2Int wallKickMoveInput = Shape.WallKicks[wallKickIndex, i];

<<<<<<< HEAD
                if (ValidateMove(wallKickMoveInput, cells) != null)
                {
                    return new(
                        Position.x + wallKickMoveInput.x,
                        Position.y + wallKickMoveInput.y,
                        Position.z
                    );
=======
                if (TryMove(wallKickMoveInput, cells) != null)
                {
                    return TryMove(wallKickMoveInput, cells);
>>>>>>> parent of 635a200 (Revert "Merge pull request #5 from ConnorGDevelopment/tetris-input-handler")
                }
            }

            return null;
        }
        private Vector3Int[] GenerateRotationCells(int rotateInput)
        {
            // Makes an non-reference copy of the array
            Vector3Int[] newCells = new List<Vector3Int>(Cells).ToArray();

            float[] matrix = ShapeVecs.RotationMatrix;

            for (int i = 0; i < newCells.Length; i++)
            {
                Vector3 cell = newCells[i];

                int x;
                int y;

                switch (Shape.ShapeKey)
                {
                    case ShapeKeys.I:
                    case ShapeKeys.O:
                        // "I" and "O" are rotated from an offset center point
                        cell.x -= 0.5f;
                        cell.y -= 0.5f;
                        x = Mathf.CeilToInt((cell.x * matrix[0] * rotateInput) + (cell.y * matrix[1] * rotateInput));
                        y = Mathf.CeilToInt((cell.x * matrix[2] * rotateInput) + (cell.y * matrix[3] * rotateInput));
                        break;
                    default:
                        x = Mathf.RoundToInt((cell.x * matrix[0] * rotateInput) + (cell.y * matrix[1] * rotateInput));
                        y = Mathf.RoundToInt((cell.x * matrix[2] * rotateInput) + (cell.y * matrix[3] * rotateInput));
                        break;
                }

                newCells[i] = new Vector3Int(x, y, 0);
            }

            return newCells;
        }

        public void Start()
        {
            Helpers.Debug.CheckIfSetInInspector(gameObject, Board, "Board");

            Move.AddListener(OnMove);
            Rotate.AddListener(OnRotate);
        }

        public void Initialize(Board board, Vector3Int position, Shape shape)
        {
            Board = board;
            Position = position;
            Shape = shape;

            RotationIndex = 0;

            _stepTime = Time.time + Board.Config.StepDelay;
            //_moveTime = Time.time + Board.Config.MoveDelay;
            _lockTime = 0f;

            // This is a Null-coalescing assignment, if the value on the left is null then it assigns the value on the right
            // It's the same as:
            // if (Cells == null) { Cells = new Vector3Int[Shape.Cells.Length]; }
            Cells ??= new Vector3Int[Shape.Cells.Length];

            Cells = Shape.GetCellsAsVec3;
        }

        public void Update()
        {
            Board.UnpaintTiles(this);

            // Timer before piece can no longer be moved
            _lockTime += Time.deltaTime;

            if (Time.time > _stepTime)
            {
                Step();
            }

            Board.PaintTiles(this);
        }

        private void Step()
        {
            _stepTime = Time.time + Board.Config.StepDelay;

            var newPosition = ValidateMove(Vector2Int.down, Cells);
            if (newPosition != null)
            {
                Position = (Vector3Int)newPosition;
            }

            if (_lockTime >= Board.Config.LockDelay)
            {
                LockMovement();
            }
        }

        private void LockMovement()
        {
            Board.PaintTiles(this);
            Board.ClearLines();
            Board.SpawnPiece();
        }
    }
}