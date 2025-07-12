using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEraser : EraserControlBase
{
    public float moveAmount;
    public float maxAmount;
    public float bombRange;
    public float flyPower;
    public GameObject effectObject;
    private Vector3 eraserPosition;
    // Update is called once per frame
    void Update()
    {
        
    }
    public override void DataReset()
    {
        moveAmount = 0;
        eraserPosition = this.transform.position;
    }
    public override void StopProcess()
    {
        moveAmount += Vector3.Distance(eraserPosition, this.transform.position);
        if (moveAmount > maxAmount)
        {
            Explosion();
        }
        eraserPosition = this.transform.position;
        FindAnyObjectByType<StopCheck>().EffectCheck(playerNumber);
    }
    public void Explosion()
    {
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
