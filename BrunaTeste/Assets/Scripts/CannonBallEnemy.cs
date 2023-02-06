using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallEnemy : MonoBehaviour
{
    [SerializeField]
    ParticleSystem effect;
    [SerializeField] 
    float speed;

    Rigidbody2D cannonBallRb2d;
    GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        cannonBallRb2d = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        Vector2 moveDir = (target.transform.position - transform.position).normalized;
        cannonBallRb2d.velocity = new Vector2(moveDir.x, moveDir.y) * speed;
        StartCoroutine(DestructAfterTime());
    }

    IEnumerator DestructAfterTime()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(effect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
