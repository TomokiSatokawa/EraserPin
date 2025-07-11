using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEraser : EraserControlBase
{
    public float moveAmount;
    public float maxAmount;
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
    }
    public void Explosion()
    {
        Debug.Log("”š”­");
    }
}
