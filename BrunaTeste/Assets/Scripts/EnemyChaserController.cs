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
    [SerializeField]
    Sprite lowLifeSprite;
    [SerializeField]
    Sprite mediumLifeSprite;


    InterfaceController uiController;
    Path path;
    int currentWayPoint = 0;
    int currentHealth;
    Seeker seeker;
    Rigidbody2D rb;
    Transform target;
    Renderer enemyRenderer;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        enemyRenderer = GetComponentInChildren<Renderer>();
        uiController = GameObject.FindGameObjectWithTag("Interface")?.GetComponent<InterfaceController>();
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
                } else
                {
                    rb.drag = 1.5f;
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

    private void LateUpdate()
    {
        if (enemyRenderer != null && target != null)
            if (!enemyRenderer.isVisible && target.position.y - 20 > gameObject.transform.position.y)
                Destroy(gameObject);
    }

    public void DestroyChaser()
    {
        Destroy(gameObject, 0.3f);
        explosionEffect.GetComponent<EnemyEffectsController>().EnableEffect();
        uiController.AddScore();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CannonBall"))
        {
            currentHealth = healthBar.TakeDamage(10, currentHealth, gameObject);

            if (currentHealth < (maxHealth * 0.5f))
            {
                enemyGraphic.GetComponent<SpriteRenderer>().sprite = mediumLifeSprite;
            }

            if (currentHealth == 0)
            {
                enemyGraphic.GetComponent<SpriteRenderer>().sprite = lowLifeSprite;
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
