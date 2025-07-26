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
        loadText.SetText("ルーム作成中・・・");
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
            errorText.GetComponent<TextMeshProUGUI>().SetText("10文字以下にしてください。");
            return;
        }
        if(name == "")
        {
            errorText.SetActive(true);
            errorText.GetComponent<TextMeshProUGUI>().SetText("公開ルールの場合、名前の入力が必須です。");
            return;
        }
        if(FindRoom(name))
        {
            errorText.SetActive(true);
            errorText.GetComponent<TextMeshProUGUI>().SetText("その名前のルームはあります。");
            return;
        }
        gamemode = roomMode;
        errorText.SetActive(false);
        PhotonNetwork.CreateRoom(name, roomOptions);
        loadObject.SetActive(true);
        loadText.SetText("ルーム作成中・・・");
    }
    public void InRoom(InputPass roomPass)
    {
       
        PhotonNetwork.JoinRoom(roomPass.GetPass());
        loadObject.SetActive(true);
        loadText.SetText("ルームに接続中・・・");
    }
    public override void OnCreatedRoom()
    {
        
        FindAnyObjectByType<PlaySettings>().SetMode(gamemode);
    }
    public override void OnJoinedRoom()
    {


        //画面移動処理
        screenChange.OnClick(3);
        loadObject.SetActive(false);

        //テキスト
        if (PhotonNetwork.CurrentRoom.IsVisible)
        {
            PlayerPrefs.SetString("IsVisible", "ルーム名");
        }
        else
        {
            PlayerPrefs.SetString("IsVisible", "ルームパス");
        }

#if UNITY_EDITOR
        deviceName = "エディター" + PhotonNetwork.CurrentRoom.PlayerCount.ToString();
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

        //同期処理
        FindAnyObjectByType<DeviceView>().View(PhotonNetwork.CurrentRoom.PlayerCount);
        deviceViewPhoton.GetComponent<PhotonView>().RPC("View", RpcTarget.All, PhotonNetwork.CurrentRoom.PlayerCount);

        //個人
        
        
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {//roomに入った時
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