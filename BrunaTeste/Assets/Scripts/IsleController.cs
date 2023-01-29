using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsleController : MonoBehaviour
{
    private Renderer isleRenderer;
    private bool hasAppeared = false;

    private void Start()
    {
        isleRenderer = GetComponentInChildren<Renderer>();
    }

    private void LateUpdate()
    {
        if (isleRenderer.isVisible)
            hasAppeared = true;
        if (!isleRenderer.isVisible && hasAppeared)
            Destroy(gameObject);
    }
}
