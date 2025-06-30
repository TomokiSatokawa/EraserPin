using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;

public class KillCheck : MonoBehaviour
{
    public GameObject killEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        GameObject hitObject = other.gameObject.transform.parent.gameObject;
        if(hitObject.GetComponent<EraserControlBase>() != null)
        {
            EraserControlBase eraserData = hitObject.GetComponent<EraserControlBase>();
            Instantiate(killEffect, hitObject.transform.position, Quaternion.identity);
            FindAnyObjectByType<GameManager>().Kill(eraserData.playerNumber);
            PhotonNetwork.Destroy(hitObject);
        }
    }
}
