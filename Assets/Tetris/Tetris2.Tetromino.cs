using System.Collections.Generic;
using Tetris;
using UnityEngine;

namespace Tetris2
{
    public enum TetrominoNames
    {
        I,
        /*   []
             []
             []
             []   */
        J,
        /*   []
             []
           [][]   */
        L,
        /*   []
             [] 
             [][] */
        O,
        /* [][]
           [][]   */
        S,
        /*   [][]
           [][]   */
        T,
        /* [][][]
             []   */
        Z
        /* [][]
             [][] */
    }

    [System.Serializable]
    public class Tetromino
    {
        public static readonly Dictionary<ShapeKeys, Vector2Int[]> Cells = new Dictionary<ShapeKeys, Vector2Int[]>() {
            { ShapeKeys.I, new Vector2Int[] { new Vector2Int(-1, 1), new Vector2Int( 0, 1), new Vector2Int( 1, 1), new Vector2Int( 2, 1) } },
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
    }
}