using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
public class GameManager : MonoBehaviourPunCallbacks, IPunObservable
{
    public int turn;
    public int deviceNumber;
    public bool isOperation;
    public FrameControl frameControl;
    public CameraWork cameraWork;
    public PointerControl pointerControl;
    public EraserClone eraserClone;
    public PowerSlider powerSlider;
    public StopCheck stopCheck;
    public ScrollbarControl scrollbarControl;
    public KillCheck killCheck;
    public GameObject eraserMove;
    public GameObject cameraPhotonView;
    public ColorData colorData;
    public CharacterDataList characterDataList;
    public GameObject frameUI;
    public GameObject playerListUI;
    public ChatManager chatManager;
    [System.Serializable]public class PlayerData
    {
        public int deviceNumber;
        public bool isComputer;
        public bool isAlive = true;
        public int computerLevel = 0;
        public CharacterData eraserData;
    }
    [SerializeField] public List<PlayerData> playerList = new List<PlayerData>();
    private static Hashtable propHash = new Hashtable();
    private ComMove comMove;
    // Start is called before the first frame update
    public void Awake()
    {
        
    }
    void Start()
    {
        GetPlayerData();
        turn = 1;
        deviceNumber = PlayerPrefs.GetInt("Dnumber");
        isOperation = deviceNumber == 1;
        FindAnyObjectByType<PlayerListControl>().Clone(playerList);
        comMove = FindAnyObjectByType<ComMove>();
        frameUI.SetActive(true);
        playerListUI.SetActive(true);
    }
    public void GetPlayerData()
    {
        playerList.Clear();
        int playerNumber = 1;
        for (int i = 1; i <= PhotonNetwork.CurrentRoom.PlayerCount; i++)
        {
            int playerCount = (PhotonNetwork.CurrentRoom.CustomProperties["playerCount" + "" + i.ToString()] is int x) ? x : 0;
            for (int p = 0 ; p < playerCount; p++)
            {
                PlayerData playerData = new PlayerData();
                playerData.deviceNumber = i;
                playerData.isComputer = false;
                playerData.computerLevel = 0;
                playerData.eraserData = GetEraserData(playerNumber);
                playerList.Add(playerData);
                playerNumber++;
            }
            int comCount = (PhotonNetwork.CurrentRoom.CustomProperties["comCount" + "" + i.ToString()] is int y) ? y : 0;
            for (int c = 0; c < comCount; c++)
            {
                PlayerData playerData = new PlayerData();
                playerData.deviceNumber = i;
                playerData.isComputer = true;
                playerData.computerLevel = 1;
                playerData.eraserData = GetEraserData(playerNumber);
                playerList.Add(playerData);
                playerNumber++;
            }
        }
    }
    public CharacterData GetEraserData(int i)
    {
        string CharacterCode = (string)PhotonNetwork.CurrentRoom.CustomProperties["character" + (i).ToString()];
        //Debug.Log( i+""+CharacterCode);
        string gameMode = CharacterCode[0].ToString();//null
        int Index = int.Parse(CharacterCode.Substring(1));
        if (CharacterCode[0] == 'A')
        {
            return characterDataList.normalEraser[Index];
        }
        else
        {
            return characterDataList.hardEraser[Index];
        }
    }
    [PunRPC]
    public void Turn(int playerNumber)
    {
        turn = playerNumber;
        foreach (EraserIcon eraserIcon in FindObjectsOfType<EraserIcon>())
        {
            eraserIcon.ActiveOutline(turn);
        }
        frameControl.ChangeColor(colorData.activeColorPackage[turn -1],turn);
        frameControl.Active(true);
        eraserClone.cloneEraserObjects[turn - 1].GetComponent<EraserControlBase>().MyTurn();
        cameraWork.TopFocus();
    }
    public void NextTurn()
    {

        turn++;
        //Debug.Log(turn);
        int aliveCount = 0;
        int winner = 0;
        int i  = 0;
        foreach(PlayerData playerData in playerList)
        {
            if (playerData.isAlive)
            {
                winner = i + 1;
                aliveCount++;
            }
            i++;
        }
        if(aliveCount <= 1)
        {
            //Debug.Log("Clear");
            killCheck.Winner(winner);
            cameraPhotonView.GetComponent<PhotonView>().RPC("Result", RpcTarget.All);
            photonView.RPC(nameof(ResultUI), RpcTarget.All);
            turn = 0;
            return;
        }
        if (turn > playerList.Count)
        {
            //Debug.Log("Reset");
            turn = 0;
            NextTurn();
            return;
        }
        if (playerList[turn - 1].isAlive == false)
        {
            Debug.Log("Skip");
            NextTurn();
            return;
        }
       
        photonView.RPC(nameof(Turn),RpcTarget.All,turn);
    }
    public void Pointor()
    {
        powerSlider.Active(false);
        if (playerList[turn-1].isComputer)
        {
            comMove.Input(eraserClone.cloneEraserObjects[turn - 1]);
            DOTween.CompleteAll();
            DOVirtual.DelayedCall(1f,EraserFocus);
            return;
        }
        GameObject targetEraser = eraserClone.cloneEraserObjects[turn - 1];
        int deviceNumber = playerList[turn - 1].deviceNumber;
        pointerControl.Active(true, targetEraser,deviceNumber);
    }
    public void Power()
    {
        powerSlider.Active(true, playerList[turn -1].deviceNumber);   
    }
    public void EraserFocus()
    {
        //cameraPhotonView.GetComponent<PhotonView>().RPC("EraserFocus", RpcTarget.All, eraserClone.cloneEraserObjects[turn - 1], true);
        cameraWork.EraserFocus(eraserClone.cloneEraserObjects[turn - 1],true);
    }
    public void Move()
    {
        float power = powerSlider.GetData();
        //Log.text("p"+power);
        Vector3 direction = pointerControl.GetData(power);
        Vector3 hitPosition = pointerControl.GetHitPosition();
        Vector3 rotate = pointerControl.GetRotate(power);
        power = pointerControl.GetPower(power);
        eraserMove.GetComponent<PhotonView>().RPC("Move",RpcTarget.All,turn,power,direction,rotate,hitPosition);
        
    }
    public void Check()
    {

        if(deviceNumber == 1)
        {

        stopCheck.Check();
        }
    }    // Update is called once per frame
    [PunRPC]
    public void Kill(int playerNumber)
    {
        playerList[playerNumber -1].isAlive = false;
        FindAnyObjectByType<PlayerListControl>().DropoutCheck();
        chatManager.ChatMessage(playerNumber + "Pが脱落した！");
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting && deviceNumber == 1) // 自分が送る側
        {
            stream.SendNext(turn);
        }
        else // 自分が受け取る側
        {
            turn = (int)stream.ReceiveNext();
        }
    }
    public void Ranking()
    {
        scrollbarControl.View(playerList);
    }
    [PunRPC]
    public void ResultUI()
    {
        frameUI.SetActive(false);
        playerListUI.SetActive(false);
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        int i = 1;
        foreach(PlayerData data in playerList)
        {
            if(data.deviceNumber == otherPlayer.ActorNumber)
            {
                data.isComputer = true;
            }
            if(turn == i)
            {
                Pointor();
            }
                i++;
        }
        if(deviceNumber == 1)
        {
            chatManager.gameObject.GetComponent<PhotonView>().RPC(nameof(chatManager.ChatMessage), RpcTarget.All, otherPlayer.NickName + "が退出しました。" + otherPlayer.NickName + "の代わりにCOMがプレイします。");
        }
    }
}

