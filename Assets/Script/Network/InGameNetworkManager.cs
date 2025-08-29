//using System.Collections;
using Photon.Pun;
using ExitGames.Client.Photon;
using UnityEngine;
public class InGameNetworkManager : MonoBehaviourPunCallbacks
{
    private static Hashtable propHash = new Hashtable();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetData(string valueName,int deviceNumber,object value)
    {
        //Debug.Log("setdata" + deviceNumber + ":" + value);
        propHash[valueName + "" + deviceNumber.ToString()] = value;
        PhotonNetwork.CurrentRoom.SetCustomProperties(propHash);
        propHash.Clear();
    }
    public int GetData(string valueName, int deviceNumber)
    {
        
        if(PhotonNetwork.IsConnected == false)
        {
            Debug.LogWarning("ネットワーク未接続時にカスタムプロパティを取得しようとしました。");
            return 0;
        }
        int value = (PhotonNetwork.CurrentRoom.CustomProperties[valueName + "" + deviceNumber.ToString()] is int a) ? a : -1;
        //Debug.Log("getdata" + deviceNumber + ":" + value);
        return value;
    }
}
