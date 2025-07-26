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
    public List<GameObject> erasers;
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
    public TextMeshProUGUI roomPass;
    public TextMeshProUGUI roomTitle;
    public TextMeshProUGUI gameMode;
    public WarningMessage warningMessage;
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
        SetRoomOption();
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
        nextButton.interactable = (playerCount.Value + comCount.Value) <= 4;

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
        warningMessage.enabled = playerCount.Value + comCount.Value <= 1;
        nextButton.interactable = playerCount.Value + comCount.Value > 1;
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
        deviceUIObject[playerNumber - 1].GetComponent<DeviceUIControl>().ActiveOutline(true);
    }
    public void StpperClick()
    {
        SetDeviceNumber();
        photonView.RPC(nameof(ChangeUI), RpcTarget.All, _deviceNumber, playerCount.Value, comCount.Value);
        netWorkObj.GetComponent<PhotonView>().RPC("SetData", RpcTarget.All, _deviceNumber, playerCount.Value, comCount.Value);
        netWorkObj.GetComponent<PlaySettings>().SetLocalData(playerCount.Value, comCount.Value);

        int playerTotal = playerCount.Value + comCount.Value;
        int i = 1;
        foreach (GameObject obj in erasers)
        {
            obj.SetActive(i <= playerTotal);
            i++;
        }
    }
    public void ChangeListTure()
    {
        SetDeviceNumber();
        int dNumber = _deviceNumber -1;
        Debug.Log(dNumber);
        GameObject gameObject = deviceUIObject[dNumber];
        deviceUIObject[dNumber] = deviceUIObject[0];
        deviceUIObject[0] = gameObject;
        int i = 1;
        foreach(GameObject obj in deviceUIObject)
        {
            obj.GetComponent<DeviceUIControl>().deviceNumber = i;
            i++;
        }
    }
    [PunRPC]
    public void ChangeUI(int deviceNumber, int playerCount, int comCount)
    { 
        deviceUIObject[deviceNumber - 1].GetComponent<DeviceUIControl>().ChangeEraserUI(playerCount, comCount,deviceNumber);
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
        PlayerPrefs.SetInt("playerCount" + _deviceNumber, playerCount.Value);
        PlayerPrefs.SetInt("comCount" + _deviceNumber, comCount.Value);
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
        PhotonNetwork.CurrentRoom.IsOpen = false;
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
    public void SetRoomOption()
    {
        roomPass.text = PhotonNetwork.CurrentRoom.Name;
        roomTitle.text = PlayerPrefs.GetString("IsVisible");
        gameMode.text = PlayerPrefs.GetString("mode");
    }
}

