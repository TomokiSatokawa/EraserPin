using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EraserControlBase : MonoBehaviour
{
    public int playerNumber;
    public GameObject TopPosition;
    public GameObject rightPosition;
    public GameObject leftPosition;
    public GameObject backPosition;
    public List<MeshCollider> colliders = new List<MeshCollider>();
    public GameObject coverObject;
    public GameObject handPosition;
    // Start is called before the first frame update
    public void Start()
    {
        DataReset();
    }
    public virtual void DataReset() { }
    public int GetPlayerNumber()
    {
        return playerNumber;
    }
    public float GetTopPosition()
    {
        return TopPosition.transform.position.y;
    }
    public virtual void StopProcess() { }
    public virtual void ChangeColor(Color color)
    {
        coverObject.GetComponent<MeshRenderer>().material.color = color;
    }
}
