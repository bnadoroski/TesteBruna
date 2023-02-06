using UnityEngine;

public class FIxedHealthBar : MonoBehaviour
{
    [SerializeField]
    Transform target;

    void Update()
    {
        if (target != null)
        {
            if (target.CompareTag("Player"))
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
