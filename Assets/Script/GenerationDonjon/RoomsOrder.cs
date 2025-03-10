using System;
using System.Collections.Generic;
using UnityEngine;

public class RoomsOrder : MonoBehaviour
{
    
    // positions ordre des rooms
    
    public List<(int, Vector2Int)> roomsorder = new();

    public HashSet<Vector2Int> visitedpos = new();
    
    public Dictionary<Vector2Int, List<Vector2Int>> allposrooms = new();
    
    public int compteur = 1;
    
    //direction
    public Direction dir;
    public List<Vector2Int> alldirection => dir.alldirection;
    
    //boolean Activate
    public ActivateFonctionnalite af;
    public bool ActivateSquareRooms => af.ActivateSquareRooms;
    
    //var necessaire
    public CreateRoomsDonjon crd;
    public HashSet<Vector2Int> allcorridors => crd.allcorridors;
    public HashSet<Vector2Int> positionroom => crd.positionroom;
    public int minimumDistance => crd.minimumDistance;
    public int distance_walk => crd.distance_walk;
    public HashSet<Vector2Int> allwalker => crd.allwalker;
    
    
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
}
