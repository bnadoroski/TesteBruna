using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesEffectsController : MonoBehaviour
{
    public void EnableEffect()
    {
        gameObject.SetActive(true);
    }

    public void DisableEffect()
    {
        gameObject.SetActive(false);
    }
}
