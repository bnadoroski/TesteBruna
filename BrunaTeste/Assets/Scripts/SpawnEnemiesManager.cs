using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemiesManager : MonoBehaviour
{
    [SerializeField] 
    List<Transform> spawnPointsShooters;
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

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("SpawnShooterEnemies", shooterSpawnTimer, shooterSpawnTimer);
        //InvokeRepeating("SpawnChaserEnemies", chaserSpawnTimer, chaserSpawnTimer);
    }

    void SpawnShooterEnemies()
    {
        if (player != null)
        {
            int index = Random.Range(0, spawnPointsShooters.Count);
            Instantiate(shooter, spawnPointsShooters[index].position, Quaternion.identity);
        }
    }

    //void SpawnChaserEnemies()
    //{
    //    if (player != null)
    //    {
    //        int index = Random.Range(0, spawnPointsChasers.Count);
    //        Instantiate(chaser, spawnPointsChasers[index].position, Quaternion.identity);
    //    }
    //}
}
