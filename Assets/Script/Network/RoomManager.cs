using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEditor;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
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
    public GameObject nullText;
    private ScreenChange screenChange;
    private int deviceNumber;
    private string deviceName;
    private List<RoomInfo> roomList;
    private List<GameObject> clonedList = new List<GameObject>();
    private InputPass inputPass;
    public mode gamemode;
    public enum mode
    {
        Normal, Hard
    }
    // Start is called before the first frame update
    void Start()
    {
        screenChange = FindAnyObjectByType<ScreenChange>();
    }
    public void Update()
    {

    }
    public bool FindRoom(string name)
    {
        bool found = false;
        foreach (RoomInfo roomInfo in roomList)
        {
            Debug.Log(roomInfo.Name);
            if (roomInfo.Name == name)
            {
                found = true;
            }

        }
        return found;
    }
    public override void OnRoomListUpdate(List<RoomInfo> list)
    {
        if (roomList == null)
            roomList = new List<RoomInfo>();

        foreach (RoomInfo info in list)
        {
            if (info.RemovedFromList)
            {
                // 削除されたルームをリストから削除
                roomList.RemoveAll(r => r.Name == info.Name);
            }
            else
            {
                // 存在しない場合は追加、存在する場合は更新
                int index = roomList.FindIndex(r => r.Name == info.Name);
                if (index != -1)
                    roomList[index] = info;
                else
                    roomList.Add(info);
            }
        }
        nullText.SetActive(roomList.Count == 0);
        RoomList();
    }
    public void CreateRandomRoom(RoomOptions roomOptions, mode roomMode)
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
            RoomPanel newPanel = Instantiate(clonePrefab, content.transform).GetComponent<RoomPanel>();
            newPanel.DataSet(roomInfo);
            newPanel.roomManager = this.gameObject.GetComponent<RoomManager>();
            clonedList.Add(newPanel.gameObject);
        }
    }
    public bool CreateOpenRoom(RoomOptions roomOptions, mode roomMode, string name)
    {

        string roomError = RoomNameCheck(name);
        if (roomError != "")
        {
            errorText.SetActive(true);
            errorText.GetComponent<TextMeshProUGUI>().SetText(roomError);
            return true;
        }

        gamemode = roomMode;
        errorText.SetActive(false);
        PhotonNetwork.CreateRoom(name, roomOptions);
        loadObject.SetActive(true);
        loadText.SetText("ルーム作成中・・・");
        return false;
    }
    public string RoomNameCheck(string roomName)
    {
        if (roomName.Length > 10)
        {
            return "10文字以下にしてください。";
        }

        if (roomName == "")
        {
            return "公開ルールの場合、名前の入力が必須です。";
        }
        if (FindRoom(roomName))
        {
            return "その名前のルームはあります。";
        }
        bool isNumberOnly = true;
        bool isSpace = false;
        foreach(char c in roomName)
        {
            switch (c)
            {
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    break;
                case ' ':
                    isSpace = true;
                    break;
                    default:
                    isNumberOnly = false;
                    break;

            }
        }
        if (isSpace)
        {
            return "名前に空白を入れることはできません。";
        }
        if (isNumberOnly)
        {
            return "数字のみの名前は使えません。";
        }
        

        return "";
    }
    public void InRoom(InputPass roomPass)
    {
        inputPass = roomPass;
        PhotonNetwork.JoinRoom(roomPass.GetPass());
        loadObject.SetActive(true);
        loadText.SetText("ルームに接続中・・・");

    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        switch (returnCode)
        {
            case 32758:
                inputPass.ErrorAcitve(true, "ルームが見つかりません");
                break;
            case 32760:
                inputPass.ErrorAcitve(true, "ルームが満員です");
                break;
            case 32766:
            default:
                inputPass.ErrorAcitve(true, "予期しないエラーが起きました");
                break;

        }
        loadObject.SetActive(false);
    }
    public override void OnCreatedRoom()
    {
        FindAnyObjectByType<PlaySettings>().SetMode(gamemode);
    }
    public override void OnJoinedRoom()
    {
        //Debug.Log(PhotonNetwork.CurrentRoom.Name);
        if (PhotonNetwork.OfflineMode)
        {
            return;
        }
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