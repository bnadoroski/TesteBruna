using System.Collections.Generic;
using UnityEngine;

public class SpawnIsleManager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> isles;
    [SerializeField]
    List<Transform> spawnPoints;
    [SerializeField]
    float distanceToAnotherSpawn;

    GameObject player;
    float distanceRemaining;
    List<int> spawnControll;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        distanceRemaining = player.transform.position.y + distanceToAnotherSpawn;
        spawnControll = new List<int>();
        Invoke("SpawnIsles", 0f);
    }

    private void Update()
    {
        if (player != null)
        {
            if (player.transform.position.y >= distanceRemaining)
                Invoke("SpawnIsles", 0f);
        }

    }

    public void SpawnIsles()
    {
        if (player != null)
        {
            int quantityIsleSpawn = Random.Range(1, 4);

            for (int i = 0; i < quantityIsleSpawn; i++)
            {
                GameObject isle = isles[Random.Range(0, 3)];
                Vector3 spawnPosition = GenerateSpawnPosition();
                Instantiate(isle, spawnPosition, Quaternion.identity);

                distanceRemaining = player.transform.position.y + distanceToAnotherSpawn;
                i++;
            }

            spawnControll = new List<int>();
        }
    }

    public Vector3 GenerateSpawnPosition()
    {
        int index;
        if (spawnControll.Count > 0)
        {
            index = Random.Range(0, spawnPoints.Count);
            if (spawnControll.Contains(index))
            {
                if (!spawnControll.Contains(1))
                    index = 0;
                else if (!spawnControll.Contains(2))
                    index = 1;
                else if (!spawnControll.Contains(3))
                    index = 2;
                else
                    index = 3;
            }
        }
        else
            index = Random.Range(0, spawnPoints.Count); 

        spawnControll.Add(index);
        return spawnPoints[index].position;
    }
}
