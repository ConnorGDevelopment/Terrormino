using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tetris2
{
    public class Board
    {
        public Tilemap BoardTilemap;

        public Vector2Int BoardSize = new(10, 20);
        public RectInt Bounds
        {
            get
            {
                Vector2Int position = new Vector2Int(-BoardSize.x / 2, -BoardSize.y / 2);
                return new RectInt(position, BoardSize);
            }
        }

        public Vector3Int SpawnPosition = new(-1, 8, 0);
    }
}