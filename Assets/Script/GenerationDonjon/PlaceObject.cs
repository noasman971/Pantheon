using System;
using System.Collections.Generic;
using System.Linq;
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

    public GenerationDonjon gd;
    
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
    
        var colorMapping = new Dictionary<int, Color>();
        
        foreach (var (roomRank, roomPos) in ro.roomsorder)
        {
            if (!colorMapping.ContainsKey(roomRank)) colorMapping[roomRank] = colors[roomRank % colors.Length];
            
        }

        int total = colorMapping.Count;
        
    
        int totalzones = 2;
        int fixedfirstzonesize = 2;
        List<float> factorzone = new List<float> { 1f, 1.5f };

        int resterooms = total - fixedfirstzonesize - 1;
        
        Debug.Log(total);
        Debug.Log(resterooms);

        int startroom = 1;
    
        interval.Add((startroom, fixedfirstzonesize));

        interval.Add((fixedfirstzonesize+1, resterooms/2+2));
        
        interval.Add((resterooms/2+2+1, total-2));
    
        interval.Add((total, total));

        foreach (var (pos1, pos2) in interval)
        {
            Debug.Log("_________");
            Debug.Log(pos1);
            Debug.Log(pos2);
        }
    
        compteurzone = interval.Count;
    }

    
    
    

    public void placeobjects(String objet, int min, int max, int nbrobjetmin, int nbrobjetmax=-2, bool ActivateBorder = false, bool ActivateAround = false)
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
                            List<Vector2Int> choosedir = new List<Vector2Int>();

                            if (objet == "caisse")
                            {
                                choosedir = alldirection_diagonale;
                            }
                            else if (objet == "wolf")
                            {
                                choosedir = dir.alldirection_wolf;
                            }

                            if (choosedir.Count != 0)
                            {
                                foreach (var dir in choosedir)
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
                                if (choosedir.Count != 0)
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
                        maxAttempts = 100000;
                        while (placedObjects < nbrobjectperrooms)
                        {
                            int posrandom = Random.Range(0, allroompos.Count);
                            Vector2Int findpos = allroompos[posrandom];
                            bool canplaceobjet = true;
                            dirobject.Clear();
                            List<Vector2Int> choosedir = new List<Vector2Int>();

                            if (objet == "caisse")
                            {
                                choosedir = alldirection_diagonale;
                            }
                            else if (objet == "wolf")
                            {
                                choosedir = dir.alldirection_wolf;
                            }

                            if (choosedir.Count != 0)
                            {
                                foreach (var dir in choosedir)
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
                                if (choosedir.Count != 0)
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
