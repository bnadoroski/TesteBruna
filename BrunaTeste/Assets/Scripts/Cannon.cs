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

    Animator recoilAnimator;
    float nextFireTime;
    float nextFireTimeE;
    float nextFireTimeQ;

    private void Start()
    {
        recoilAnimator = GetComponent<Animator>();
    }

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
                cannonEffect.GetComponent<PlayerEffectsController>().EnableEffect();
                InstantiateShoot(Vector3.right, transformSpawnBall.position);
                nextFireTime = Time.time + fireRate;
                recoilAnimator.SetTrigger("Shoot");
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (nextFireTimeE < Time.time)
            {
                foreach (Transform spawn in transformSpawnSpecialBallRight)
                {
                    InstantiateShoot(Vector3.down, spawn.position);
                    nextFireTimeE = Time.time + fireRateSides;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (nextFireTimeQ < Time.time)
            {
                foreach (Transform spawn in transformSpawnSpecialBallLeft)
                {
                    InstantiateShoot(Vector3.up, spawn.position);
                    nextFireTimeQ = Time.time + fireRateSides;
                }
            }
        }
    }

    void InstantiateShoot(Vector3 direction, Vector3 position)
    {
        cannonBall.GetComponent<CannonBall>().Shoot(direction);
        Instantiate(cannonBall, position, transform.rotation);
    }
}
