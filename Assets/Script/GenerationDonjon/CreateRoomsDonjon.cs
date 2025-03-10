using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;

public class CreateRoomsDonjon : MonoBehaviour
{
    //position wall and floor
    public HashSet<Vector2Int> walker = new();
    
    public HashSet<Vector2Int> allwalker = new();

    public HashSet<Vector2Int> allneightborcase = new();

    public List<Vector2Int> corridors = new();

    public HashSet<Vector2Int> allcorridors = new();

    public HashSet<Vector2Int> positionroom = new();

    public List<Vector2Int> endcorridors = new();

    public HashSet<Vector2Int> roomwalker = new();

    public HashSet<Vector2Int> delpositionroom = new();
    
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

    public void setstart_position(Vector2Int pos)
    {
        start_position = pos;
    }
    
    public void setstart_position_corridors(Vector2Int pos)
    {
        start_position_corridors = pos;
    }

    //direction
    public Direction dir;

    public List<Vector2Int> alldirection_diagonale => dir.alldirection_diagonale;
    public List<Vector2Int> alldirection => dir.alldirection;
    
    //boolean activate 
    public ActivateFonctionnalite af;
    public bool ActivateSquareRooms => af.ActivateSquareRooms;
    public bool StartRandomEachIteration => af.StartRandomEachIteration;
    public bool SoloRoomsActivate => af.SoloRoomsActivate;

    
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

            // Si la génération de la salle est activée (et pas encore au-delà de la limite)
            if (ActivateSquareRooms)
            {
                // Vérifie que la nouvelle position est dans les limites et qu'elle n'est pas déjà visitée
                if (limiteco(start_position, new_position, distance_walk / 2) && !walker.Contains(new_position))
                {
                    walker.Add(new_position);
                }
                else
                {
                    // Si la position dépasse la limite, on ne l'ajoute pas et on continue
                    new_position -= result;
                }
            }
            else
            {
                // Si pas de limites, on ajoute toutes les nouvelles positions
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
}
