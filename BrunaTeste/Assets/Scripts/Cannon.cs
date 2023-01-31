using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] 
    Transform transformSpawnBall;
    [SerializeField] 
    List<Transform> transformSpawnSpecialBallLeft;
    [SerializeField]
    List<Transform> transformSpawnSpecialBallRight;
    [SerializeField]
    GameObject cannonBall;
    [SerializeField]
    GameObject cannonEffect;
    [SerializeField]
    float fireRate;
    [SerializeField]
    float fireRateSides;

    float nextFireTime;
    float nextFireTimeE;
    float nextFireTimeSQ;


    void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        if (Input.GetKeyDown("space"))
        {
            if (nextFireTime < Time.time)
            {
                cannonBall.GetComponent<CannonBall>().Shoot(Vector3.right);
                cannonEffect.GetComponent<PlayerEffectsController>().EnableEffect();
                Instantiate(cannonBall, transformSpawnBall.position, transform.rotation);
                nextFireTime = Time.time + fireRate;
            }

        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (nextFireTimeE < Time.time)
            {
                foreach (Transform spawn in transformSpawnSpecialBallRight)
                {
                    cannonBall.GetComponent<CannonBall>().Shoot(Vector3.down);
                    Instantiate(cannonBall, spawn.position, transform.rotation);
                    nextFireTimeE = Time.time + fireRateSides;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (nextFireTimeSQ < Time.time)
            {
                foreach (Transform spawn in transformSpawnSpecialBallLeft)
                {
                    cannonBall.GetComponent<CannonBall>().Shoot(Vector3.up);
                    Instantiate(cannonBall, spawn.position, transform.rotation);
                    nextFireTimeSQ = Time.time + fireRateSides;
                }
            }
        }
    }
}
