using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] List<Transform> spawnPoints;
    [SerializeField] GameObject enemy;
    [SerializeField] int maxEnemies;

    GameObject player;
    int actuallyEnemies = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("SpawnEnemies", 0.5f, 1f);
    }

    void SpawnEnemies()
    {
        if (player != null && actuallyEnemies < maxEnemies)
        {
            int index = Random.Range(0, spawnPoints.Count);
            print(index);
            Instantiate(enemy, spawnPoints[index].position, Quaternion.identity);
            actuallyEnemies++;
        }
    }
}
