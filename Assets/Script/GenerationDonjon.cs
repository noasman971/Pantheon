using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Tilemaps;
using UnityEditor;

public class GenerationDonjon : MonoBehaviour
{
    // positions wall and corridors and floor
    public HashSet<Vector2Int> walker = new HashSet<Vector2Int>();
    public HashSet<Vector2Int> allwalker = new HashSet<Vector2Int>();

    public HashSet<Vector2Int> allneightborcase = new HashSet<Vector2Int>();

    public List<Vector2Int> corridors = new List<Vector2Int>();

    public HashSet<Vector2Int> allcorridors = new HashSet<Vector2Int>();

    public HashSet<Vector2Int> positionroom = new HashSet<Vector2Int>();

    public List<Vector2Int> endcorridors = new List<Vector2Int>();

    public HashSet<Vector2Int> roomwalker = new HashSet<Vector2Int>();

    // public Dictionary<Vector2Int, HashSet<Vector2Int>> roomsdictionnary = new Dictionary<Vector2Int, HashSet<Vector2Int>>();

    // public Dictionary<string, Dictionary<string, string>> roomsorder = new Dictionary<string, Dictionary<string, string>>();

    // direction
    public List<Vector2Int> alldirection = new List<Vector2Int>{
        new Vector2Int(0,1),
        new Vector2Int(0,-1),
        new Vector2Int(1,0),
        new Vector2Int(-1,0),
    };
    public List<Vector2Int> alldirection_diagonale = new List<Vector2Int>{
        new Vector2Int(0,1),
        new Vector2Int(0,-1),
        new Vector2Int(1,0),
        new Vector2Int(-1,0),
        new Vector2Int(1,1),
        new Vector2Int(-1,-1),
        new Vector2Int(1,-1),
        new Vector2Int(-1,1),
    };

    // variables for corridors 
    public Vector2Int start_position_corridors = new Vector2Int(0, 0); 
    public int corridors_lenght = 10;
    public int nbrcorridors = 5;

    // variables for floor 
    public int iterations = 2;
    public Vector2Int start_position = new Vector2Int(0, 0); 
    public  int distance_walk = 5;

    // variables for salle
    public float roompourcentage = 0.80f;

    public int minimumDistance = 5; 

    // affichage
    public Tilemap sol, mur;
    public TileBase tile_sol, tile_mur, tile_test, tile_test2;

    // bool activation fonctionnalite
    public bool StartRandomEachIteration;

    public bool SmoothActivate;

    public bool DeleteSoloCasesActivate;

    public bool SoloRoomsActivate;

    public bool ActivateCorridors;

    public bool ActivateStartRooms;
    public HashSet<Vector2Int> RandomWalker(Vector2Int start_position, int distance_walk)
    {
        walker.Add(start_position);
        var new_position = start_position;
        
        for (int i = 0; i < distance_walk; i++)
        {
            int nbr = UnityEngine.Random.Range(0, 4);
            Vector2Int result = alldirection[nbr];
            new_position+=result;
            walker.Add(new_position);
        }
        return walker;
        
    }

    // public void SoloRooms ()
    // {
    //     bool overlap = false;
    //     foreach(Vector2Int pos in roomwalker)
    //     {
    //         if(allwalker.Contains(pos))
    //         {
    //             overlap = true;
    //             break;
    //         }
    //     }
    //     if(overlap)
    //     {
    //         roomwalker.Clear();
    //     }
    //     Debug.Log($"Walker size: {walker}");
    //     Debug.Log($"Walker size: {walker.Count}");
    //     Debug.Log($"Overlap detected: {overlap}");
    // }

    public void SoloRooms()
    {
        bool overlap = false;
        

        foreach (Vector2Int pos in roomwalker)
        {
            foreach (Vector2Int existingpos in allwalker)
            {
                if (Vector2Int.Distance(pos, existingpos) < minimumDistance)
                {
                    overlap = true;
                    break;
                }
            }
        }

        if (overlap) 
        {
            roomwalker.Clear();
        }
    }

    public HashSet<Vector2Int> RandomAllWalker()
    {
        for (int i = 0; i < iterations; i++)
        {
            RandomWalker(start_position, distance_walk);
            roomwalker.UnionWith(walker);
            walker.Clear();
            if(StartRandomEachIteration)
            {
                start_position = allwalker.ElementAt(UnityEngine.Random.Range(0, allwalker.Count));
            }
        }
        if (SoloRoomsActivate)
        {
            SoloRooms();
        }
        // roomsdictionnary.Add(start_position,roomwalker);
        allwalker.UnionWith(roomwalker);
        roomwalker.Clear();
        return allwalker;
    }

    public void CreateWalls(HashSet<Vector2Int> positions)
    {
        foreach (Vector2Int pos in positions)
        {
           foreach (Vector2Int direction in alldirection)
           {
                Vector2Int neightborcase = pos + direction;
                if (positions.Contains(neightborcase) == false)
                {
                    allneightborcase.Add(neightborcase);
                }
           }
        }
    }

    public void CreateWalls_coin(HashSet<Vector2Int> positions)
    {
        foreach (Vector2Int pos in positions)
        {
           foreach (Vector2Int direction in alldirection_diagonale)
           {
                Vector2Int neightborcase = pos + direction;
                if (positions.Contains(neightborcase) == false)
                {
                    allneightborcase.Add(neightborcase);
                }
           }
        }
    }

    // public void Corridors(HashSet<Vector2Int> positions, HashSet<Vector2Int> allneightborcase, HashSet<Vector2Int> corridors)
    // {
    //     Vector2Int mur_random = new Vector2Int();
    //     Vector2Int mouv = new Vector2Int();
    
    //     var count_random = UnityEngine.Random.Range(0, allneightborcase.Count);
    //     var count = 0;
    //     var distance_walk_corridors = UnityEngine.Random.Range(10, 15);

    //     foreach  (Vector2Int pos in allneightborcase)
    //     {
    //         Debug.Log(pos);
    //         if (count_random == count)
    //         {
    //             Debug.Log(pos);
    //             mur_random = pos;
    //         }
    //         count++;
    //     }

    //     foreach (Vector2Int direction in alldirection)
    //     {
    //         if (!positions.Contains(mur_random+direction) && !allneightborcase.Contains(mur_random+direction))
    //         {
    //             mouv = mur_random+direction-mur_random;
    //         }
    //     }

    //     Debug.Log(mur_random);
    //     for (int i = 0; i<distance_walk_corridors; i++)
    //     {
    //         Debug.Log(mur_random);
    //         mur_random = mur_random+mouv;
    //         corridors.Add(mur_random);
    //     }

    // }

    public void Corridors(Vector2Int start_position_corridors, int corroridorlenght, int nbrcorridors)
    {  
        for(int i = 0; i < nbrcorridors; i++ ) 
        {
            Vector2Int direction = alldirection[UnityEngine.Random.Range(0, alldirection.Count)];
            corridors.Add(start_position_corridors);
            var current_position = start_position_corridors;
            for (int j = 0; j < corroridorlenght; j++) 
            {
                current_position += direction;
                corridors.Add(current_position);
            
            }
            start_position_corridors = corridors[corridors.Count - 1];
            allcorridors.UnionWith(corridors);
        }
    }

    public void DeadEnd()
    {
        
        foreach(Vector2Int pos in allcorridors)
        {
            int compteur = 0;
            foreach(Vector2Int direction in alldirection)
            {
                if (allcorridors.Contains(pos + direction))
                {
                    compteur++;
                }

            }
            if (compteur==1)
            {
                endcorridors.Add(pos);
            }
        }

        foreach(Vector2Int pos in endcorridors)
        {
            start_position = pos;
            RandomAllWalker();
        }
    }

    public void CreateRooms()
    {
        int roomCreateCount = Mathf.RoundToInt(roompourcentage * allcorridors.Count);
         
        for (int i = 0; i < roomCreateCount;i++) 
        {
            positionroom.Add(allcorridors.ElementAt(UnityEngine.Random.Range(0, allcorridors.Count)));
        }
    }

    public void Smooth(HashSet<Vector2Int> positions, HashSet<Vector2Int> allneightborcase)
    {
        HashSet<Vector2Int> newallneightborcase = new HashSet<Vector2Int>();
        HashSet<Vector2Int> newpositions = new HashSet<Vector2Int>();
        foreach (Vector2Int pos in positions)
        {
            foreach(Vector2Int direction in alldirection)
            {
                Vector2Int neightbordcase = pos + direction;
                Vector2Int neightbordcase2 = pos + (direction*2);
                if (allneightborcase.Contains(neightbordcase) == true)
                {
                    if(positions.Contains(neightbordcase2) == true)
                    {
                        newallneightborcase.Add(neightbordcase);
                        newpositions.Add(neightbordcase);
                    }
                }
            }
        }
        foreach (Vector2Int pos in newallneightborcase)
        {
            allneightborcase.Remove(pos);
        }
        foreach (Vector2Int pos in newpositions)
        {
            positions.Add(pos);
        }
    }

    public void DeleteSoloCases (HashSet<Vector2Int> positions, HashSet<Vector2Int> allneightborcase)
    {
        HashSet<Vector2Int> newallneightborcase = new HashSet<Vector2Int>();
        foreach (Vector2Int pos in allneightborcase)
        {
            int count = 0;
            foreach (Vector2Int direction in alldirection)
            {
                Vector2Int neightbordcase = pos + direction;
                if (positions.Contains(neightbordcase))
                {
                    count++;
                }   
            }
            if (count==4)
            {
                newallneightborcase.Add(pos);
            }
        }
        foreach (Vector2Int pos in newallneightborcase)
        {
            allneightborcase.Remove(pos);
            positions.Add(pos);
        }
    }

    public void RoomsOrders()
    {

    }

    public void GenerateMap()
    {
        // Clear the tilemaps
        sol.ClearAllTiles();
        mur.ClearAllTiles();
        
        // Clear previous data
        walker.Clear();
        allwalker.Clear();
        allneightborcase.Clear();
        corridors.Clear();
        allcorridors.Clear();
        positionroom.Clear();
        endcorridors.Clear();

        // Generate map
        Corridors(start_position_corridors, corridors_lenght, nbrcorridors);
        DeadEnd();
        CreateRooms();
        HashSet<Vector2Int> positions = new HashSet<Vector2Int>();
        bool firstpositionroom = true;
        foreach (Vector2Int pos in positionroom) 
        {
            if (firstpositionroom)
            {
                start_position = new Vector2Int(0, 0);
                firstpositionroom = false;
            }
            else 
            {
                start_position = pos;
            }
            
            positions.UnionWith(RandomAllWalker());
        }
        positions.UnionWith(allcorridors);
        CreateWalls(positions);
        CreateWalls_coin(positions);
        
        if (SmoothActivate)
        {
            Smooth(positions, allneightborcase);
        }
        if (DeleteSoloCasesActivate)
        {
            DeleteSoloCases(positions, allneightborcase);
        }
        foreach (Vector2Int pos in positions)
        {
            var tileposition_sol = sol.WorldToCell((Vector3Int)pos);
            sol.SetTile(tileposition_sol, tile_sol);
        }
        foreach(Vector2Int pos in allneightborcase)
        {
            var tileposition_mur = mur.WorldToCell((Vector3Int)pos);
            mur.SetTile(tileposition_mur, tile_mur);
        }
        foreach(Vector2Int pos in allcorridors)
        {
            var tileposition_sol = sol.WorldToCell((Vector3Int)pos);
            if(ActivateCorridors) 
            { 
                sol.SetTile(tileposition_sol, tile_test);
            }
            else 
            {
                sol.SetTile(tileposition_sol, tile_sol);
            }
            
        }
        if(ActivateStartRooms)
        {
            foreach (Vector2Int pos in positionroom)
            {
                var tileposition_sol = sol.WorldToCell((Vector3Int)pos);
                sol.SetTile(tileposition_sol, tile_test2);
            }
        }
    }
    
    void Start()
    {
        GenerateMap();
    }
    
#if UNITY_EDITOR
    [CustomEditor(typeof(GenerationDonjon))]
    public class GenerationDonjonEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            GenerationDonjon script = (GenerationDonjon)target;
            if (GUILayout.Button("Generate Map"))
            {
                script.GenerateMap();
            }
        }
    }
#endif
}
    