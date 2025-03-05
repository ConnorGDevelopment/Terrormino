using UnityEngine;
using UnityEngine.InputSystem;

namespace Tetris
{
    // Unity is set to generate a C# file for our DefaultInputs ActionMap
    // In addition to implementing the MonoBehaviour interface, we're also implement the DefaultInputs.ITetrisActions
    // DefaultInputs.ITetrisActions corresponds to the Tetris control scheme
    //      Using "I" as a prefix is standard for interfaces, because you'll usually make one that corresponds with a class, in this case "TetrisActions"
    // Whereas MonoBehaviours is filled with a bunch of optional properties and methods we can use, DefaultInputs.ITetrisActions has required methods that we have to use
    // This makes sure we have handlers written for each action in the control scheme: MoveLeft, MoveRight, Drop, RotateCounterclockwise, RotateClockwise

    public class Tetromino : MonoBehaviour, DefaultInputs.ITetrisActions
    {
        public Board Board;
        public Shape Shape;
        public Vector3Int[] Cells;
        public Vector3Int Position;
        public int RotationIndex;

        private float _stepTime;
        private float _moveTime;
        private float _lockTime;

        // Left/Right, Down/Drop, Clockwise/Counterclockwise
        private Vector3Int _moveInput = new Vector3Int(0, 0, 0);

        public DefaultInputs Controls;
        public void OnEnable()
        {
            if (Controls == null)
            {
                Controls = new DefaultInputs();
                // We can make Unity handle hooking up all our OnSomething handlers, because we've promised they're all there by implementing DefaultInputs.ITetrisActions
                Controls.Tetris.SetCallbacks(this);
            }
            Controls.Tetris.Enable();
        }
        public void OnDisable()
        {
            Controls.Tetris.Disable();
        }

        public void Initialize(Board board, Vector3Int position, Shape shape)
        {
            Board = board;
            Position = position;
            Shape = shape;

            RotationIndex = 0;

            _stepTime = Time.time + Board.Config.StepDelay;
            _moveTime = Time.time + Board.Config.MoveDelay;
            _lockTime = 0f;

            // This is a Null-coalescing assignment, if the value on the left is null then it assigns the value on the right
            // It's the same as:
            // if (Cells == null) { Cells = new Vector3Int[Shape.Cells.Length]; }
            Cells ??= new Vector3Int[Shape.Cells.Length];

            Cells = Shape.GetCellsAsVec3;
        }

        public void Update()
        {
            Board.Clear(this);

            // Timer before piece can no longer be moved
            _lockTime += Time.deltaTime;

            if (_moveInput.z != 0)
            {
                Rotate(_moveInput.z);
            }

            if (_moveInput.y == 1)
            {
                Drop();
            }

            if (Time.time > _moveTime)
            {
                HandleMoveInput();
            }

            if (Time.time > _stepTime)
            {
                Step();
            }

            Board.Set(this);

            _moveInput = new(0, 0, 0);
        }

        public void OnMoveDown(InputAction.CallbackContext context)
        {
            if (context.ReadValueAsButton())
            {
                _moveInput = new(0, -1, 0);
            }
        }

        public void OnMoveLeft(InputAction.CallbackContext context)
        {
            if (context.ReadValueAsButton())
            {
                _moveInput = new(-1, 0, 0);
            }
        }

        public void OnMoveRight(InputAction.CallbackContext context)
        {
            if (context.ReadValueAsButton())
            {
                _moveInput = new(1, 0, 0);
            }
        }

        public void OnRotateClockwise(InputAction.CallbackContext context)
        {
            if (context.ReadValueAsButton())
            {
                _moveInput = new(0, 0, 1);
            }
        }

        public void OnRotateCounterclockwise(InputAction.CallbackContext context)
        {
            if (context.ReadValueAsButton())
            {
                _moveInput = new(0, 0, -1);
            }
        }

        public void OnDrop(InputAction.CallbackContext context)
        {
            if (context.ReadValueAsButton())
            {
                _moveInput = new(0, 1, 0);
            }
        }

        private void HandleMoveInput()
        {
            if (_moveInput.y == -1)
            {
                //_stepTime = Time.time + Board.Config.StepDelay;
                Move(new(_moveInput.x, _moveInput.y));
            }

            if (_moveInput.x != 0)
            {
                Move(new(_moveInput.x, _moveInput.y));
            }
        }

        private void Step()
        {
            _stepTime = Time.time + Board.Config.StepDelay;

            Move(Vector2Int.down);

            if (_lockTime >= Board.Config.LockDelay)
            {
                Lock();
            }
        }

        private void Drop()
        {
            while (Move(Vector2Int.down))
            {
                continue;
            }

            Lock();
        }

        private void Lock()
        {
            Board.Set(this);
            Board.ClearLines();
            Board.SpawnTetromino();
        }

        public bool Move(Vector2Int translation)
        {
            Vector3Int newPosition = Position;

            newPosition.x += translation.x;
            newPosition.y += translation.y;

            bool valid = Board.IsValidPosition(this, newPosition);

            if (valid)
            {
                Position = newPosition;
                _moveTime = Time.time + Board.Config.MoveDelay;
                _lockTime = 0f;
            }

            return valid;
        }

        public void Rotate(int direction)
        {
            int originalRotation = RotationIndex;

            RotationIndex = Wrap(RotationIndex + direction, 0, 4);
            ApplyRotationMatrix(direction);

            if (!TestWallKicks(RotationIndex, direction))
            {
                RotationIndex = originalRotation;
                ApplyRotationMatrix(-direction);
            }
        }

        private void ApplyRotationMatrix(int direction)
        {
            float[] matrix = ShapeVecs.RotationMatrix;

            for (int i = 0; i < Cells.Length; i++)
            {
                Vector3 cell = Cells[i];

                int x, y;

                switch (Shape.ShapeKey)
                {
                    case ShapeKeys.I:
                    case ShapeKeys.O:
                        // "I" and "O" are rotated from an offset center point
                        cell.x -= 0.5f;
                        cell.y -= 0.5f;
                        x = Mathf.CeilToInt((cell.x * matrix[0] * direction) + (cell.y * matrix[1] * direction));
                        y = Mathf.CeilToInt((cell.x * matrix[2] * direction) + (cell.y * matrix[3] * direction));
                        break;
                    default:
                        x = Mathf.RoundToInt((cell.x * matrix[0] * direction) + (cell.y * matrix[1] * direction));
                        y = Mathf.RoundToInt((cell.x * matrix[2] * direction) + (cell.y * matrix[3] * direction));
                        break;
                }

                Cells[i] = new Vector3Int(x, y, 0);
            }
        }



        // Fetches an index to a presaved transformation of the shape vector
        private int GetWallKickIndex(int rotationIndex, int rotationDirection)
        {
            int wallKickIndex = rotationIndex * 2;

            if (rotationDirection < 0)
            {
                wallKickIndex--;
            }

            return Wrap(wallKickIndex, 0, Shape.WallKicks.GetLength(0));
        }

        private bool TestWallKicks(int rotationIndex, int direction)
        {
            int wallKickIndex = GetWallKickIndex(rotationIndex, direction);

            for (int i = 0; i < Shape.WallKicks.GetLength(1); i++)
            {
                Vector2Int translation = Shape.WallKicks[wallKickIndex, i];

                if (Move(translation))
                {
                    return true;
                }
            }

            return false;
        }

        // If value is greater than max, cap it at max
        // If value is less than min, cap it at min
        private static int Wrap(int input, int min, int max)
        {
            if (input < min)
            {
                return max - (min - input) % (max - min);
            }
            else
            {
                return min + (input - min) % (max - min);
            }
        }
    }

}