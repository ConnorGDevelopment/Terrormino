using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tetris
{
    public enum TetrominoShapes
    {
        I,
        O,
        T,
        J,
        L,
        S,
        Z
    }

    public static class TetrominoVectors
    {
        public static readonly float cos = Mathf.Cos(Mathf.PI / 2f);
        public static readonly float sin = Mathf.Sin(Mathf.PI / 2f);
        public static readonly float[] RotationMatrix = new float[] { cos, sin, -sin, cos };

        public static readonly Dictionary<TetrominoShapes, Vector2Int[]> Cells = new Dictionary<TetrominoShapes, Vector2Int[]>() {
            { TetrominoShapes.I, new Vector2Int[] { new Vector2Int(-1, 1), new Vector2Int( 0, 1), new Vector2Int( 1, 1), new Vector2Int( 2, 1) } },
            { TetrominoShapes.J, new Vector2Int[] { new Vector2Int(-1, 1), new Vector2Int(-1, 0), new Vector2Int( 0, 0), new Vector2Int( 1, 0) } },
            { TetrominoShapes.L, new Vector2Int[] { new Vector2Int( 1, 1), new Vector2Int(-1, 0), new Vector2Int( 0, 0), new Vector2Int( 1, 0) } },
            { TetrominoShapes.O, new Vector2Int[] { new Vector2Int( 0, 1), new Vector2Int( 1, 1), new Vector2Int( 0, 0), new Vector2Int( 1, 0) } },
            { TetrominoShapes.S, new Vector2Int[] { new Vector2Int( 0, 1), new Vector2Int( 1, 1), new Vector2Int(-1, 0), new Vector2Int( 0, 0) } },
            { TetrominoShapes.T, new Vector2Int[] { new Vector2Int( 0, 1), new Vector2Int(-1, 0), new Vector2Int( 0, 0), new Vector2Int( 1, 0) } },
            { TetrominoShapes.Z, new Vector2Int[] { new Vector2Int(-1, 1), new Vector2Int( 0, 1), new Vector2Int( 0, 0), new Vector2Int( 1, 0) } },
        };

        private static readonly Vector2Int[,] _wallKicksI = new Vector2Int[,] {
            { new Vector2Int(0, 0), new Vector2Int(-2, 0), new Vector2Int( 1, 0), new Vector2Int(-2,-1), new Vector2Int( 1, 2) },
            { new Vector2Int(0, 0), new Vector2Int( 2, 0), new Vector2Int(-1, 0), new Vector2Int( 2, 1), new Vector2Int(-1,-2) },
            { new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int( 2, 0), new Vector2Int(-1, 2), new Vector2Int( 2,-1) },
            { new Vector2Int(0, 0), new Vector2Int( 1, 0), new Vector2Int(-2, 0), new Vector2Int( 1,-2), new Vector2Int(-2, 1) },
            { new Vector2Int(0, 0), new Vector2Int( 2, 0), new Vector2Int(-1, 0), new Vector2Int( 2, 1), new Vector2Int(-1,-2) },
            { new Vector2Int(0, 0), new Vector2Int(-2, 0), new Vector2Int( 1, 0), new Vector2Int(-2,-1), new Vector2Int( 1, 2) },
            { new Vector2Int(0, 0), new Vector2Int( 1, 0), new Vector2Int(-2, 0), new Vector2Int( 1,-2), new Vector2Int(-2, 1) },
            { new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int( 2, 0), new Vector2Int(-1, 2), new Vector2Int( 2,-1) },
        };

        private static readonly Vector2Int[,] _wallKicksJLOSTZ = new Vector2Int[,] {
            { new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, 1), new Vector2Int(0,-2), new Vector2Int(-1,-2) },
            { new Vector2Int(0, 0), new Vector2Int( 1, 0), new Vector2Int( 1,-1), new Vector2Int(0, 2), new Vector2Int( 1, 2) },
            { new Vector2Int(0, 0), new Vector2Int( 1, 0), new Vector2Int( 1,-1), new Vector2Int(0, 2), new Vector2Int( 1, 2) },
            { new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, 1), new Vector2Int(0,-2), new Vector2Int(-1,-2) },
            { new Vector2Int(0, 0), new Vector2Int( 1, 0), new Vector2Int( 1, 1), new Vector2Int(0,-2), new Vector2Int( 1,-2) },
            { new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1,-1), new Vector2Int(0, 2), new Vector2Int(-1, 2) },
            { new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1,-1), new Vector2Int(0, 2), new Vector2Int(-1, 2) },
            { new Vector2Int(0, 0), new Vector2Int( 1, 0), new Vector2Int( 1, 1), new Vector2Int(0,-2), new Vector2Int( 1,-2) },
        };

        public static readonly Dictionary<TetrominoShapes, Vector2Int[,]> WallKicks = new Dictionary<TetrominoShapes, Vector2Int[,]>(){
            { TetrominoShapes.I, _wallKicksI },
            { TetrominoShapes.J, _wallKicksJLOSTZ },
            { TetrominoShapes.L, _wallKicksJLOSTZ },
            { TetrominoShapes.O, _wallKicksJLOSTZ },
            { TetrominoShapes.S, _wallKicksJLOSTZ },
            { TetrominoShapes.T, _wallKicksJLOSTZ },
            { TetrominoShapes.Z, _wallKicksJLOSTZ },
        };
    };

    public struct TetrominoData
    {
        public TetrominoShapes Tetromino;
        public Tile Tile;
        public Vector2Int[] Cells;
    }

    public class Tetromino : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}