using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandControl : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject handObject;
    public Animator anim;
    public GameObject handPosition;
    // Start is called before the first frame update
    void Start()
    {
        Active(false);
        anim.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void MoveEraser()
    {
        gameManager.Move();

    }
    public void Active(bool a)
    {
        if (!a)
        {
            //anim.enabled = false;
            handObject.SetActive(false);
        }
    }
    public void Active(bool a, EraserControl eraserControl)
    {
        if (a)
        {
            //this.transform.position = eraserControl.handPosition.transform.position;
            //this.transform.LookAt(eraserControl.transform.position);
            //Vector3 rotation = this.transform.eulerAngles;
            //rotation.x = 0;
            //rotation.y += 180;
            //rotation.z = 0;
            //this.transform.eulerAngles = rotation;
            this.transform.position = handPosition.transform.position;
            this.transform.rotation = handPosition.transform.rotation;
            anim.enabled = true;
            handObject.SetActive(true);
            anim.Play("pin",0,0);
        }
    }
    public void End()
    {
        Active(false);
    }
}
