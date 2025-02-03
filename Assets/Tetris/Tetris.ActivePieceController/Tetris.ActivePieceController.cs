using UnityEngine;
using UnityEngine.Events;

namespace Tetris
{
    // Unity is set to generate a C# file for our DefaultInputs ActionMap
    // In addition to implementing the MonoBehaviour interface, we're also implement the DefaultInputs.ITetrisActions
    // DefaultInputs.ITetrisActions corresponds to the Tetris control scheme
    //      Using "I" as a prefix is standard for interfaces, because you'll usually make one that corresponds with a class, in this case "TetrisActions"
    // Whereas MonoBehaviours is filled with a bunch of optional properties and methods we can use, DefaultInputs.ITetrisActions has required methods that we have to use
    // This makes sure we have handlers written for each action in the control scheme: MoveLeft, MoveRight, Drop, RotateCounterclockwise, RotateClockwise

    public partial class ActivePieceController : MonoBehaviour
    {
        public Board Board;
        public Shape Shape;
        public Vector3Int[] Cells;
        public Vector3Int Position;
        public int RotationIndex;

        private float _stepTime;
        // TODO: Reimplement repeated movement handling, potentially use hold action on action map
        //private float _moveTime;
        private float _lockTime;

        public UnityEvent<Vector2Int> Move;
        public UnityEvent<int> Rotate;

        public void Start()
        {
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

            OnMove(Vector2Int.down);

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

        public void OnMove(Vector2Int moveInput)
        {
            Vector3Int newPosition = Position;

            newPosition.x += moveInput.x;
            newPosition.y += moveInput.y;

            if (Board.IsValidPosition(this, newPosition))
            {
                Position = newPosition;
                //_moveTime = Time.time + Board.Config.MoveDelay;
                _lockTime = 0f;
            }
        }

        public void OnRotate(int rotateInput)
        {
            // Stores original rotation as fallback
            int originalRotation = RotationIndex;

            // Use predefined matrix to do rotations
            RotationIndex = Wrap(RotationIndex + rotateInput, 0, 4);
            ApplyRotationMatrix(rotateInput);

            // If the rotation + wall kick fails, revert the rotation
            if (!TestWallKicks(RotationIndex, rotateInput))
            {
                RotationIndex = originalRotation;
                ApplyRotationMatrix(-rotateInput);
            }
        }
    }
}