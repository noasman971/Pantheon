using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject [] ennemyPrefabs;
    int compteur = 0;

    void Update()
    {
        
        if (compteur ==0)
        {
            int randEnemy = Random.Range(0, ennemyPrefabs.Length);
            int randSpawnPoint = Random.Range(0, spawnPoints.Length);
            
             GameObject newObject = Instantiate(ennemyPrefabs[randEnemy], spawnPoints[randSpawnPoint].position, transform.rotation);
             newObject.name = ennemyPrefabs[randEnemy].name; 
            compteur = 1;
        }
    }
}
