using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Square : MonoBehaviour
{

    public Tilemap tilemap; 
    public TileBase[] tiles; 
    public Vector3Int position;

    public static Vector3 AddX(Vector3 vector, float value)
    {
        return new Vector3(vector.x + value, vector.y, vector.z);
    }
    public static Vector3 AddY(Vector3 vector, float value)
    {
        return new Vector3(vector.x, vector.y + value, vector.z);
    }
    public static Vector3 AddZ(Vector3 vector, float value)
    {
        return new Vector3(vector.x, vector.y, vector.z + value);
    }

    void Start()
    {
        position = new Vector3Int(4,4,0);
        AddX(position,-2);
        tilemap.SetTile(position, tiles[0]);
        
    }
    
}
