using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    Sprite lowLifeSprite;
    [SerializeField]
    Sprite mediumLifeSprite;
    [SerializeField]
    GamePlayManager gamePlayManager;
    Vector2 moveInput;
    Rigidbody2D rb2D;
    int currentHealth;

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
        if (transform.rotation.eulerAngles.z > 90 && transform.rotation.eulerAngles.z < 270)
            rb2D.velocity = -1 * transform.up * Mathf.Clamp01(moveInput.y) * Time.deltaTime * moveSpeed;
        else
            rb2D.velocity = new Vector2(0.01f, 0.01f);

        RotatePlayer();
    }

    private void CalculateDamage(GamePlayManager.EnemyType enemy)
    {
        int damage;
        switch (enemy)
        {
            case GamePlayManager.EnemyType.Shooter:
                damage = EnemyShootDamage;
                break;
            case GamePlayManager.EnemyType.Chaser:
                damage = EnemyChaseDamage;
                break;
            default:
                damage = 0;
                break;
        }

        currentHealth = healthBar.TakeDamage(damage, currentHealth, gameObject);

        if (currentHealth < (maxHealth * 0.6f))
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = mediumLifeSprite;
        }

        if (currentHealth == 0)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = lowLifeSprite;
            playerEffect.GetComponent<PlayerEffectsController>().EnableEffect();
            gamePlayManager.GameOver();
            Destroy(gameObject, 0.3f);
        }
    }

    private void RotatePlayer()
    {
        float rotation = -moveInput.x * rotationSpeed;
        transform.Rotate(Vector3.forward * rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CannonBallEnemy"))
        {
            CalculateDamage(GamePlayManager.EnemyType.Shooter);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("EnemyChaser"))
        {
            collision.gameObject.GetComponent<EnemyChaserController>().DestroyChaser();
            CalculateDamage(GamePlayManager.EnemyType.Chaser);
        }
    }
}
