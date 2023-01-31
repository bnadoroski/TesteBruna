using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] 
    private float moveSpeed;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    int maxHealth = 100;
    [SerializeField]
    HealthBar healthBar;
    [SerializeField]
    int EnemyShootDamage;
    [SerializeField]
    int EnemyChaseDamage;
    [SerializeField]
    GameObject playerEffect;
    [SerializeField]
    GameObject gameManager;
    [SerializeField] 
    TMP_Text scoreText;
    Vector2 moveInput;
    Rigidbody2D rb2D;
    int currentHealth;
    int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        rb2D.velocity = -1 * transform.up * Mathf.Clamp01(moveInput.y) * Time.deltaTime * moveSpeed;
        RotatePlayer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CannonBallEnemy") )
        {
            CalculateDamage(GameManager.EnemyType.Shooter);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("EnemyChaser"))
        {
            collision.gameObject.GetComponent<EnemyChaserController>().DestroyChaser();
            CalculateDamage(GameManager.EnemyType.Chaser);
        }
    }

    private void CalculateDamage(GameManager.EnemyType enemy)
    {
        int damage;
        switch (enemy)
        {
            case GameManager.EnemyType.Shooter:
                damage = EnemyShootDamage;
                break;
            case GameManager.EnemyType.Chaser:
                damage = EnemyChaseDamage;
                break;
            default:
                damage = 0;
                break;
        }

        currentHealth = healthBar.TakeDamage(damage, currentHealth, gameObject);
        if (currentHealth <= 0)
        {
            playerEffect.GetComponent<PlayerEffectsController>().EnableEffect();
            gameManager.GetComponent<GameManager>().GameOver(score);
            Destroy(gameObject, 0.3f);
        }
    }

    private void RotatePlayer()
    {
        float rotation = -moveInput.x * rotationSpeed;
        transform.Rotate(Vector3.forward * rotation);
    }

    public void AddScore()
    {
        score++;
        scoreText.text = string.Format("Score: {0}", score);
    }
}
