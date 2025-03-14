using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class GenerationDonjon : MonoBehaviour
{
    public HashSet<Vector2Int> positions = new ();
    // affichage
    public Tilemap sol, mur, soltest;

    [HideInInspector] public TileBase
        tile_solcoinhg1,
        tile_solcoinhd1,
        tile_solcoinbg1,
        tile_solcoinbd1,
        tile_sold1,
        tile_solg1,
        tile_solh1,
        tile_solh2,
        tile_solb1,
        tile_solb2,
        tile_sol1,
        tile_sol2,
        tile_sol3,
        tile_sol4,
        tile_sol5,
        tile_test,
        tile_test2,
        tile_test3,
        tile_murh1,
        tile_murh2,
        tile_murb1,
        tile_murb2,
        tile_murg1,
        tile_murg2,
        tile_murd1,
        tile_murd2,
        tile_murcoing1,
        tile_murcoind1,
        tile_murcoingi1,
        tile_murcoindi1; 
        
    public List<TileBase> tiles = new List<TileBase>{};

    public CreateRoomsDonjon crd;
    public HashSet<Vector2Int> allcorridors => crd.allcorridors;
    public HashSet<Vector2Int> allneightborcase => crd.allneightborcase;
    public HashSet<Vector2Int> positionroom => crd.positionroom;

    public RoomsOrder ro;

    public ActivateFonctionnalite af;
    public bool SmoothActivate => af.SmoothActivate;
    public bool DeleteSoloCasesActivate => af.DeleteSoloCasesActivate;
    public bool ActivateStartRooms => af.ActivateStartRooms;
    public bool ActivateDelRooms => af.ActivateDelRooms;
    public bool ActivateCorridors => af.ActivateCorridors;

    public PlaceObject po;
    public Dictionary<String, List<Vector2Int>> posobjects => po.posobjects;

    public AffichageTiles at;
    
    public void GenerateMap()
    {
        // Clear the tilemaps
        sol.ClearAllTiles();
        mur.ClearAllTiles();
        soltest.ClearAllTiles();

        // Clear previous data
        positions.Clear();
        crd.walker.Clear();
        crd.allwalker.Clear();
        crd.allneightborcase.Clear();
        crd.corridors.Clear();
        crd.allcorridors.Clear();
        crd.positionroom.Clear();
        crd.endcorridors.Clear();
        crd.delpositionroom.Clear();
        ro.roomsorder.Clear();
        ro.visitedpos.Clear();
        ro.allposrooms.Clear();
        
        ro.compteur = 1;
        
        // Generate map
        crd.setstart_position_corridors(new Vector2Int(0, 0));
        crd.Corridors(crd.start_position_corridors, crd.corridors_lenght, crd.nbrcorridors);
        crd.DeadEnd();
        crd.CreateRooms();
        
        var firstpositionroom = true;
        foreach (var pos in positionroom)
        {
            if (firstpositionroom)
            {
                crd.setstart_position(new Vector2Int(0, 0));
                
                firstpositionroom = false;
            }
            else
            {
                crd.setstart_position(pos);
            }
            positions.UnionWith(crd.RandomAllWalker());
        }

        positions.UnionWith(allcorridors);
        crd.CreateWalls(positions);
        crd.CreateWalls_coin(positions);
        crd.verifpositionrooms();
        foreach (var posroom in crd.delpositionroom) positionroom.Remove(posroom);
        foreach (var posroom in crd.endcorridors) positionroom.Add(posroom);
        
        ro.allorderroom();
        
        if (SmoothActivate) crd.Smooth(positions, allneightborcase);
        if (DeleteSoloCasesActivate) crd.DeleteSoloCases(positions, allneightborcase);
        
        // ro.stockposrooms();
        // po.placeobjects("caillou",3,10,50);
        
        foreach (var pos in positions)
        {
            if (!positions.Contains(pos + new Vector2Int(1,0)) && !positions.Contains(pos + new Vector2Int(0,1)))
            {
                var tileposition_sol = sol.WorldToCell((Vector3Int)pos);
                sol.SetTile(tileposition_sol, tile_solcoinhd1);
            }
            else if (!positions.Contains(pos + new Vector2Int(-1,0)) && !positions.Contains(pos + new Vector2Int(0,1)))
            {
                var tileposition_sol = sol.WorldToCell((Vector3Int)pos);
                sol.SetTile(tileposition_sol, tile_solcoinhg1);
            }
            else if (!positions.Contains(pos + new Vector2Int(-1,0)) && !positions.Contains(pos + new Vector2Int(0,-1)))
            {
                var tileposition_sol = sol.WorldToCell((Vector3Int)pos);
                sol.SetTile(tileposition_sol, tile_solcoinbg1);
            }
            else if (!positions.Contains(pos + new Vector2Int(1,0)) && !positions.Contains(pos + new Vector2Int(0,-1)))
            {
                var tileposition_sol = sol.WorldToCell((Vector3Int)pos);
                sol.SetTile(tileposition_sol, tile_solcoinbd1);
            }
            else if (!positions.Contains(pos + new Vector2Int(-1,0)))
            {
                var tileposition_sol = sol.WorldToCell((Vector3Int)pos);
                sol.SetTile(tileposition_sol, tile_solg1);
            }
            else if (!positions.Contains(pos + new Vector2Int(1,0)))
            {
                var tileposition_sol = sol.WorldToCell((Vector3Int)pos);
                sol.SetTile(tileposition_sol, tile_sold1);
            }
            else if (!positions.Contains(pos + new Vector2Int(0,1)))
            {
                TileBase[] tileSol = new TileBase[] { tile_solh1, tile_solh2};
                int x = Random.Range(0, tileSol.Length);
                var tileposition_sol = sol.WorldToCell((Vector3Int)pos);
                sol.SetTile(tileposition_sol, tileSol[x]);
            }
            else if (!positions.Contains(pos + new Vector2Int(0,-1)))
            {
                TileBase[] tileSol = new TileBase[] { tile_solb1, tile_solb2};
                int x = Random.Range(0, tileSol.Length);
                var tileposition_sol = sol.WorldToCell((Vector3Int)pos);
                sol.SetTile(tileposition_sol, tile_solb1);
            }
            else
            {
                TileBase[] tileSol = new TileBase[] { tile_sol1, tile_sol2, tile_sol3, tile_sol4, tile_sol5};
                int x = Random.Range(0, tileSol.Length);
                var tileposition_sol = sol.WorldToCell((Vector3Int)pos);
                sol.SetTile(tileposition_sol, tileSol[x]);
            }
        }

        foreach (var pos in allcorridors)
        {
            TileBase[] tileSol = new TileBase[] { tile_sol1, tile_sol2, tile_sol3, tile_sol4, tile_sol5};
            int x = Random.Range(0, tileSol.Length);
            var tileposition_sol = sol.WorldToCell((Vector3Int)pos);
            sol.SetTile(tileposition_sol, tileSol[x]);
        }

        foreach (var pos in allneightborcase)
        {
            if(positions.Contains(pos+ new Vector2Int(0,-1)))
            {
                TileBase[] tileMur = new TileBase[] { tile_murh1, tile_murh2};
                int x = Random.Range(0, tileMur.Length);
                var tilepositionMur = mur.WorldToCell((Vector3Int)pos);
                mur.SetTile(tilepositionMur, tileMur[x]);
            }
            else if(positions.Contains(pos+ new Vector2Int(0,1)))
            {
                TileBase[] tileMur = { tile_murb1, tile_murb2};
                int x = Random.Range(0, tileMur.Length);
                var tilepositionMur = mur.WorldToCell((Vector3Int)pos);
                mur.SetTile(tilepositionMur, tileMur[x]);
            }
            else if(positions.Contains(pos+ new Vector2Int(1,0)))
            {
                TileBase[] tileMur = new TileBase[] { tile_murg1, tile_murg2};
                int x = Random.Range(0, tileMur.Length);
                var tilepositionMur = mur.WorldToCell((Vector3Int)pos);
                mur.SetTile(tilepositionMur, tileMur[x]);
            }
            else if(positions.Contains(pos+ new Vector2Int(-1,0)))
            {
                TileBase[] tileMur = new TileBase[] { tile_murd1, tile_murd2};
                int x = Random.Range(0, tileMur.Length);
                var tilepositionMur = mur.WorldToCell((Vector3Int)pos);
                mur.SetTile(tilepositionMur, tileMur[x]);
            }
            else if(positions.Contains(pos+ new Vector2Int(-1,-1)))
            {
                TileBase[] tileMur = new TileBase[] { tile_murd1, tile_murd2};
                int x = Random.Range(0, tileMur.Length);
                var tilepositionMur = mur.WorldToCell((Vector3Int)pos);
                mur.SetTile(tilepositionMur, tileMur[x]);
            }
            else if(positions.Contains(pos+ new Vector2Int(1,-1)))
            {
                TileBase[] tileMur = new TileBase[] { tile_murg1, tile_murg2};
                int x = Random.Range(0, tileMur.Length);
                var tilepositionMur = mur.WorldToCell((Vector3Int)pos);
                mur.SetTile(tilepositionMur, tileMur[x]);
            }
            else if(positions.Contains(pos+ new Vector2Int(1,1)))
            {
                var tilepositionMur = mur.WorldToCell((Vector3Int)pos);
                mur.SetTile(tilepositionMur, tile_murcoing1);
            }
            else if(positions.Contains(pos+ new Vector2Int(-1,1)))
            {
                var tilepositionMur = mur.WorldToCell((Vector3Int)pos);
                mur.SetTile(tilepositionMur, tile_murcoind1);
            }
        }
        
        foreach (var pos in allneightborcase)
        {
            if (positions.Contains(pos + new Vector2Int(1, 0)) 
                && positions.Contains(pos + new Vector2Int(0, 1))
                && allneightborcase.Contains(pos + new Vector2Int(-1,0)) 
                && allneightborcase.Contains(pos+ new Vector2Int(0,-1)))
            {
                var tilepositionMur = mur.WorldToCell((Vector3Int)pos);
                mur.SetTile(tilepositionMur, tile_murcoingi1);
            }
            else if (positions.Contains(pos + new Vector2Int(0, 1)) 
                && positions.Contains(pos + new Vector2Int(-1, 0))
                && allneightborcase.Contains(pos + new Vector2Int(0,-1)) 
                && allneightborcase.Contains(pos+ new Vector2Int(1,0)))
            {
                var tilepositionMur = mur.WorldToCell((Vector3Int)pos);
                mur.SetTile(tilepositionMur, tile_murcoindi1);
            }
        }
        
        foreach (var pos in allcorridors)
        {
            var tileposition_sol = sol.WorldToCell((Vector3Int)pos);
            if (ActivateCorridors)
                sol.SetTile(tileposition_sol, tile_test);
        }

        if (af.ActivateTestStock)
        {
            

            foreach (KeyValuePair<Vector2Int, List<Vector2Int>> roomEntry in ro.allposrooms)
            {
                Vector2Int roomStart = roomEntry.Key;
                
                foreach (Vector2Int pos in roomEntry.Value)
                {
                    var tileposition_sol_test = soltest.WorldToCell(new Vector3Int(pos.x, pos.y, 0));

                    if (pos == roomStart)
                    {
                        soltest.SetTile(tileposition_sol_test, tile_test3);
                    }
                    else 
                    {
                        soltest.SetTile(tileposition_sol_test, tile_test2);
                    }
                }
            }

            
        }
        
        if (ActivateStartRooms)
        {
            // var colors = new[]
            // {
            //     new Color(0f, 1f, 0f),   // Vert pur
            //     new Color(0.05f, 0.975f, 0f),
            //     new Color(0.1f, 0.95f, 0f),
            //     new Color(0.15f, 0.925f, 0f),
            //     new Color(0.2f, 0.9f, 0f),
            //     new Color(0.25f, 0.875f, 0f),
            //     new Color(0.3f, 0.85f, 0f),
            //     new Color(0.35f, 0.825f, 0f),
            //     new Color(0.4f, 0.8f, 0f),
            //     new Color(0.45f, 0.775f, 0f),
            //     new Color(0.5f, 0.75f, 0f),
            //     new Color(0.55f, 0.725f, 0f),
            //     new Color(0.6f, 0.7f, 0f),
            //     new Color(0.65f, 0.675f, 0f),
            //     new Color(0.7f, 0.65f, 0f),
            //     new Color(0.75f, 0.625f, 0f),
            //     new Color(0.8f, 0.6f, 0f),
            //     new Color(0.825f, 0.575f, 0f),
            //     new Color(0.85f, 0.55f, 0f),
            //     new Color(0.875f, 0.525f, 0f),
            //     new Color(0.9f, 0.5f, 0f),
            //     new Color(0.925f, 0.475f, 0f),
            //     new Color(0.95f, 0.45f, 0f),
            //     new Color(0.975f, 0.425f, 0f),
            //     new Color(1f, 0.4f, 0f),   // Orange
            //     new Color(1f, 0.375f, 0f),
            //     new Color(1f, 0.35f, 0f),
            //     new Color(1f, 0.325f, 0f),
            //     new Color(1f, 0.3f, 0f),
            //     new Color(1f, 0.275f, 0f),
            //     new Color(1f, 0.25f, 0f),
            //     new Color(1f, 0.225f, 0f),
            //     new Color(1f, 0.2f, 0f),
            //     new Color(1f, 0.175f, 0f),
            //     new Color(1f, 0.15f, 0f),
            //     new Color(1f, 0.125f, 0f),
            //     new Color(1f, 0.1f, 0f),
            //     new Color(1f, 0.05f, 0f),
            //     new Color(1f, 0f, 0f)    // Rouge pur
            // };
            
            var colors = new[]
            {
                new Color(1f, 0f, 0f),   // Rouge
                new Color(0f, 1f, 0f),   // Vert
                new Color(0f, 0f, 1f),   // Bleu
                new Color(1f, 1f, 0f),   // Jaune
                new Color(1f, 0f, 1f),   // Magenta
                new Color(0f, 1f, 1f),   // Cyan
                new Color(1f, 0.5f, 0f), // Orange
                new Color(0.5f, 0f, 1f), // Violet
                new Color(0.5f, 1f, 0f), // Vert clair
                new Color(1f, 0f, 0.5f), // Rose foncé
                new Color(0f, 0.5f, 1f), // Bleu clair
                new Color(0.5f, 1f, 1f), // Turquoise
                new Color(1f, 0.5f, 0.5f), // Rouge clair
                new Color(0.5f, 0.5f, 1f), // Bleu lavande
                new Color(1f, 1f, 0.5f), // Jaune pastel
                new Color(0.5f, 1f, 0.5f), // Vert pastel
                new Color(1f, 0.75f, 0f), // Jaune orangé
                new Color(0.75f, 0f, 1f), // Violet profond
                new Color(0.25f, 0.25f, 0.25f), // Gris foncé
                new Color(0.75f, 0.75f, 0.75f)  // Gris clair
            };
        
            
            // Dictionnaire pour associer un rang de salle à une couleur.
            var colorMapping = new Dictionary<int, Color>();
        
            // Pour chaque salle enregistrée dans roomsorder, on crée une tuile colorée.
            foreach (var (roomRank, roomPos) in ro.roomsorder)
            {
                // Si ce rang n'a pas encore de couleur attribuée, on lui en assigne une.
                if (!colorMapping.ContainsKey(roomRank)) colorMapping[roomRank] = colors[roomRank % colors.Length];
        
                // Conversion de la position du monde en position de tile.
                var tilePosition = sol.WorldToCell((Vector3Int)roomPos);
        
                // Création d'une nouvelle tuile en se basant sur tile_test2
                var coloredTile = ScriptableObject.CreateInstance<Tile>();
                var baseTile = tile_test2 as Tile;
                if (baseTile != null) coloredTile.sprite = baseTile.sprite;
                // Appliquer la couleur déterminée par le rang
                coloredTile.color = colorMapping[roomRank];
        
                // Placer la tuile colorée dans le tilemap du sol
                sol.SetTile(tilePosition, coloredTile);
            }
        }
        
        if (ActivateDelRooms)
        {
            foreach (var pos in crd.delpositionroom)
            {
                var tileposition_sol = sol.WorldToCell((Vector3Int)pos);
                sol.SetTile(tileposition_sol, tile_test3);
            }
        }
        
        foreach (var (name_object , allposobject) in posobjects)
        {
            if (name_object == "caillou")
            {
                for (int i = 0; i < allposobject.Count; i++)
                {
                    var tileposition_sol = sol.WorldToCell((Vector3Int)allposobject[i]);
                    sol.SetTile(tileposition_sol, tile_test3);
                }
                
            }
        }
    }

    private void Start()
    {
        GenerateMap();
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        tiles.Clear();
        tiles.Add(tile_solcoinhg1);
        tiles.Add(tile_solcoinhd1);
        tiles.Add(tile_solcoinbg1);
        tiles.Add(tile_solcoinbd1);
        tiles.Add(tile_sold1);
        tiles.Add(tile_solg1);
        tiles.Add(tile_solh1);
        tiles.Add(tile_solh2);
        tiles.Add(tile_solb1);
        tiles.Add(tile_solb2);
        tiles.Add(tile_sol1);
        tiles.Add(tile_sol2);
        tiles.Add(tile_sol3);
        tiles.Add(tile_sol4);
        tiles.Add(tile_sol5);
        tiles.Add(tile_test);
        tiles.Add(tile_test2);
        tiles.Add(tile_test3);
        tiles.Add(tile_murh1);
        tiles.Add(tile_murh2);
        tiles.Add(tile_murb1);
        tiles.Add(tile_murb2);
        tiles.Add(tile_murg1);
        tiles.Add(tile_murg2);
        tiles.Add(tile_murd1);
        tiles.Add(tile_murd2);
        tiles.Add(tile_murcoing1);
        tiles.Add(tile_murcoind1);
        tiles.Add(tile_murcoingi1);
        tiles.Add(tile_murcoindi1);
        
        EditorUtility.SetDirty(this); 
    }
    
    [CustomEditor(typeof(GenerationDonjon))]
    public class GenerationDonjonEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var script = (GenerationDonjon)target;
            if (GUILayout.Button("Generate Map")) script.GenerateMap();
        }
    }
#endif
}