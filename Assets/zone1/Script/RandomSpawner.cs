using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    //list de position de point de spawn
    public Transform[] spawnPoints;
    //list des ennemies
    public GameObject [] ennemyPrefabs;
    
    /// <summary>
    /// Spawn a random ennemy on a random position
    /// </summary>
    void Awake()
    {
            // recup un nombre entre 0 et nb Ennemie in list
            int randEnemy = Random.Range(0, ennemyPrefabs.Length);
            //recup un nb entre 0 et nb point de spawn
            int randSpawnPoint = Random.Range(0, spawnPoints.Length);
             //Pour crée ennemie(game object) qui est a l'indice [rand Enemy] de la list ennemie prefab a la position de l'indice de la position dans la liste
            GameObject newObject = Instantiate(ennemyPrefabs[randEnemy], spawnPoints[randSpawnPoint].position, transform.rotation);
            //rename ennemie cloner 
            newObject.name = ennemyPrefabs[randEnemy].name; 
    }
}
