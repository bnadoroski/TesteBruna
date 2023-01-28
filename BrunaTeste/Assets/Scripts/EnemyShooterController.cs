using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyShooterController : MonoBehaviour
{
    [SerializeField]
    float speed;
    [SerializeField]
    float shootingRange;
    [SerializeField]
    float rotationModifier;
    [SerializeField]
    GameObject enemyCannon;
    [SerializeField]
    float nextWayPointDistance = 3f;
    Path path;
    int currentWayPoint = 0;

    Seeker seeker;
    Rigidbody2D rb;
    Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    void UpdatePath()
    {
        if (seeker.IsDone() && target != null)
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }

    void FixedUpdate()
    {
        if (path == null)
        {
            return;
        }

        if (target != null)
        {
            Vector3 vectorToTarget = target.transform.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - rotationModifier;
            Quaternion quaternion = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, quaternion, Time.deltaTime * speed);

            float distanceFromPlayer = 0;
            if (currentWayPoint < path.vectorPath.Count)
            {
                distanceFromPlayer = Vector2.Distance(target.transform.position, transform.position);
                if (distanceFromPlayer > shootingRange)
                {
                    Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;
                    Vector2 force = direction * speed * Time.deltaTime;

                    rb.AddForce(force);
                }
                else
                {
                    enemyCannon.GetComponent<EnemyCannon>().EnemyShoot();
                }
            }

            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);

            if (distance < nextWayPointDistance)
            {
                currentWayPoint++;
            }

            if (distanceFromPlayer < shootingRange)
            {
                enemyCannon.GetComponent<EnemyCannon>().EnemyShoot();
            }
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
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}
