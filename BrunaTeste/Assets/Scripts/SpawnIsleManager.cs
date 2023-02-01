using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnIsleManager : MonoBehaviour
{
    [SerializeField]
    float isleSpawnTimer;
    [SerializeField]
    List<GameObject> isles;
    [SerializeField]
    Collider2D[] colliders;
    [SerializeField]
    float radius;

    GameObject player;

    float minWidth = -10f;
    float maxWidth = 14f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("SpawnIsles", 0.2f, isleSpawnTimer);
    }

    private bool PreventSpawnOverLap(Vector3 spawnPosition)
    {
        colliders = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach (Collider2D collider in colliders)
        {
            Vector3 centerPoint = collider.bounds.center;
            float width = collider.bounds.center.x;
            float height = collider.bounds.center.y;

            float leftExtend = centerPoint.x - width;
            float rightExtend = centerPoint.x + width;
            float lowerExtent = centerPoint.y - height;
            float upperExtent = centerPoint.y + height;

            if (spawnPosition.x >= leftExtend && spawnPosition.x <= rightExtend)
            {
                if(spawnPosition.y >= lowerExtent && spawnPosition.y <= upperExtent) {
                    return false;
                }
            }
        }
        return true;
    }

    public void SpawnIsles()
    {
        if (player != null)
        {
            bool canSpawnHere = false;
            Vector3 spawnPosition = transform.position;
            while (!canSpawnHere) {
                float spawnPositionX = Random.Range(minWidth, maxWidth);
                spawnPosition = new Vector3(spawnPositionX, transform.position.y, transform.position.z);
                canSpawnHere = PreventSpawnOverLap(spawnPosition);

                if (canSpawnHere)
                    break;
            }

            Instantiate(isles[Random.Range(0, 3)], spawnPosition, Quaternion.identity);
        }
    }


}
