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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int GetPlayerNumber()
    {
        return playerNumber;
    }
    public float GetTopPosition()
    {
        return TopPosition.transform.position.y;
    }
}
