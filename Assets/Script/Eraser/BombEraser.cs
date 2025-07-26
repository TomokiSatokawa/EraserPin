using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class BombEraser : HardEraserBase
{
    public float bombRange;
    public float flyPower;
    public GameObject effectObject;

    [PunRPC]
    public override void EraserEffect(int number)
    {
        base.EraserEffect(number);
        Debug.Log("”š”­");
        foreach(GameObject eraser in FindAnyObjectByType<EraserClone>().cloneEraserObjects)
        {
            if(eraser == this.gameObject || eraser == null)
            {
                continue;
            }
            if(Vector3.Distance(eraser.transform.position,this.transform.position) <= bombRange)
            {
                //•ûŒü * ã
                Vector3 direction = eraser.transform.position - this.transform.position;
                direction = direction.normalized;
                direction += Vector3.up * flyPower;
                direction /= 8;
                Log.text("”š•— : " + direction);
                eraser.GetComponent<Rigidbody>().AddForce(direction, ForceMode.Impulse);
                eraser.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(90, 360), Random.Range(90, 360), Random.Range(90, 360)),ForceMode.Impulse);
            }
        }
        Instantiate(effectObject, this.transform.position, Quaternion.identity);
    }
}
