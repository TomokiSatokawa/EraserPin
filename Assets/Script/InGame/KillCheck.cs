using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;
using System.Threading;
public class KillCheck : MonoBehaviourPunCallbacks
{
    public GameObject killEffect;
    public GameObject gameMasterNet;
    private static Hashtable propHash = new Hashtable();
    public GameManager gameManager;
    public int winnerEraser = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (PlayerPrefs.GetInt("Dnumber") != 1)
        {
            return;
        }
        GameObject hitObject = other.gameObject.transform.parent.gameObject;
        if (hitObject.GetComponent<EraserControlBase>() != null)
        {
            EraserControlBase eraserData = hitObject.GetComponent<EraserControlBase>();
            PhotonNetwork.Instantiate(killEffect.name, hitObject.transform.position, Quaternion.identity);
            gameMasterNet.GetComponent<PhotonView>().RPC("Kill", RpcTarget.All, eraserData.playerNumber);


            //Debug.Log("A");.
            winnerEraser = hitObject.GetComponent<EraserControlBase>().playerNumber;
            propHash["ranking" + "" + (RemainingPlayer() + 1).ToString()] = eraserData.playerNumber;
            Debug.Log(eraserData.playerNumber + "P");
            PhotonNetwork.CurrentRoom.SetCustomProperties(propHash);
            propHash.Clear();
            PhotonNetwork.Destroy(hitObject);

            //Debug.Log("B");
        }


    }
    public int RemainingPlayer()
    {
        int count = 0;
        foreach(GameManager.PlayerData data in gameManager.playerList)//êÊÇ…GM.KillÇµÇƒÇ¢ÇÈ
        {
            if (data.isAlive)
            {
                count++;
            }
        }
        Debug.Log(count + 1 + "à ");
        return count;
    }
    public void Winner(int playerNumber)
    {
        if(playerNumber == 0)
        {
            if(winnerEraser == 0)
            {
                Debug.LogError("WinnerEraser Null");
            }
            playerNumber = winnerEraser;
        }
        propHash["ranking" + "" + 1] = playerNumber;
        PhotonNetwork.CurrentRoom.SetCustomProperties(propHash);
        propHash.Clear();
    }

}
