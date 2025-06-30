using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using System.ComponentModel;
using TMPro;

public class DeviceView : MonoBehaviourPunCallbacks
{
    public List<GameObject> deviceUIObject;
    public StepperControl playerCount;
    public StepperControl comCount;
    public GameObject netWorkObj;
    public PhotonView canvasPhoton;
    public Button nextButton;
    public GameObject nextWindowButton;
    public PlaySettings playSettings;
    private int _deviceNumber = 0;
    private bool ready = false;
    private bool isInRoom = false;
    public TextMeshProUGUI textMeshProUGUI;
    public void Awake()
    {
        foreach (GameObject obj in deviceUIObject)
        {
            obj.SetActive(false);
        }
    }
    public void Start()
    {
        nextButton.interactable = true;
        nextWindowButton.SetActive(false) ;
        CountChange();
    }


    private void Update()
    {
        if (!isInRoom)
        {
            InRoomChenge();
            isInRoom = true;
        }
        SetDeviceNumber();
        if (!ready)
        {
        nextButton.interactable = (playerCount.GetData() + comCount.GetData()) <= 4;

        }
        bool b = true;
        for (int i = 1; i <= PhotonNetwork.CurrentRoom.PlayerCount; i++)
        {
            int ready = (PhotonNetwork.CurrentRoom.CustomProperties["ready" + "" + i.ToString()] is int a) ? a : 3;
            if(ready == 0)
            {
                b = false;
                break;
            }
        }
        nextWindowButton.GetComponent<Button>().interactable = b;
    }

    [PunRPC]
    public void View(int deviceNumber)
    {
        int i = 1;
        foreach (GameObject obj in deviceUIObject)
        {
            if (i > deviceNumber)
            {
                break;
            }
            deviceUIObject[i - 1].SetActive(true);
            i++;
        }
    }
    public void OutLine(int playerNumber)
    {
        deviceUIObject[playerNumber - 1].GetComponent<Outline>().enabled = true;
    }
    public void StpperClick()
    {
        SetDeviceNumber();
        photonView.RPC(nameof(ChangeUI), RpcTarget.All, _deviceNumber, playerCount.GetData(), comCount.GetData());
        netWorkObj.GetComponent<PhotonView>().RPC("SetData", RpcTarget.All, _deviceNumber, playerCount.GetData(), comCount.GetData());
        netWorkObj.GetComponent<PlaySettings>().SetLocalData(playerCount.GetData(), comCount.GetData());
    }
    [PunRPC]
    public void ChangeUI(int deviceNumber, int playerCount, int comCount)
    {
        deviceUIObject[deviceNumber - 1].GetComponent<DeviceUIControl>().ChangeEraserUI(playerCount, comCount);
    }
    public void InRoomChenge()
    {
        SetDeviceNumber();

        
        for (int i = 1; i < _deviceNumber; i++)
        { 
            //€”õ
            int active = (PhotonNetwork.CurrentRoom.CustomProperties["ready" + "" + i.ToString()] is int a) ? a : 3;
            if (active == 1)
            {
               RavelView(i);
            }
            else if(active == 3) 
            {
                Debug.Log("A");
            }
        }
    }
    public void CountChange()
    {
        SetDeviceNumber();

        for (int i = 1; i < _deviceNumber; i++)
        {
            //l”
            int playerCount = (PhotonNetwork.CurrentRoom.CustomProperties["playerCount" + "" + i.ToString()] is int p) ? p : 0;
            int comCount = (PhotonNetwork.CurrentRoom.CustomProperties["comCount" + "" + i.ToString()] is int c) ? c : 0;
            PlaySettings playSettings = FindAnyObjectByType<PlaySettings>();
            playSettings.SetData(i, playerCount, comCount);
            //deviceUIObject[i -1].GetComponent<DeviceUIControl>().ChangeEraserUI(playerCount,comCount);
            ChangeUI(i, playerCount, comCount);
           
        }
    }
    public void NextButtonClick()
    {
        PlayerPrefs.SetInt("playerCount" + _deviceNumber, playerCount.GetData());
        PlayerPrefs.SetInt("comCount" + _deviceNumber, comCount.GetData());
        PlayerPrefs.Save();
        playerCount.Active(false);
        comCount.Active(false);
        ready = true;
        nextButton.interactable = false;
        photonView.RPC(nameof(RavelView), RpcTarget.All,_deviceNumber);
        playSettings.Ready(_deviceNumber,1);
        if (_deviceNumber == 1)
        {
            nextWindowButton.SetActive(true);
        }
        textMeshProUGUI.text += PlayerPrefs.GetInt("playerCount" + _deviceNumber);
        textMeshProUGUI.text += PlayerPrefs.GetInt("comCount" + _deviceNumber);
    }
    [PunRPC]
    public void RavelView(int playerNumber)
    {
        deviceUIObject[playerNumber -1].GetComponent<DeviceUIControl>().RavelActive();
    }
    public void CharacterChoiceMove()
    {
        canvasPhoton.RPC("OnClick", RpcTarget.All, 4);
    }
    public void SetDeviceNumber()
    {
        if (_deviceNumber == 0)
        {
            _deviceNumber = FindAnyObjectByType<RoomManager>().GetNumber();
            PlayerPrefs.SetInt("Dnumber", _deviceNumber);
            PlayerPrefs.Save();
        }
    }
}
