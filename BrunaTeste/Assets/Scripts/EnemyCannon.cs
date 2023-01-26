using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCannon : MonoBehaviour
{
    [SerializeField]
    Transform transformSpawnBall;
    [SerializeField]
    GameObject enemyCannonBall;
    [SerializeField]
    float fireRate;

    float nextFireTime;

    public void EnemyShoot()
    {
        if (nextFireTime < Time.time)
        {
            Instantiate(enemyCannonBall, transformSpawnBall.position, Quaternion.identity);
            nextFireTime = Time.time + fireRate;
        }
    }
}
