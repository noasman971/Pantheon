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
    public Dictionary<Vector2Int, List<Vector2Int>> allposroomsneight => ro.allposroomsneight;
    
    public List<(int,int)> interval = new();
    
    public CreateRoomsDonjon crd;
    public HashSet<Vector2Int> allcorridors => crd.allcorridors;

    public Direction dir;
    public List<Vector2Int> alldirection_diagonale => dir.alldirection_diagonale;
    
    public int maxAttempts ;
    
    //position objects
    public Dictionary<String, List<Vector2Int>> posobjects = new();

    public int compteurzone = 0;

    public HashSet<Vector2Int> allobjet = new HashSet<Vector2Int>() ;
    public HashSet<Vector2Int> dirobject = new HashSet<Vector2Int>();
    
    public List<Vector2Int> totalsroomsminmax(int min, int max)
    {
        
        List<Vector2Int> rooms = new List<Vector2Int>();
        for (int i = min; i <= max; i++)
        {
            foreach (var (roomcompteur, roomposstart) in roomsorder)
            {
                if (roomcompteur == i)
                {
                    rooms.Add(roomposstart);
                }
            }
        }
        return rooms;
    }

    public void intervalobject()
    {
        int total = 0;
        foreach (var (roomcompteur, roomposstart) in roomsorder)
        {
            if (total < roomcompteur)
            {
                total = roomcompteur;
            }
        }
        int totalzones = 2;
        int fixedfirstzonesize = 3;
        List<float> factorzone = new List<float> { 1f, 1.5f};
        float totalfactorzone = 0;
        foreach (var factor in factorzone)
        {
            totalfactorzone += factor;
        }
        int resterooms = total - fixedfirstzonesize - 1;
        
        List<int> allsize = new List<int>();
        
        allsize.Add((int)(resterooms * (factorzone[0] / totalfactorzone)));
        allsize.Add(resterooms - allsize[0]);

        int startroom = 1;
        int endroom = fixedfirstzonesize;
        
        interval.Add((startroom, endroom));

        for (int i = 0; i < totalzones; i++)
        {
            startroom = endroom + 1;
            endroom = startroom + allsize[i] - 1;
            interval.Add((startroom, endroom));
        }
        
        interval.Add((total, total));
        
        foreach (var (start, end) in interval)
        {
            compteurzone++;
        }
        
    }
    
    
    

    public void placeobjects(String objet, Vector2Int objectsize, int min, int max, int nbrobjetmin, int nbrobjetmax=-2, bool ActivateBorder = false, bool ActivateAround = false)
    {
        

        if (nbrobjetmax == -2)
        {
            nbrobjetmax = nbrobjetmin;
        }
        
        posobjects[objet] = new List<Vector2Int>();
        List<Vector2Int> roomsplaceobject= totalsroomsminmax(min, max);

        
        foreach (Vector2Int pos in roomsplaceobject)
        {
            if (!ActivateBorder)
            {
                foreach (var (roomposstart, allroompos) in allposrooms)
                {
                    if (roomposstart == pos)
                    {
                        int nbrobjectperrooms = Random.Range(nbrobjetmin, nbrobjetmax + 1);
                        int placedObjects = 0;
                        maxAttempts = 100000;
                        while (placedObjects < nbrobjectperrooms)
                        {
                            int posrandom = Random.Range(0, allroompos.Count);
                            Vector2Int findpos = allroompos[posrandom];
                            bool canplaceobjet = true;
                            dirobject.Clear();


                            if (objet == "caisse")
                            {
                                foreach (var dir in alldirection_diagonale)
                                {
                                    if (allobjet.Contains(findpos + dir))
                                    {
                                        canplaceobjet = false;
                                    }

                                    if (allcorridors.Contains(findpos + dir))
                                    {
                                        canplaceobjet = false;
                                    }

                                    if (!canplaceobjet)
                                    {
                                        break;
                                    }

                                    dirobject.Add(findpos + dir);
                                }
                            }

                            if (allobjet.Contains(findpos))
                            {
                                canplaceobjet = false;
                            }

                            if (allcorridors.Contains(findpos))
                            {
                                canplaceobjet = false;
                            }
                            

                            if (ActivateAround)
                            {
                                foreach (var dir in alldirection_diagonale)
                                {
                                    if (allobjet.Contains(findpos + dir))
                                    {
                                        canplaceobjet = false;
                                        Debug.Log("f");
                                    }
                                    if (!canplaceobjet)
                                    {
                                        break;
                                    }

                                    dirobject.Add(findpos + dir);
                                }
                                
                            }

                            if (canplaceobjet)
                            {
                                if (objet == "caisse")
                                {
                                    allobjet.UnionWith(dirobject);
                                }

                                allobjet.Add(findpos);
                                posobjects[objet].Add(findpos);
                                placedObjects++;
                            }

                            maxAttempts--;
                            if (maxAttempts == 0)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            else if (ActivateBorder)
            {
                foreach (var (roomposstart, allroompos) in allposroomsneight)
                {
                    if (roomposstart == pos)
                    {
                        int nbrobjectperrooms = Random.Range(nbrobjetmin, nbrobjetmax + 1);
                        int placedObjects = 0;
                        maxAttempts = 1000000;
                        while(placedObjects < nbrobjectperrooms)
                        {
                            int posrandom = Random.Range(0, allroompos.Count);
                            Vector2Int findpos = allroompos[posrandom];
                            bool doblebojet = false;
                            bool around = false;
                            HashSet<Vector2Int> dirobject = new HashSet<Vector2Int>();


                            if (objet == "caisse")
                            {
                                foreach (var dir in alldirection_diagonale)
                                {
                                    if (allobjet.Contains(findpos + dir))
                                    {
                                        doblebojet = true;
                                    }

                                    if (allcorridors.Contains(findpos + dir))
                                    {
                                        doblebojet = true;
                                    }

                                    if (doblebojet)
                                    {
                                        break;
                                    }
                                    dirobject.Add(findpos + dir);
                                }
                            }

                            if (allobjet.Contains(findpos))
                            {
                                doblebojet = true;
                            }

                            if (allcorridors.Contains(findpos))
                            {
                                doblebojet = true;
                            }
                            
                            
                            // if (ActivateAround)
                            // {
                            //     foreach (var dir in alldirection_diagonale)
                            //     {
                            //         foreach (var (nameobjet,allposobjet) in posobjects)
                            //         {
                            //             foreach (var posbjet in allposobjet)
                            //             {
                            //                 if (posbjet == findpos+dir)
                            //                 {
                            //                     around = true;
                            //                     break;
                            //                 }
                            //                 
                            //             }
                            //             if (doblebojet) break;
                            //         }
                            //     }
                            //     
                            // }
                            
                            if (!doblebojet)
                            {
                                if (objet == "caisse")
                                {
                                    allobjet.UnionWith(dirobject);
                                }
                                allobjet.Add(findpos);
                                posobjects[objet].Add(findpos);
                                placedObjects++;
                            }
                            
                            maxAttempts--;
                            if (maxAttempts == 0)
                            {
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}
