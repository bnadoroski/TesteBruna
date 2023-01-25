using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] 
    private float MoveSpeed;
    [SerializeField]
    private float RotationSpeed;
    [SerializeField]
    public Camera MainCam;

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
        MainCam.transform.position = new Vector3(MainCam.transform.position.x, transform.position.y, MainCam.transform.position.z);
    }

    private void FixedUpdate()
    {
        rb2D.velocity = -1 * transform.up * Mathf.Clamp01(moveInput.y) * Time.deltaTime * MoveSpeed;
        RotatePlayer();
    }

    private void RotatePlayer()
    {
        float rotation = -moveInput.x * RotationSpeed;
        transform.Rotate(Vector3.forward * rotation);
    }
}
