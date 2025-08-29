using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAnimControl : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnEnable()
    {
        anim = GetComponent<Animator>();
        anim.Play("Load", 0, 0);
    }
}
