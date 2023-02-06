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

    void Update()
    {
        transform.Translate(shootDirection * Time.deltaTime * speed); 
        StartCoroutine(DestructAfterTime());
    }

    IEnumerator DestructAfterTime()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

    public void Shoot(Vector3 changeShootDirection)
    {
        shootDirection = new Vector3(changeShootDirection.x, changeShootDirection.y, changeShootDirection.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            Instantiate(effect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
