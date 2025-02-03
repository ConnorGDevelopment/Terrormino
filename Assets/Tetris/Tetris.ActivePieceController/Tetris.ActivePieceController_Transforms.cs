using UnityEngine;

namespace Tetris
{
    public partial class ActivePieceController
    {
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

        // See Wall Kick: https://tetris.wiki/Super_Rotation_System#Wall_Kicks
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
                Vector2Int wallKickTranslation = Shape.WallKicks[wallKickIndex, i];

                Vector3Int newPosition = Position;

                newPosition.x += wallKickTranslation.x;
                newPosition.y += wallKickTranslation.y;

                if (Board.IsValidPosition(this, newPosition))
                {
                    Position = newPosition;
                    //_moveTime = Time.time + Board.Config.MoveDelay;
                    _lockTime = 0f;
                    return true;
                }
            }

            return false;
        }
    }
}