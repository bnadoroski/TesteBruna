using UnityEngine;

public class PlayerEffectsController : MonoBehaviour
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
