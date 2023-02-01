using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FIxedHealthBar : MonoBehaviour
{
    [SerializeField]
    Transform target;

    void Update()
    {
        if (target != null)
        {
            if (target.tag == "Player")
                transform.position = target.position + transform.up * -1f;
            else
                transform.position = target.position + transform.up;
        } 
        else
        {
            Destroy(gameObject, 0.01f);
        }
    }
}
