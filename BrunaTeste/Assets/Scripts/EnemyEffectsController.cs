using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEffectsController : MonoBehaviour
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