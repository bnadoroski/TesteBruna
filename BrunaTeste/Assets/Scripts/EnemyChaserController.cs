using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaserController : MonoBehaviour
{
    [SerializeField]
    float speed;
    [SerializeField]
    float decreaseSpeedZone;
    [SerializeField]
    float rotationModifier;

    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (player != null)
        {
            float distanceFromPlayer = Vector2.Distance(player.transform.position, transform.position);
            float updateSpeed = speed;
            if (distanceFromPlayer <= decreaseSpeedZone)
            {
                updateSpeed = speed / 2;
            }

            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, updateSpeed * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        if (player != null)
        {
            Vector3 vectorToTarget = player.transform.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - rotationModifier;
            Quaternion quaternion = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, quaternion, Time.deltaTime * speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CannonBall"))
        {
            Destroy(gameObject, 0.1f);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, decreaseSpeedZone);
    }
}
