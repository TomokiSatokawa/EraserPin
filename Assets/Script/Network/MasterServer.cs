using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
public class MasterServer : MonoBehaviourPunCallbacks
{
    public GameObject loadObject;
    public TextMeshProUGUI loadText;
    public GameObject errorObject;
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI errorText;
    private ScreenChange screenChange;
    private void Start()
    {
        screenChange = FindAnyObjectByType<ScreenChange>();
    }
    public void OnMasterSever()
    {
        loadObject.SetActive(true);
        loadText.SetText("サーバーに接続中・・・");
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnJoinedLobby()
    {
        loadObject.SetActive(false);
        screenChange.OnClick(1);
    }
    public override void OnConnectedToMaster()
    {
        loadText.SetText("ロビーに接続中・・・");
        PhotonNetwork.JoinLobby();
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        messageText.SetText("サーバーに接続できませんでした。");
        errorText.SetText(cause.ToString());
        errorObject.SetActive(true);
        loadObject.SetActive(false);
    }
}
