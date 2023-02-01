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
    [SerializeField]
    GameObject cannonEffect;

    float nextFireTime;

    public void EnemyShoot()
    {
        if (nextFireTime < Time.time)
        {
            cannonEffect.GetComponent<EnemyEffectsController>().EnableEffect();
            Instantiate(enemyCannonBall, transformSpawnBall.position, Quaternion.identity);
            nextFireTime = Time.time + fireRate;
        }
    }
}
