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
    public Camera mainCam;

    Vector2 moveInput;
    Rigidbody2D rb2D;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");
        mainCam.transform.position = new Vector3(mainCam.transform.position.x, transform.position.y, mainCam.transform.position.z);
    }

    private void FixedUpdate()
    {
        rb2D.velocity = -1 * transform.up * Mathf.Clamp01(moveInput.y) * Time.deltaTime * moveSpeed;
        RotatePlayer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CannonBallEnemy") || collision.CompareTag("EnemyChaser"))
        {
            Destroy(gameObject, 0.1f);
        }
    }

    private void RotatePlayer()
    {
        float rotation = -moveInput.x * rotationSpeed;
        transform.Rotate(Vector3.forward * rotation);
    }
}
