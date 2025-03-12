using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tetris2
{
    public class Board
    {
        public Tilemap BoardTilemap;

        public Vector2Int BoardSize = new(10, 20);
        public Vector3Int SpawnPosition = new(-1, 8, 0);


    }
}