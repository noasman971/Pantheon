using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AffichageTiles : MonoBehaviour
{
    [HideInInspector] public TileBase 
        tile_rock;
    
    public List<TileBase> objet = new List<TileBase>{};
    
#if UNITY_EDITOR
    private void OnValidate()
    {
        objet.Clear();
        objet.Add(tile_rock);
        
        EditorUtility.SetDirty(this); 
    }
#endif
}
