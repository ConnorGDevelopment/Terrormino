using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tetris
{
    [System.Serializable]
    public struct Config
    {
        public float StepDelay;
        public float MoveDelay;
        public float LockDelay;
    }

    [DefaultExecutionOrder(-1)]
    public class Board : MonoBehaviour
    {
        public Tilemap BoardTilemap;
        public Vector2Int BoardSize = new(10, 20);
        public RectInt BoardBounds
        {
            get
            {
                return new RectInt(new Vector2Int(-BoardSize.x / 2, -BoardSize.y / 2), BoardSize);
            }
        }
        public Vector3Int SpawnPosition = new(-1, 8, 0);

        public Shape[] Tetrominoes;
        public Tetromino ActiveTetromino;
        public Config Config;

        public void Awake()
        {
            for (int i = 0; i < Tetrominoes.Length; i++)
            {
                Tetrominoes[i].Initialize();
            }
        }

        public void Start()
        {
            if (BoardTilemap == null)
            {
                if (GameObject.Find("BoardTilemap").TryGetComponent(out Tilemap tetrisBoard))
                {
                    BoardTilemap = tetrisBoard;
                }
                else
                {
                    Debug.Log("The Tetris Board was not set in the Inspector and could not be found in Scene");
                }
            }

            ActiveTetromino = GetComponentInChildren<Tetromino>();

            SpawnTetromino();
        }

        public void SpawnTetromino()
        {
            int random = Random.Range(0, Tetrominoes.Length);
            Shape shape = Tetrominoes[random];

            ActiveTetromino.Initialize(this, SpawnPosition, shape);

            if (IsValidPosition(ActiveTetromino, SpawnPosition))
            {
                Set(ActiveTetromino);
            }
            else
            {
                GameOver();
            }
        }

        public void GameOver()
        {
            BoardTilemap.ClearAllTiles();
        }

        public void Set(Tetromino tetromino)
        {
            for (int i = 0; i < tetromino.Cells.Length; i++)
            {
                Vector3Int tilePosition = tetromino.Cells[i] + tetromino.Position;
                BoardTilemap.SetTile(tilePosition, tetromino.Shape.Tile);
            }
        }

        public void Clear(Tetromino tetromino)
        {
            for (int i = 0; i < tetromino.Cells.Length; i++)
            {
                Vector3Int tilePosition = tetromino.Cells[i] + tetromino.Position;
                BoardTilemap.SetTile(tilePosition, null);
            }
        }

        public bool IsValidPosition(Tetromino tetromino, Vector3Int position)
        {
            RectInt bounds = BoardBounds;

            // Validate each cell position
            for (int i = 0; i < tetromino.Cells.Length; i++)
            {
                Vector3Int tilePosition = tetromino.Cells[i] + position;

                if (!bounds.Contains(new(tilePosition.x, tilePosition.y)))
                {
                    return false;
                }

                if (BoardTilemap.HasTile(tilePosition))
                {
                    return false;
                }
            }

            // This only gets called if "return false" wasn't called above
            return true;
        }

        public void ClearLines()
        {
            RectInt bounds = BoardBounds;
            int row = bounds.yMin;

            // Clear from bottom to top
            while (row < bounds.yMax)
            {
                // Only advance to the next row if the current is not cleared
                // because the tiles above will fall down when a row is cleared
                if (IsLineFull(row))
                {
                    LineClear(row);
                }
                else
                {
                    row++;
                }
            }
        }

        public bool IsLineFull(int row)
        {
            RectInt bounds = BoardBounds;

            // Iterate through each column, if any are missing then is not full
            for (int col = bounds.xMin; col < bounds.xMax; col++)
            {
                Vector3Int position = new Vector3Int(col, row, 0);

                if (!BoardTilemap.HasTile(position))
                {
                    return false;
                }
            }

            return true;
        }

        public void LineClear(int row)
        {
            RectInt bounds = BoardBounds;

            // Clear all tiles in the row
            for (int col = bounds.xMin; col < bounds.xMax; col++)
            {
                Vector3Int position = new Vector3Int(col, row, 0);
                BoardTilemap.SetTile(position, null);
            }

            // Shift every row above down one
            while (row < bounds.yMax)
            {
                for (int col = bounds.xMin; col < bounds.xMax; col++)
                {
                    Vector3Int position = new Vector3Int(col, row + 1, 0);
                    TileBase above = BoardTilemap.GetTile(position);

                    position = new Vector3Int(col, row, 0);
                    BoardTilemap.SetTile(position, above);
                }

                row++;
            }
        }
    }
}