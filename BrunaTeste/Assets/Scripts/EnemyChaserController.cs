using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class EnemyChaserController : MonoBehaviour
{
    [SerializeField]
    float speed;
    [SerializeField]
    float increaseSpeedZone;
    [SerializeField]
    float rotationModifier;
    [SerializeField]
    GameObject enemyGraphic;
    [SerializeField]
    GameObject explosionEffect;
    [SerializeField]
    float nextWayPointDistance = 3f;
    [SerializeField]
    int maxHealth = 30;
    [SerializeField]
    HealthBar healthBar;

    Path path;
    int currentWayPoint = 0;
    int currentHealth;
    Seeker seeker;
    Rigidbody2D rb;
    Transform target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

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
            Vector3 vectorToTarget = target.transform.position - enemyGraphic.transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - rotationModifier;
            Quaternion quaternion = Quaternion.AngleAxis(angle, Vector3.forward);
            enemyGraphic.transform.rotation = Quaternion.Slerp(enemyGraphic.transform.rotation, quaternion, Time.deltaTime * speed);

            float distanceFromPlayer = 0;
            if (currentWayPoint < path.vectorPath.Count)
            {
                distanceFromPlayer = Vector2.Distance(target.transform.position, transform.position);
                if (distanceFromPlayer <= increaseSpeedZone)
                {
                    rb.drag = 0;
                }

                Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;
                Vector2 force = direction * speed * Time.deltaTime;
                rb.AddForce(force);
            }
            else
            {
                return;
            }

            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);

            if (distance < nextWayPointDistance)
            {
                currentWayPoint++;
            } 
        }
    }

    public void DestroyChaser()
    {
        Destroy(gameObject, 0.3f);
        explosionEffect.GetComponent<EnemiesEffectsController>().EnableEffect();
        target.GetComponent<PlayerController>().AddScore();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CannonBall"))
        {
            currentHealth = healthBar.TakeDamage(10, currentHealth, gameObject);
            if(currentHealth <= 0)
            {
                DestroyChaser();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, increaseSpeedZone);
    }
}
