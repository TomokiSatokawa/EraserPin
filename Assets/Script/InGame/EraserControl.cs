using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EraserControl : EraserControlBase
{
    public List<MeshCollider> colliders = new List<MeshCollider>();
    public GameObject coverObject;
    public GameObject handPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeColor(Color color)
    {
        coverObject.GetComponent<MeshRenderer>().material.color = color;
    }
}
