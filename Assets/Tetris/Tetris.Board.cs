using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

namespace Tetris
{
    [DefaultExecutionOrder(-1)]
    public class Board : MonoBehaviour
    {
        public Tilemap BoardTilemap;
        public Shape[] Tetrominoes;
        public ActivePieceController ActivePiece;
        public Config Config;
        public Vector2Int BoardSize = new(10, 20);
        public RectInt BoardBounds
        {
            get
            {
                return new RectInt(new Vector2Int(-BoardSize.x / 2, -BoardSize.y / 2), BoardSize);
            }
        }
        public Vector3Int SpawnPosition = new(-1, 8, 0);

        public void Awake()
        {
            for (int i = 0; i < Tetrominoes.Length; i++)
            {
                Tetrominoes[i].Initialize();
            }
        }


        public void Start()
        {
            BoardTilemap = Helpers.Debug.TryFindComponentOnGameObjectByName<Tilemap>("BoardTilemap");
            ActivePiece = Helpers.Debug.TryFindComponent<ActivePieceController>(gameObject);
            GameOver.AddListener(OnGameOver);

            SpawnPiece();
        }


        public void SpawnPiece()
        {
            // Currently picks randomly from all potential shapes, could use weighting
            int random = Random.Range(0, Tetrominoes.Length);
            Shape shape = Tetrominoes[random];

            // Pass to the PieceController component
            ActivePiece.Initialize(this, SpawnPosition, shape);

            // If the stack is too high that the new piece can't be legally spawned, game ends
            if (IsValidPosition(ActivePiece.Cells, SpawnPosition))
            {
                PaintTiles(ActivePiece);
            }
            else
            {
                GameOver.Invoke();
            }
        }

        public UnityEvent GameOver = new();
        public void OnGameOver()
        {
            BoardTilemap.ClearAllTiles();
        }

        public void PaintTiles(ActivePieceController tetromino)
        {
            for (int i = 0; i < tetromino.Cells.Length; i++)
            {
                Vector3Int tilePosition = tetromino.Cells[i] + tetromino.Position;
                BoardTilemap.SetTile(tilePosition, tetromino.Shape.Tile);
            }
        }
        public void UnpaintTiles(ActivePieceController tetromino)
        {
            for (int i = 0; i < tetromino.Cells.Length; i++)
            {
                Vector3Int tilePosition = tetromino.Cells[i] + tetromino.Position;
                BoardTilemap.SetTile(tilePosition, null);
            }
        }


        public bool IsValidPosition(Vector3Int[] cells, Vector3Int position)
        {
            // Validate each cell position
            for (int i = 0; i < cells.Length; i++)
            {
                Vector3Int tilePosition = cells[i] + position;

                if (!BoardBounds.Contains(new(tilePosition.x, tilePosition.y)))
                {
                    return false;
                }


                if (BoardTilemap.HasTile(tilePosition))
                {
                    return false;
                }
            }
            return true;
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
                else
                {
                    row++;
                }
            }
        }
    }
}
