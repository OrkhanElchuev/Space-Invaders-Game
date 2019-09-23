using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [SerializeField] float BgScrollingSpeed = 1.0f;
    private Material bgMaterial;
    private Vector2 offSet;

    // Start is called before the first frame update
    void Start()
    {
        bgMaterial = GetComponent<Renderer>().material;
        offSet = new Vector2(0f, BgScrollingSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        bgMaterial.mainTextureOffset += offSet * Time.deltaTime;
    }
}
