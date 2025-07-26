using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEditor;
public class RoomManager : MonoBehaviourPunCallbacks
{
    public GameObject loadObject;
    public TextMeshProUGUI loadText;
    public TextMeshProUGUI roomPas;
    public TextMeshProUGUI masterText;
    public GameObject deviceViewPhoton;
    public GameObject errorText;
    public GameObject content;
    public GameObject clonePrefab;
    private ScreenChange screenChange;
    private int deviceNumber;
    private string deviceName;
    private List<RoomInfo> roomList;
    private List<GameObject> clonedList = new List<GameObject>();
    private mode gamemode;
    public enum mode
    {
        Normal,Hard
    }
    // Start is called before the first frame update
    void Start()
    {
        screenChange = FindAnyObjectByType<ScreenChange>();
    }
    public bool FindRoom(string name)
    {
        bool found = false;
        foreach(RoomInfo roomInfo in roomList)
        {
            if(roomInfo.Name == name)
            {
                found = true;
            }
            
        }
        return found;
    }
    public override void OnRoomListUpdate(List<RoomInfo> list)
    {
        roomList = list;
    }
    public void CreateRandomRoom(RoomOptions roomOptions,mode roomMode)
    {
        //Debug.Log("Create mode : " + roomOptions);
        gamemode = roomMode;
        PhotonNetwork.CreateRoom(Random.Range(0, 9999).ToString("0000"), roomOptions);
        loadObject.SetActive(true);
        loadText.SetText("���[���쐬���E�E�E");
    }
    public void RoomList()
    {
        foreach (GameObject obj in clonedList)
        {
            Destroy(obj);
        }
        clonedList.Clear();
        foreach (RoomInfo roomInfo in roomList)
        {
            RoomPanel newPanel  = Instantiate(clonePrefab,content.transform).GetComponent<RoomPanel>();
            newPanel.DataSet(roomInfo);
            newPanel.roomManager = this.gameObject.GetComponent<RoomManager>();
            clonedList.Add(newPanel.gameObject);
        }
    }
    public void CreateOpenRoom(RoomOptions roomOptions, mode roomMode,string name)
    {
        if (name.Length > 10)
        {
            errorText.SetActive(true);
            errorText.GetComponent<TextMeshProUGUI>().SetText("10�����ȉ��ɂ��Ă��������B");
            return;
        }
        if(name == "")
        {
            errorText.SetActive(true);
            errorText.GetComponent<TextMeshProUGUI>().SetText("���J���[���̏ꍇ�A���O�̓��͂��K�{�ł��B");
            return;
        }
        if(FindRoom(name))
        {
            errorText.SetActive(true);
            errorText.GetComponent<TextMeshProUGUI>().SetText("���̖��O�̃��[���͂���܂��B");
            return;
        }
        gamemode = roomMode;
        errorText.SetActive(false);
        PhotonNetwork.CreateRoom(name, roomOptions);
        loadObject.SetActive(true);
        loadText.SetText("���[���쐬���E�E�E");
    }
    public void InRoom(InputPass roomPass)
    {
       
        PhotonNetwork.JoinRoom(roomPass.GetPass());
        loadObject.SetActive(true);
        loadText.SetText("���[���ɐڑ����E�E�E");
    }
    public override void OnCreatedRoom()
    {
        
        FindAnyObjectByType<PlaySettings>().SetMode(gamemode);
    }
    public override void OnJoinedRoom()
    {


        //��ʈړ�����
        screenChange.OnClick(3);
        loadObject.SetActive(false);

        //�e�L�X�g
        if (PhotonNetwork.CurrentRoom.IsVisible)
        {
            PlayerPrefs.SetString("IsVisible", "���[����");
        }
        else
        {
            PlayerPrefs.SetString("IsVisible", "���[���p�X");
        }

#if UNITY_EDITOR
        deviceName = "�G�f�B�^�[" + PhotonNetwork.CurrentRoom.PlayerCount.ToString();
#elif UNITY_WEBGL
        deviceName = "Web" + PhotonNetwork.CurrentRoom.PlayerCount.ToString();
#else
        deviceName = SystemInfo.deviceType.ToString() + PhotonNetwork.CurrentRoom.PlayerCount.ToString();
#endif
        masterText.SetText(deviceName);
        PlaySettings playSettings = this.gameObject.GetComponent<PlaySettings>();
        playSettings.NameSet(PhotonNetwork.CurrentRoom.PlayerCount, deviceName);

        deviceNumber = PhotonNetwork.CurrentRoom.PlayerCount;
        deviceViewPhoton.GetComponent<DeviceView>().OutLine(deviceNumber);
        playSettings.ResetData();

        FindAnyObjectByType<DeviceView>().ChangeListTure();

        //��������
        FindAnyObjectByType<DeviceView>().View(PhotonNetwork.CurrentRoom.PlayerCount);
        deviceViewPhoton.GetComponent<PhotonView>().RPC("View", RpcTarget.All, PhotonNetwork.CurrentRoom.PlayerCount);

        //�l
        
        
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {//room�ɓ�������
        deviceViewPhoton.GetComponent<PhotonView>().RPC("View", RpcTarget.All, PhotonNetwork.CurrentRoom.PlayerCount);
    }
    public string GetName()
    {
        return deviceName;
    }
    public int GetNumber()
    {
        return deviceNumber;
    }
    public override void OnLeftRoom()
    {
        //int playerCount = this.gameObject.GetComponent <PlaySettings>().GetPlayerCount();
        //deviceViewPhoton.GetComponent<PhotonView>().RPC("ChangeUI", RpcTarget.All, PhotonNetwork.CurrentRoom.PlayerCount, playerCount, 0);
    }
}