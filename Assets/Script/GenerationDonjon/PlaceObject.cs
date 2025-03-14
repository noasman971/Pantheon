using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlaceObject : MonoBehaviour
{
    //var necessaire 
    public RoomsOrder ro;
    public List<(int, Vector2Int)> roomsorder => ro.roomsorder;
    public Dictionary<Vector2Int, List<Vector2Int>> allposrooms => ro.allposrooms;

    public CreateRoomsDonjon crd;
    public HashSet<Vector2Int> allcorridors => crd.allcorridors;
    
    //position objects
    public Dictionary<String, List<Vector2Int>> posobjects = new();
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
}
