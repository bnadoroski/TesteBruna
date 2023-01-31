using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemiesManager : MonoBehaviour
{
    [SerializeField] 
    Transform spawnPointShooters;
    [SerializeField] 
    List<Transform> spawnPointsChasers;
    [SerializeField] 
    GameObject shooter;
    [SerializeField] 
    GameObject chaser;
    [SerializeField]
    float shooterSpawnTimer;
    [SerializeField]
    float chaserSpawnTimer;

    GameObject player;
    float minWidth = -10f;
    float maxWidth = 10f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("SpawnShooterEnemies", shooterSpawnTimer, shooterSpawnTimer);
        InvokeRepeating("SpawnChaserEnemies", chaserSpawnTimer, chaserSpawnTimer);
    }

    void SpawnShooterEnemies()
    {
        if (player != null)
        {
            float spawnPosition = Random.Range(minWidth, maxWidth);
            Instantiate(shooter, new Vector3(spawnPosition, spawnPointShooters.position.y, spawnPointShooters.position.z), Quaternion.identity); 
        }
    }

    void SpawnChaserEnemies()
    {
        if (player != null)
        {
            int index = Random.Range(0, spawnPointsChasers.Count);
            Instantiate(chaser, spawnPointsChasers[index].position, Quaternion.identity);
        }
    }
}
