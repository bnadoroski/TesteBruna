using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnIsleManager : MonoBehaviour
{
    [SerializeField]
    float isleSpawnTimer;
    [SerializeField]
    List<GameObject> isles;

    RaycastHit2D[] raycasts;
    GameObject player;
    float minWidth = -10f;
    float maxWidth = 14f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("SpawnIsles", 0.2f, isleSpawnTimer);
    }

    public void SpawnIsles()
    {
        if (player != null)
        {
            GameObject isle = isles[Random.Range(0, 3)];
            Vector3 spawnPosition = GenerateSpawnPosition();
            if (CanSpawn(spawnPosition, isle.transform.position))
                Instantiate(isle, spawnPosition, Quaternion.identity);
            else
                SpawnIsles();
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        Vector3 spawnPosition = transform.position;
        spawnPosition.x = Random.Range(minWidth, maxWidth);

        return spawnPosition;
    }

    private bool CanSpawn(Vector2 position, Vector2 islePosition)
    {
        raycasts = Physics2D.RaycastAll(position + new Vector2(10, 10), islePosition);
        if (raycasts.Length > 0)
        {
            foreach (var item in raycasts)
            {
               if(item.collider != null)
                {
                    print(item.collider.name);
                }
            }
            return false;
        }

        return true;
    }
}
