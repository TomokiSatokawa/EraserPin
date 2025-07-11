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
        Rigidbody rb = targetEraser.GetComponent<Rigidbody>();
        Log.text("Foce" + direction * power);
        rb.AddForce(direction * power/50, ForceMode.Impulse);
        rb.inertiaTensor = new Vector3(1f, 1f, 1f); // åyÇ≠Ç∑ÇÈï˚å¸Çí≤êÆ
        rb.inertiaTensorRotation = Quaternion.identity;
        rb.maxAngularVelocity = 50f;
        power *= 10;
        if(power / 10 < 0f)
        {
            power = 0f;
            rotate = Vector3.zero;
        }
        power /= 20;
        Log.text("Rotate" + rotate +""+ power);
        rb.AddTorque(rotate * power, ForceMode.Impulse);
        FindAnyObjectByType<GameManager>().Check();
    }
}
