using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] 
    Transform TransformSpawnBall;
    [SerializeField] 
    Transform[] TransformSpawnSpecialBallLeft;
    [SerializeField]
    Transform[] TransformSpawnSpecialBallRight;
    [SerializeField]
    GameObject CannonBall;

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        if (Input.GetKeyDown("space"))
        {
            CannonBall.GetComponent<CannonBall>().Shoot(Vector3.right);
            Instantiate(CannonBall, TransformSpawnBall.position, transform.rotation);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            foreach (Transform spawn in TransformSpawnSpecialBallRight)
            {
                CannonBall.GetComponent<CannonBall>().Shoot(Vector3.down);
                Instantiate(CannonBall, spawn.position, transform.rotation);
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            foreach (Transform spawn in TransformSpawnSpecialBallLeft)
            {
                CannonBall.GetComponent<CannonBall>().Shoot(Vector3.up);
                Instantiate(CannonBall, spawn.position, transform.rotation);
            }
        }
    }
}
