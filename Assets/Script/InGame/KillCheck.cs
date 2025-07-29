using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;
using System.Threading;
public class KillCheck : MonoBehaviourPunCallbacks
{
    public GameObject killEffect;
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
        GameObject hitObject = other.gameObject.transform.parent.gameObject;
        if(hitObject.GetComponent<EraserControlBase>() != null)
        {
            EraserControlBase eraserData = hitObject.GetComponent<EraserControlBase>();
            PhotonNetwork.Instantiate(killEffect.name, hitObject.transform.position, Quaternion.identity);
            FindAnyObjectByType<GameManager>().Kill(eraserData.playerNumber);
            PhotonNetwork.Destroy(hitObject);

            if (PlayerPrefs.GetInt("Dnumber") == 1)
            {
                Debug.Log("A");
                winnerEraser = hitObject.GetComponent<EraserControlBase>().playerNumber;
                propHash["ranking" + "" + eraserData.playerNumber] = RemainingPlayer() + 1;
                PhotonNetwork.CurrentRoom.SetCustomProperties(propHash);
                propHash.Clear();
            }
                Debug.Log("B");
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
        Debug.Log(count);
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
        propHash["ranking" + "" + playerNumber] = 1;
        PhotonNetwork.CurrentRoom.SetCustomProperties(propHash);
        propHash.Clear();
    }

}
