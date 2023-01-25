using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    [SerializeField] float Speed;

    public Vector3 shootDirection;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(shootDirection * Time.deltaTime * Speed);
        print("c " + shootDirection);
    }

    public void Shoot(Vector3 changeShootDirection)
    {
        print("a " + Vector3.down);
        shootDirection = new Vector3(changeShootDirection.x, changeShootDirection.y, changeShootDirection.z);
        print("b " + shootDirection);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
