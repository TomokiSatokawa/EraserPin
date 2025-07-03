using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class EraserMove : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [PunRPC]
    public void Move(int eraserIndex, float power , Vector3 direction ,Vector3 rotate, Vector3 hitPosition)
    {
        if(PlayerPrefs.GetInt("Dnumber") != 1)
        {
            //return;
        }

        direction.y = 0;
        direction = direction.normalized;
        GameObject targetEraser = FindAnyObjectByType<EraserClone>().cloneEraserObjects[eraserIndex - 1];
        //direction = targetEraser.transform.TransformPoint(direction);
        //targetEraser.GetComponent<Rigidbody>().AddForce(direction * power);
        //hitPosition += targetEraser.transform.up;
        Vector3 local = targetEraser.transform.InverseTransformPoint(hitPosition);
        local *= 10;
        hitPosition = targetEraser.transform.TransformPoint(local);
        Rigidbody rb = targetEraser.GetComponent<Rigidbody>();
        Debug.Log("Foce" + direction * power);
        rb.AddForce(direction * power/50, ForceMode.Impulse);
        rb.inertiaTensor = new Vector3(1f, 0.1f, 1f); // Œy‚­‚·‚é•ûŒü‚ð’²®
        rb.inertiaTensorRotation = Quaternion.identity;
        rb.maxAngularVelocity = 10f;
        Debug.Log("Rotate" + rotate * power);
        if((rotate * power).magnitude >= 100)
        {
            rotate = Vector3.zero;
        }
        rb.AddTorque(rotate * power,ForceMode.Impulse);
        FindAnyObjectByType<GameManager>().Check();
    }
}
