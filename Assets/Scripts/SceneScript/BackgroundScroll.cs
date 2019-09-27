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
        // Scrolling up on Y axis
        offSet = new Vector2(0f, BgScrollingSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        // Applying frame rate independent moving speed
        bgMaterial.mainTextureOffset += offSet * Time.deltaTime;
    }
}
