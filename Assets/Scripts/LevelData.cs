using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is a data class that represents a few things. It tells the game where the enemies are going to walk.
//It also tells the game how many enemies are in each wave.
public static class LevelData {

    //This data is used to make enemies go in the right direction. The first value is the spawn position.
    //The last value is the end of the path. Every value in between is a corner of the path.
    //The PathCorner position is not necessarily the same as transform position, but it is the 
    //tile position.
    public static readonly Vector2Int[,] PathCorners = new Vector2Int[,] {
        { new Vector2Int(-19, -3), new Vector2Int(-10, -3), new Vector2Int(-10, -10), new Vector2Int(-1, -10), new Vector2Int(-1, 3), new Vector2Int(12, 3), new Vector2Int(12, -8), new Vector2Int(21, -8), new Vector2Int(21, -4), new Vector2Int(30, -4)}
    };
}
