
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using ExitGames.Client.Photon;
using TMPro;
using UnityEngine.Events;
using Photon.Realtime;
using DG.Tweening;
using UnityEngine.UI;
public class InGameRoomManager : MonoBehaviourPunCallbacks
{
    public GameObject logObj;
    public TextMeshProUGUI logText;
    public UnityEvent dissolution;
    public GameObject[] buttons;
    private int deviceNumber;
    private InGameNetworkManager networkManager;
    private bool isReloading = false; // 🔹 無限ループ防止フラグ
    private int playerCount;
    private float timer = 0f;

    void Start()
    {
        deviceNumber = PlayerPrefs.GetInt("Dnumber");
        networkManager = FindAnyObjectByType<InGameNetworkManager>();
        networkManager.SetData("RoomStatus", deviceNumber, 1);
        playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        ButtonActive(true);
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        OnRoomPropertiesUpdate(new Hashtable());
    }
    public void Update()
    {

        if (timer < 5f)
        {
            timer += Time.deltaTime;
        }
    }
    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        if (!PhotonNetwork.IsConnected)
        {
            return;
        }
        if (timer < 5f)
        {
            return;
        }
        // すでにロード中ならこれ以上処理しない
        if (isReloading)
        {
            return;
        }

        if (networkManager.GetData("RoomStatus", 1) == 0)
        {
            DissolutionLogic();
        }

        if (deviceNumber != 1)
        {
            Debug.Log("D");
            return;
        }
        if(networkManager.GetData("RoomStatus",deviceNumber) == 0)
        {
            Debug.Log("E");
            return;
        }
        

        bool isContinueAll = true;
        bool isDissolution = false;
        string st = "";
        for (int i = 1; i <= playerCount; i++)
        {
            int roomStatus = networkManager.GetData("RoomStatus", i);
            st += roomStatus;

            if (roomStatus != 2)
            {
                isContinueAll = false;
            }
            if (roomStatus == 0)
            {
                isDissolution = true;
            }
        }
        if (isContinueAll && !isDissolution)
        {
            for (int i = 1; i <= playerCount; i++)
            {
                networkManager.SetData("RoomStatus", deviceNumber, 1);
                networkManager.SetData("Load", deviceNumber, true);
            }

            // 🔹 これ以上呼ばれないようにフラグを立てる
            isReloading = true;

            // 全員同時にリロードしたいなら RPC にするのも可
            logText.text = "ゲームを続けます。";
            //ContinueLogic();  
            photonView.RPC(nameof(ContinueLogic), RpcTarget.All);
        }
        else if (isDissolution)
        {
            logText.text = "ルームを解散しています。";
            photonView.RPC(nameof(DissolutionLogic), RpcTarget.All);
        }
    }

    public void ContinueClick()
    {
        ButtonActive(false);
        // 0:退出済み 1:未選択(プレイ中、リザルト) 2:続ける意志あり 
        networkManager.SetData("RoomStatus", deviceNumber, 2);
        logObj.SetActive(true);
        logText.text = "他のプレイヤーを待っています。";
    }

    public void EnterClick()
    {
        ButtonActive(false);
        networkManager.SetData("RoomStatus", deviceNumber, 0);
        logObj.SetActive(true);
        logText.text = "退出しています。";
        DOVirtual.DelayedCall(1, () => dissolution.Invoke());

    }
    public void ButtonActive(bool a)
    {
        foreach (GameObject obj in buttons)
        {
            if (obj.GetComponent<Button>() != null)
            {
                obj.GetComponent<Button>().interactable = a;
            }
            else
            {
                obj.SetActive(a);
            }
        }
    }
    [PunRPC]
    public void ContinueLogic()
    {

        logObj.SetActive(false);
        PhotonNetwork.LoadLevel(SceneManager.GetActiveScene().name);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    [PunRPC]
    public void DissolutionLogic()
    {
        // 部屋解散処理をここに書く
        logText.text = "ルームを解散しています。";
        DOVirtual.DelayedCall(1, () => dissolution.Invoke());
    }
}
