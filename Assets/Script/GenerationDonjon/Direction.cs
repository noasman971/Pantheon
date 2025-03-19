using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Direction : MonoBehaviour
{
    public List<Vector2Int> alldirection = new()
    {
        new Vector2Int(0, 1),
        new Vector2Int(0, -1),
        new Vector2Int(1, 0),
        new Vector2Int(-1, 0)
    };
    
    public List<Vector2Int> alldirection_corridors = new()
    {
        new Vector2Int(0, 1), new Vector2Int(0, 1), new Vector2Int(0, 1), 
        new Vector2Int(1, 0), new Vector2Int(1, 0), new Vector2Int(1, 0), 
        new Vector2Int(0, -1),
        new Vector2Int(-1, 0)
    };

    public List<Vector2Int> alldirection_diagonale = new()
    {
        new Vector2Int(0, 1),
        new Vector2Int(0, -1),
        new Vector2Int(1, 0),
        new Vector2Int(-1, 0),
        new Vector2Int(1, 1),
        new Vector2Int(-1, -1),
        new Vector2Int(1, -1),
        new Vector2Int(-1, 1)
    };

    public List<Vector2Int> alldirection_wolf = new()
    {
        new Vector2Int(0, 1),
        new Vector2Int(1, 0),
        new Vector2Int(1, 1),
        new Vector2Int(2, 1)
    };
}
