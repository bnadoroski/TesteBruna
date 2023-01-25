using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    [SerializeField]
    public float Speed;

    private Material CurrentMaterial;
    private float Offset;
    private bool OffsetLimitReached = false;

    // Start is called before the first frame update
    void Start()
    {
        CurrentMaterial = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (!OffsetLimitReached)
        {
            Offset += 0.001f;
            OffsetLimitReached = Offset >= 0.1f;
        }
        else
        {
            Offset -= 0.001f; 
            OffsetLimitReached = !(Offset <= 0.001f);
        }

        CurrentMaterial.SetTextureOffset("_MainTex", new Vector2(Offset * Speed, Offset * Speed));
    }

}
