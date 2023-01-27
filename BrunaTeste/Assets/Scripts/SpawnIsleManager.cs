using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnIsleManager : MonoBehaviour
{
    [SerializeField]
    float isleSpawnTimer;
    [SerializeField]
    List<GameObject> isles;

    GameObject player;

    float minWidth = -10f;
    float maxWidth = 10f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("SpawnIsles", 0.5f, isleSpawnTimer);
    }

    public void SpawnIsles()
    {
        if (player != null)
        {
            float spawnPosition = Random.Range(minWidth, maxWidth);
            Instantiate(isles[Random.Range(0, 2)], new Vector3(spawnPosition, transform.position.y, transform.position.z), Quaternion.identity);
        }
    }
}
