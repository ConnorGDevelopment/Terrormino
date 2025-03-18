using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tetris2
{
    public enum ShapeName
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
        public ShapeName ShapeName;
        public Tile Tile;
        public static Dictionary<ShapeName, List<Vector2Int[,]>> WallKickVecs = new()
        {
            // Use the SRS.xlsx in the Docs folder, it shows these visually and generates the code
            {
                ShapeName.I,
                new List<Vector2Int[,]>() {
                    new Vector2Int[,]
                    {
                        { new Vector2Int(2,1), new Vector2Int(2,0), new Vector2Int(2,-1), new Vector2Int(2,-2) },
                        { new Vector2Int(-1,4), new Vector2Int(-1,3), new Vector2Int(-1,2), new Vector2Int(-1,1) },
                        { new Vector2Int(2,2), new Vector2Int(2,1), new Vector2Int(2,0), new Vector2Int(2,-1) },
                        { new Vector2Int(-1,2), new Vector2Int(-1,1), new Vector2Int(-1,0), new Vector2Int(-1,-1) },
                        { new Vector2Int(0,2), new Vector2Int(0,1), new Vector2Int(0,0), new Vector2Int(0,-1) },
                        { new Vector2Int(1,2), new Vector2Int(1,1), new Vector2Int(1,0), new Vector2Int(1,-1) },
                        { new Vector2Int(-1,2), new Vector2Int(-1,1), new Vector2Int(-1,0), new Vector2Int(-1,-1) },
                        { new Vector2Int(2,2), new Vector2Int(2,1), new Vector2Int(2,0), new Vector2Int(2,-1) },
                        { new Vector2Int(-1,1), new Vector2Int(-1,0), new Vector2Int(-1,-1), new Vector2Int(-1,-2) },
                        { new Vector2Int(2,4), new Vector2Int(2,3), new Vector2Int(2,2), new Vector2Int(2,1) },
                    },
                    new Vector2Int[,]
                    {
                        { new Vector2Int(-2,-1), new Vector2Int(-1,-1), new Vector2Int(0,-1), new Vector2Int(1,-1) },
                        { new Vector2Int(1,2), new Vector2Int(2,2), new Vector2Int(3,2), new Vector2Int(4,2) },
                        { new Vector2Int(-2,1), new Vector2Int(-1,1), new Vector2Int(0,1), new Vector2Int(1,1) },
                        { new Vector2Int(1,1), new Vector2Int(2,1), new Vector2Int(3,1), new Vector2Int(4,1) },
                        { new Vector2Int(-1,1), new Vector2Int(0,1), new Vector2Int(1,1), new Vector2Int(2,1) },
                        { new Vector2Int(-1,0), new Vector2Int(0,0), new Vector2Int(1,0), new Vector2Int(2,0) },
                        { new Vector2Int(-2,0), new Vector2Int(-1,0), new Vector2Int(0,0), new Vector2Int(1,0) },
                        { new Vector2Int(1,0), new Vector2Int(2,0), new Vector2Int(3,0), new Vector2Int(4,0) },
                        { new Vector2Int(-2,2), new Vector2Int(-1,2), new Vector2Int(0,2), new Vector2Int(1,2) },
                        { new Vector2Int(1,-1), new Vector2Int(2,-1), new Vector2Int(3,-1), new Vector2Int(4,-1) },
                    },
                    new Vector2Int[,]
                    {
                        { new Vector2Int(-1,3), new Vector2Int(-1,2), new Vector2Int(-1,1), new Vector2Int(-1,0) },
                        { new Vector2Int(2,0), new Vector2Int(2,-1), new Vector2Int(2,-2), new Vector2Int(2,-3) },
                        { new Vector2Int(-1,2), new Vector2Int(-1,1), new Vector2Int(-1,0), new Vector2Int(-1,-1) },
                        { new Vector2Int(2,2), new Vector2Int(2,1), new Vector2Int(2,0), new Vector2Int(2,-1) },
                        { new Vector2Int(1,2), new Vector2Int(1,1), new Vector2Int(1,0), new Vector2Int(1,-1) },
                        { new Vector2Int(0,2), new Vector2Int(0,1), new Vector2Int(0,0), new Vector2Int(0,-1) },
                        { new Vector2Int(2,2), new Vector2Int(2,1), new Vector2Int(2,0), new Vector2Int(2,-1) },
                        { new Vector2Int(-1,2), new Vector2Int(-1,1), new Vector2Int(-1,0), new Vector2Int(-1,-1) },
                        { new Vector2Int(2,3), new Vector2Int(2,2), new Vector2Int(2,1), new Vector2Int(2,0) },
                        { new Vector2Int(-1,0), new Vector2Int(-1,-1), new Vector2Int(-1,-2), new Vector2Int(-1,-3) },
                    },
                    new Vector2Int[,]
                    {
                        { new Vector2Int(0,2), new Vector2Int(1,2), new Vector2Int(2,2), new Vector2Int(3,2) },
                        { new Vector2Int(-3,-1), new Vector2Int(-2,-1), new Vector2Int(-1,-1), new Vector2Int(0,-1) },
                        { new Vector2Int(0,0), new Vector2Int(1,0), new Vector2Int(2,0), new Vector2Int(3,0) },
                        { new Vector2Int(-3,0), new Vector2Int(-2,0), new Vector2Int(-1,0), new Vector2Int(0,0) },
                        { new Vector2Int(-1,0), new Vector2Int(0,0), new Vector2Int(1,0), new Vector2Int(2,0) },
                        { new Vector2Int(-1,1), new Vector2Int(0,1), new Vector2Int(1,1), new Vector2Int(2,1) },
                        { new Vector2Int(0,1), new Vector2Int(1,1), new Vector2Int(2,1), new Vector2Int(3,1) },
                        { new Vector2Int(-3,1), new Vector2Int(-2,1), new Vector2Int(-1,1), new Vector2Int(0,1) },
                        { new Vector2Int(0,-1), new Vector2Int(1,-1), new Vector2Int(2,-1), new Vector2Int(3,-1) },
                        { new Vector2Int(-3,2), new Vector2Int(-2,2), new Vector2Int(-1,2), new Vector2Int(0,2) },
                    }
                }
            },
        };
        public static Dictionary<ShapeName, List<Vector2Int[]>> Orientation = new()
        {
            {
                ShapeName.I,
                new List<Vector2Int[]>(){
                    new Vector2Int[] { new Vector2Int(-1,1), new Vector2Int(0,1), new Vector2Int(1,1), new Vector2Int(2,1) }
                }
            }
        };
        public int RotationIndex = 0;
        public Vector2Int[] Cells
        {
            get
            {
                return Orientation[ShapeName][RotationIndex];
            }
        }
    };
}












