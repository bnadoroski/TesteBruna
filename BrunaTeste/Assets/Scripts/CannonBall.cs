using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    [SerializeField] 
    float speed;
    [SerializeField] 
    ParticleSystem effect;
    public Vector3 shootDirection;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(shootDirection * Time.deltaTime * speed);
    }

    public void Shoot(Vector3 changeShootDirection)
    {
        shootDirection = new Vector3(changeShootDirection.x, changeShootDirection.y, changeShootDirection.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(effect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
