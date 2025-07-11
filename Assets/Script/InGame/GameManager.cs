using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
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
    [System.Serializable]public class PlayerData
    {
        public int deviceNumber;
        public bool isComputer;
        public bool isAlive = true;
        public int computerLevel = 0;
    }
    [SerializeField] public List<PlayerData> playerList = new List<PlayerData>();
    private static Hashtable propHash = new Hashtable();
    // Start is called before the first frame update
    void Start()
    {
        GetPlayerData();
        turn = 1;
        deviceNumber = PlayerPrefs.GetInt("Dnumber");
        isOperation = deviceNumber == 1;


    }
    public void GetPlayerData()
    {
        playerList.Clear();
        for (int i = 1; i <= PhotonNetwork.CurrentRoom.PlayerCount; i++)
        {
            int playerCount = (PhotonNetwork.CurrentRoom.CustomProperties["playerCount" + "" + i.ToString()] is int x) ? x : 0;
            for (int p = 0 ; p < playerCount; p++)
            {
                PlayerData playerData = new PlayerData();
                playerData.deviceNumber = i;
                playerData.isComputer = false;
                playerData.computerLevel = 0;
                playerList.Add(playerData);
            }
            int comCount = (PhotonNetwork.CurrentRoom.CustomProperties["comCount" + "" + i.ToString()] is int y) ? y : 0;
            for (int c = 0; c < comCount; c++)
            {
                PlayerData playerData = new PlayerData();
                playerData.deviceNumber = i;
                playerData.isComputer = true;
                playerData.computerLevel = 1;
                playerList.Add(playerData);
            }
        }
    }
    [PunRPC]
    public void Turn(int playerNumber)
    {
        turn = playerNumber;
        frameControl.ChangeColor(colorData.activeColorPackage[turn -1],turn);
        frameControl.Active(true);
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
        if(aliveCount == 1)
        {
            Debug.Log("Clear");
            killCheck.Winner(winner);
            cameraPhotonView.GetComponent<PhotonView>().RPC("Result", RpcTarget.All);
            return;
        }
        if (turn > playerList.Count)
        {
            Debug.Log("Reset");
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
    public void Kill(int playerNumber)
    {
        playerList[playerNumber -1].isAlive = false;
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting && deviceNumber == 1) // Ž©•ª‚ª‘—‚é‘¤
        {
            stream.SendNext(turn);
        }
        else // Ž©•ª‚ªŽó‚¯Žæ‚é‘¤
        {
            turn = (int)stream.ReceiveNext();
        }
    }
    public void Ranking()
    {
        scrollbarControl.View(playerList);
    }
}

