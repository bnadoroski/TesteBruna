using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsleController : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            Destroy(collision.gameObject, 0.1f);
    }
}
