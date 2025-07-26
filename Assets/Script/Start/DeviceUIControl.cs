using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Unity.VisualScripting;
using System;
public class DeviceUIControl : MonoBehaviourPunCallbacks
{
    public int deviceNumber;
    public TextMeshProUGUI deviceText;
    public List<EraserUIControl> eraserUIs;
    public GameObject readyRavel;
    public GameObject Outline;
    // Start is called before the first frame update
    void Start()
    {

       readyRavel.SetActive(false);
    }

    public void RavelActive()
    {
        readyRavel.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        string key = "name" + deviceNumber;
        string name = PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey(key)
            && PhotonNetwork.CurrentRoom.CustomProperties[key] is string n
            ? n
            : "";
        if(name == "" || deviceText.text != "")
        {
            return;
        }
        deviceText.SetText(name);
    }

    public void ChangeEraserUI(int playerCount, int comCount,int deviceNumber)
    {
        int i = 1;
        foreach (EraserUIControl obj in eraserUIs)
        {
            obj.Active(i <= playerCount + comCount);
            i++;
        }

        eraserUIs[0].IsCom(false);
        eraserUIs[1].IsCom(false);
        eraserUIs[2].IsCom(false);
        eraserUIs[3].IsCom(false);
        int n = comCount;

        if (playerCount == 1)
        {
            if (comCount >= 1)
            {
                eraserUIs[1].IsCom(true);
            }
            if (comCount >= 2)
            {
                eraserUIs[2].IsCom(true);

            }
            if (comCount == 3)
            {
                eraserUIs[3].IsCom(true);
            }


        }
        else if (playerCount == 2)
        {
            if (comCount >= 1)
            {
                eraserUIs[2].IsCom(true);
            }
            if (comCount >= 2)
            {
                eraserUIs[3].IsCom(true);

            }
        }
        else if (playerCount == 3)
        {
            if (comCount >= 1)
            {
                eraserUIs[3].IsCom(true);
            }
        }
    }
    public void ActiveOutline(bool b)
    {
        //Outline.SetActive(b);
    }
}
