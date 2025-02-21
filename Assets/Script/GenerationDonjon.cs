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
    // positions wall and corridors and floor
    public HashSet<Vector2Int> walker = new();
    public HashSet<Vector2Int> allwalker = new();

    public HashSet<Vector2Int> allneightborcase = new();

    public List<Vector2Int> corridors = new();

    public HashSet<Vector2Int> allcorridors = new();

    public HashSet<Vector2Int> positionroom = new();

    public List<Vector2Int> endcorridors = new();

    public HashSet<Vector2Int> roomwalker = new();

    public HashSet<Vector2Int> delpositionroom = new();

    public List<(int, Vector2Int)> roomsorder = new();

    public List<(Vector2Int, Vector2Int)> possibilite = new();

    public HashSet<Vector2Int> visitedpos = new();
    
    public Dictionary<Vector2Int, List<Vector2Int>> allposrooms = new();

    //position objects
    public Dictionary<String, List<Vector2Int>> posobjects = new();

    // direction
    public List<Vector2Int> alldirection = new()
    {
        new Vector2Int(0, 1),
        new Vector2Int(0, -1),
        new Vector2Int(1, 0),
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

    // variables for corridors 
    public Vector2Int start_position_corridors = new(0, 0);
    public int corridors_lenght = 30;
    public int nbrcorridors = 50;

    // variables for floor 
    public int iterations = 100;
    public Vector2Int start_position = new(0, 0);
    public int distance_walk = 10;

    // variables for salle
    public float roompourcentage = 0.05f;

    public int minimumDistance = 7;
    
    //varaible for algo order rooms
    public int compteur = 1;
    
    // bool activation fonctionnalite
    public bool StartRandomEachIteration;

    public bool SmoothActivate;

    public bool DeleteSoloCasesActivate;

    public bool SoloRoomsActivate;

    public bool ActivateCorridors;

    public bool ActivateStartRooms;

    public bool ActivateDelRooms;
    
    public bool ActivateSquareRooms;

    // affichage
    public Tilemap sol, mur;

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
        tile_murcoindi1, 
        tile_rock;
    
    public List<TileBase> tiles = new List<TileBase>{};
    
    public List<TileBase> objet = new List<TileBase>{};
    
    public bool limiteco(Vector2Int startpos, Vector2Int newposition, int limit)
    {
        return Mathf.Abs(newposition.x - startpos.x) <= limit && Mathf.Abs(newposition.y - startpos.y) <= limit;
    }

    public HashSet<Vector2Int> RandomWalker(Vector2Int start_position, int distance_walk)
    {
        walker.Add(start_position);
        var new_position = start_position;

        for (var i = 0; i < distance_walk; i++)
        {
            var nbr = Random.Range(0, 4);
            var result = alldirection[nbr];
            new_position += result;

            if (ActivateSquareRooms)
            {
                if (limiteco(start_position, new_position, distance_walk/2))
                {
                    walker.Add(new_position);
                }
                else
                {
                    new_position -= result;
                }
            }
            else
            {
                walker.Add(new_position);
            }
        }

        return walker;
    }

    public void SoloRooms()
    {
        var overlap = false;

        foreach (var pos in roomwalker)
        foreach (var existingpos in allwalker)
            if (Vector2Int.Distance(pos, existingpos) < minimumDistance)
            {
                overlap = true;
                break;
            }

        if (overlap)
        {
            delpositionroom.Add(start_position);
            roomwalker.Clear();
        }
    }

    public void verifpositionrooms()
    {
        foreach (var pos in positionroom)
        foreach (var dir in alldirection_diagonale)
            if (allneightborcase.Contains(pos + dir))
            {
                delpositionroom.Add(pos);
                break;
            }
        
    }
    
    public HashSet<Vector2Int> RandomAllWalker()
    {
        for (var i = 0; i < iterations; i++)
        {
            RandomWalker(start_position, distance_walk);
            roomwalker.UnionWith(walker);
            walker.Clear();
            if (StartRandomEachIteration) start_position = roomwalker.ElementAt(Random.Range(0, roomwalker.Count));
        }

        if (SoloRoomsActivate) SoloRooms();
        allwalker.UnionWith(roomwalker);
        roomwalker.Clear();
        return allwalker;
    }

    public void CreateWalls(HashSet<Vector2Int> positions)
    {
        foreach (var pos in positions)
        foreach (var direction in alldirection)
        {
            var neightborcase = pos + direction;
            if (positions.Contains(neightborcase) == false) allneightborcase.Add(neightborcase);
        }
    }

    public void CreateWalls_coin(HashSet<Vector2Int> positions)
    {
        foreach (var pos in positions)
        foreach (var direction in alldirection_diagonale)
        {
            var neightborcase = pos + direction;
            if (positions.Contains(neightborcase) == false) allneightborcase.Add(neightborcase);
        }
    }
    
    public void Corridors(Vector2Int start_position_corridors, int corroridorlenght, int nbrcorridors)
    {
        for (var i = 0; i < nbrcorridors; i++)
        {
            var direction = alldirection[Random.Range(0, alldirection.Count)];
            corridors.Add(start_position_corridors);
            var current_position = start_position_corridors;
            for (var j = 0; j < corroridorlenght; j++)
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
        foreach (var pos in allcorridors)
        {
            var compteur = 0;
            foreach (var direction in alldirection)
                if (allcorridors.Contains(pos + direction))
                    compteur++;

            if (compteur == 1) endcorridors.Add(pos);
        }

        foreach (var pos in endcorridors)
        {
            start_position = pos;
            RandomAllWalker();
        }
    }

    public void CreateRooms()
    {
        var roomCreateCount = Mathf.RoundToInt(roompourcentage * allcorridors.Count);

        for (var i = 0; i < roomCreateCount; i++)
            positionroom.Add(allcorridors.ElementAt(Random.Range(0, allcorridors.Count)));
    }

    public void Smooth(HashSet<Vector2Int> positions, HashSet<Vector2Int> allneightborcase)
    {
        var newallneightborcase = new HashSet<Vector2Int>();
        var newpositions = new HashSet<Vector2Int>();
        foreach (var pos in positions)
        foreach (var direction in alldirection)
        {
            var neightbordcase = pos + direction;
            var neightbordcase2 = pos + direction * 2;
            if (allneightborcase.Contains(neightbordcase))
                if (positions.Contains(neightbordcase2))
                {
                    newallneightborcase.Add(neightbordcase);
                    newpositions.Add(neightbordcase);
                }
        }

        foreach (var pos in newallneightborcase) allneightborcase.Remove(pos);
        foreach (var pos in newpositions) positions.Add(pos);
    }

    public void DeleteSoloCases(HashSet<Vector2Int> positions, HashSet<Vector2Int> allneightborcase)
    {
        var newallneightborcase = new HashSet<Vector2Int>();
        foreach (var pos in allneightborcase)
        {
            var count = 0;
            foreach (var direction in alldirection)
            {
                var neightbordcase = pos + direction;
                if (positions.Contains(neightbordcase)) count++;
            }

            if (count == 4) newallneightborcase.Add(pos);
        }

        foreach (var pos in newallneightborcase)
        {
            allneightborcase.Remove(pos);
            positions.Add(pos);
        }
    }

    public List<(Vector2Int, Vector2Int)> GetDirPossibilite(List<Vector2Int> positions)
    {
        var newPossibilites = new List<(Vector2Int, Vector2Int)>();
        foreach (var pos in positions)
        foreach (var dir in alldirection)
        {
            var candidate = pos + dir;
            if (allcorridors.Contains(candidate) && !visitedpos.Contains(candidate))
                newPossibilites.Add((pos, candidate));
        }

        return newPossibilites;
    }
    
    // empecher que deux position de start rooms soit proche 
    // empecehr le faite que la start position d'une room soit dans un virage 
   public void allorderroom()
    {
        List<Vector2Int> initialPositions = new List<Vector2Int> { new Vector2Int(0, 0) };
        List<(Vector2Int, Vector2Int)> currentPossibilites = GetDirPossibilite(initialPositions);
        Dictionary<int, List<(Vector2Int, Vector2Int)>> allcurrentPossibilites = new Dictionary<int, List<(Vector2Int, Vector2Int)>>();

        while (currentPossibilites.Count > 0)
        {
            List<(Vector2Int, Vector2Int)> nextLevelPossibilities = new List<(Vector2Int, Vector2Int)>();
            List<Vector2Int> nextLevelEndpoints = new List<Vector2Int>();
            
            for (int i = 0; i < currentPossibilites.Count; i++) 
            {
                var (origin, candidate) = currentPossibilites[i];
                Vector2Int direction = candidate - origin;
                Vector2Int currentPos = origin;

                while (!(positionroom.Contains(currentPos) && currentPos != origin))
                {
                    if (!allcorridors.Contains(currentPos + direction))
                    {
                        List<(Vector2Int, Vector2Int)> branchPoss = GetDirPossibilite(new List<Vector2Int> { currentPos });
                        foreach (var (branchOrigin, branchCandidate) in branchPoss)
                        {
                            if (branchCandidate != currentPos + direction && !visitedpos.Contains(branchCandidate))
                            {
                                currentPossibilites.Add((currentPos, branchCandidate));
                            }
                        }

                        break;
                    }

                    currentPos += direction;
                    visitedpos.Add(currentPos);

                    List<(Vector2Int, Vector2Int)> branchPoss2 = GetDirPossibilite(new List<Vector2Int> { currentPos });
                    foreach (var (branchOrigin, branchCandidate) in branchPoss2)
                    {
                        if (branchCandidate != currentPos + direction && !visitedpos.Contains(branchCandidate))
                        {
                            currentPossibilites.Add((currentPos, branchCandidate));
                        }
                    }
                }

                if (positionroom.Contains(currentPos))
                {
                    nextLevelEndpoints.Add(currentPos);
                    roomsorder.Add((compteur, currentPos));
                }
            } 
            
            compteur++;
            
            List<(Vector2Int, Vector2Int)> endpointsPoss = GetDirPossibilite(nextLevelEndpoints);
            currentPossibilites = endpointsPoss;
            // currentPossibilites = nextLevelPossibilities.Concat(endpointsPoss).ToList();
        }
    }

    public void stockposrooms()
    {
        int iteration;
        if (ActivateSquareRooms)
        {
            iteration =  minimumDistance*minimumDistance;
        }
        else
        {
            iteration = distance_walk*distance_walk;
        }
        foreach (var (roomposstart, roompos) in roomsorder)
        {
            allposrooms[roompos] = new List<Vector2Int>();
            for (int i = roompos[0]-(int)Math.Sqrt(iteration); i < iteration ; i++)
            {
                for (int j = roompos[1]-(int)Math.Sqrt(iteration); j < iteration; j++)
                {
                    Vector2Int posactu = new Vector2Int(i, j);
                    if (allwalker.Contains(posactu))
                    {
                        allposrooms[roompos].Add(posactu);
                    }
                }
            }
        }
    }

    public (int, List<Vector2Int>) totalsrooms(int min, int max)
    {
        int result = 0;
        List<Vector2Int> rooms = new List<Vector2Int>();
        for (int i = min; i < max; i++)
        {
            foreach (var (roomposstart, roompos) in roomsorder)
            {
                if (roomposstart == i)
                {
                    result++;
                    rooms.Add(roompos);
                }
            }
        }
        return (result, rooms);
    }

    public void placeobjects(String objet, int min, int max, int nbrobjet, bool ActivateBorder = false)
    {
        posobjects[objet] = new List<Vector2Int>();
        (int nbrrooms, List<Vector2Int> roomsplaceobject)= totalsrooms(min, max);

        int nbrobjectperroomsmax = (int)Math.Round((double)nbrobjet / nbrrooms);
        foreach (Vector2Int pos in roomsplaceobject)
        {
            foreach (var (roomposstart, allroompos) in allposrooms)
            {
                if (roomposstart == pos)
                {
                    int nbrobjectperrooms = Random.Range(nbrobjectperroomsmax-2, nbrobjectperroomsmax);
                    if (nbrobjectperrooms < 0)
                    {
                        nbrobjectperrooms = 1;
                    }

                    if (!ActivateBorder)
                    {
                        for (int i = 0; i < nbrobjectperrooms; i++)
                        {
                            int posrandom = Random.Range(0, allroompos.Count);
                            Vector2Int findpos = allroompos[posrandom];
                            if (!allcorridors.Contains(findpos))
                            {
                                posobjects[objet].Add(findpos);
                            }
                            else
                            {
                                i--;
                            }
                        }
                    }
                }
            }
        }
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
        delpositionroom.Clear();
        roomsorder.Clear();
        visitedpos.Clear();
        possibilite.Clear();
        
        compteur = 1;
        
        
        
        // Generate map
        Corridors(start_position_corridors, corridors_lenght, nbrcorridors);
        DeadEnd();
        CreateRooms();
        var positions = new HashSet<Vector2Int>();
        var firstpositionroom = true;
        foreach (var pos in positionroom)
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
        verifpositionrooms();
        foreach (var posroom in delpositionroom) positionroom.Remove(posroom);
        foreach (var posroom in endcorridors) positionroom.Add(posroom);
        
        allorderroom();
        
        if (SmoothActivate) Smooth(positions, allneightborcase);
        if (DeleteSoloCasesActivate) DeleteSoloCases(positions, allneightborcase);
        
        stockposrooms();
        placeobjects("caillou",3,5,50);
        
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
            foreach (var (roomRank, roomPos) in roomsorder)
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
            foreach (var pos in delpositionroom)
            {
                var tileposition_sol = sol.WorldToCell((Vector3Int)pos);
                sol.SetTile(tileposition_sol, tile_test3);
            }

        foreach (var (name_object , allposobject) in posobjects)
        {
            if (name_object == "caillou")
            {
                for (int i = 0; i < allposobject.Count; i++)
                {
                    Debug.Log(allposobject[i]);
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
        objet.Clear();
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
        objet.Add(tile_rock);
        
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