using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomsOrder : MonoBehaviour
{
    
    // positions ordre des rooms
    
    public List<(int, Vector2Int)> roomsorder = new();

    public HashSet<Vector2Int> visitedpos = new();
    
    public Dictionary<Vector2Int, List<Vector2Int>> allposrooms = new();
    
    public Dictionary<Vector2Int, List<Vector2Int>> allposroomsneight = new();
    
    public int compteur = 1;
    
    //direction
    public Direction dir;
    public List<Vector2Int> alldirection => dir.alldirection;
    public List<Vector2Int> alldirection_diagonale => dir.alldirection_diagonale;
    
    //boolean Activate
    public ActivateFonctionnalite af;
    
    //var necessaire
    public CreateRoomsDonjon crd;
    public HashSet<Vector2Int> allcorridors => crd.allcorridors;
    public HashSet<Vector2Int> positionroom => crd.positionroom;
    public int distance_walk => crd.distance_walk;
    public HashSet<Vector2Int> allwalker => crd.allwalker;
    
    public HashSet<Vector2Int> allneightborcase => crd.allneightborcase;

    public void doubleposallcoridors()
    {
        visitedpos.Clear();
        foreach (Vector2Int start_position_rooms in positionroom)
        {
            List<Vector2Int> startPositionList = new List<Vector2Int> { start_position_rooms };
            List<(Vector2Int, Vector2Int)> currentPossibilites = GetDirPossibilite(startPositionList);
            List<Vector2Int> nextLevelEndpoints = new List<Vector2Int>();
            
            for (int i = 0; i < currentPossibilites.Count; i++) 
            {
                var (origin, candidate) = currentPossibilites[i];
                Vector2Int direction = candidate - origin;
                Vector2Int currentPos = origin;
                Vector2Int start_position = origin;
                int compteur_distance = 0;
                
                while (!(positionroom.Contains(currentPos) && currentPos != origin))
                {
                    if (!allcorridors.Contains(currentPos + direction))
                    {
                        List<(Vector2Int, Vector2Int)> branchPoss = GetDirPossibilite(new List<Vector2Int> { currentPos });
                        foreach (var (branchOrigin, branchCandidate) in branchPoss)
                        {
                            if (branchCandidate != currentPos + direction && !visitedpos.Contains(branchCandidate))
                            {
                                direction = branchCandidate - branchOrigin;
                                break;
                            }
                            
                        }
                    }

                    currentPos += direction;
                    visitedpos.Add(currentPos);
                    compteur_distance++;

                    List<(Vector2Int, Vector2Int)> branchPoss2 = GetDirPossibilite(new List<Vector2Int> { currentPos });
                    foreach (var (branchOrigin, branchCandidate) in branchPoss2)
                    {
                        if (branchCandidate != currentPos + direction && !visitedpos.Contains(branchCandidate))
                        {
                            break;
                        }
                    }
                }

                if (positionroom.Contains(currentPos))
                {
                    nextLevelEndpoints.Add(currentPos);
                    if (compteur_distance > 5)
                    {
                        crd.delpositionroom.Add(currentPos);
                    }
                }
            } 
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
    // si deux pos sont identiques donnée la prio a la plus grande 
   public void allorderroom()
    {
        visitedpos.Clear();
        List<Vector2Int> initialPositions = new List<Vector2Int> { new Vector2Int(0, 0) };
        List<(Vector2Int, Vector2Int)> currentPossibilites = GetDirPossibilite(initialPositions);

        while (currentPossibilites.Count > 0)
        {
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

    public void doubleposroomsorder()
    {
        for (int i = 0; i < roomsorder.Count; i++)
        {
            for (int j = i + 1; j < roomsorder.Count; j++)
            {
                if (roomsorder[i].Item2 == roomsorder[j].Item2)
                {
                    if (roomsorder[i].Item1 > roomsorder[j].Item1)
                    {
                        roomsorder.RemoveAt(i);
                        i--; 
                        break;
                    }
                    else
                    {
                        roomsorder.RemoveAt(j);
                        j--; 
                    }
                }
            }
        }


    }
    
    public void stockposrooms()
    {
        int iteration = distance_walk;
        
        foreach (var (roomposstart, roompos) in roomsorder)
        {
            allposrooms[roompos] = new List<Vector2Int>();
            for (int i = roompos.x-iteration; i < roompos.x+iteration ; i++)
            {
                for (int j = roompos.y-iteration; j < roompos.y+iteration; j++)
                {
                    Vector2Int posactu = new Vector2Int(i, j);
                    bool neight = true;
                    foreach (var pos in alldirection_diagonale)
                    {
                        if (allneightborcase.Contains(pos + posactu))
                        {
                            neight = false;
                        }
                    }
                    if (allwalker.Contains(posactu) && neight)
                    {
                        allposrooms[roompos].Add(posactu);
                    }
                }
            }
            if (allposrooms[roompos].Count == 0)
            {
                allposrooms[roompos].Add(roompos);
            }
        }
    }
    
    public void stockposroomsneight()
    {
        int iteration = distance_walk;
        
        foreach (var (roomposstart, roompos) in roomsorder)
        {
            allposroomsneight[roompos] = new List<Vector2Int>();
            for (int i = roompos.x-iteration; i < roompos.x+iteration ; i++)
            {
                for (int j = roompos.y-iteration; j < roompos.y+iteration; j++)
                {
                    Vector2Int posactu = new Vector2Int(i, j);
                    bool neight = false;
                    foreach (var pos in alldirection_diagonale)
                    {
                        if (allneightborcase.Contains(pos + posactu))
                        {
                            neight = true;
                        }
                    }
                    if (allwalker.Contains(posactu) && neight)
                    {
                        allposroomsneight[roompos].Add(posactu);
                    }
                }
            }
            if (allposroomsneight[roompos].Count == 0)
            {
                allposroomsneight[roompos].Add(roompos);
            }
        }
    }

    
}
