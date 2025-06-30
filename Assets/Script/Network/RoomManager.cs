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
    private ScreenChange screenChange;
    private int deviceNumber;
    private string deviceName;
    // Start is called before the first frame update
    void Start()
    {
        screenChange = FindAnyObjectByType<ScreenChange>();
    }
    public void CreateRandomRoom()
    {
        RoomOptions roomOptions = new RoomOptions
        {
            MaxPlayers = 4, // ��: �ő�4�l
            IsVisible = true, // ���r�[�ɕ\��
            IsOpen = true // �Q���\
        };

        PhotonNetwork.CreateRoom(Random.Range(0, 9999).ToString("0000"), roomOptions);
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
    }
    public override void OnJoinedRoom()
    {


        //��ʈړ�����
        screenChange.OnClick(3);
        loadObject.SetActive(false);

        //�e�L�X�g
        roomPas.SetText("���[���p�X�F" + PhotonNetwork.CurrentRoom.Name);
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


        //��������
        FindAnyObjectByType<DeviceView>().View(PhotonNetwork.CurrentRoom.PlayerCount);
        deviceViewPhoton.GetComponent<PhotonView>().RPC("View", RpcTarget.All, PhotonNetwork.CurrentRoom.PlayerCount);

        //�l
        deviceNumber = PhotonNetwork.CurrentRoom.PlayerCount;
        deviceViewPhoton.GetComponent<DeviceView>().OutLine(deviceNumber);
        playSettings.ResetData();
        
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
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