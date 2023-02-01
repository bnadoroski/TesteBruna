using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsleController : MonoBehaviour
{
    private Transform target;
    private Renderer isleRenderer;

    private void Start()
    {
        isleRenderer = GetComponentInChildren<Renderer>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        if (!isleRenderer.isVisible && target.position.y - 10 > gameObject.transform.position.y )
            Destroy(gameObject);
    }
}
