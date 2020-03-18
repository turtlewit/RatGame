using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMaterial : MonoBehaviour
{
    [SerializeField]
    RenderTexture tex;

    // Start is called before the first frame update
    void Start()
    {
        //Camera cam = GameObject.FindWithTag("DepthCamera").GetComponent<Camera>();
        Camera cam = Camera.main;
        tex.width = cam.pixelWidth;
        tex.height = cam.pixelHeight;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
