using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    [SerializeField]
    public float speed;

    private Material currentMaterial;
    private float offset;
    private bool offsetLimitReached = false;

    // Start is called before the first frame update
    void Start()
    {
        currentMaterial = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (!offsetLimitReached)
        {
            offset += 0.001f;
            offsetLimitReached = offset >= 0.1f;
        }
        else
        {
            offset -= 0.001f; 
            offsetLimitReached = !(offset <= 0.001f);
        }

        currentMaterial.SetTextureOffset("_MainTex", new Vector2(offset * speed, offset * speed));
    }

}
