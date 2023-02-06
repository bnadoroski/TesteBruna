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
    float spawnEnemiesTime;

    GameObject player;
    float minWidth = -10f;
    float maxWidth = 10f;

    private void Awake()
    {
        spawnEnemiesTime = GameManager.instance?.spawnEnemyTime ?? spawnEnemiesTime;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("SpawnShooterEnemies", 0.1f, spawnEnemiesTime);
        InvokeRepeating("SpawnChaserEnemies", 0.1f, spawnEnemiesTime);
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
