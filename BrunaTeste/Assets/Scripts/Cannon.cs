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
    float fireRate;

    float nextFireTime;
    // Update is called once per frame
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
                Instantiate(cannonBall, transformSpawnBall.position, transform.rotation);
                nextFireTime = Time.time + fireRate;
            }

        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            foreach (Transform spawn in transformSpawnSpecialBallRight)
            {
                cannonBall.GetComponent<CannonBall>().Shoot(Vector3.down);
                Instantiate(cannonBall, spawn.position, transform.rotation);
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            foreach (Transform spawn in transformSpawnSpecialBallLeft)
            {
                cannonBall.GetComponent<CannonBall>().Shoot(Vector3.up);
                Instantiate(cannonBall, spawn.position, transform.rotation);
            }
        }
    }
}
