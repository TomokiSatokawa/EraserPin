//using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;
public class PlaySettings : MonoBehaviourPunCallbacks
{
    private static Hashtable propHash = new Hashtable();
    public int playerNumber;
    public int comNumber;
    private void Awake()
    {

       
    }
    public void Start()
    {
       
    }
    public void OnJointBreak(float breakForce)
    {
        
    }
    public void ResetData()
    {
        int deviceNumber = PhotonNetwork.CurrentRoom.PlayerCount;
        Ready(deviceNumber, 0);
        SetData(deviceNumber, 1, 0);
    }
    [PunRPC]
    public void SetData(int deviceNumber , int playerCount , int comCount)
    {
        propHash["playerCount" + "" + deviceNumber.ToString()] = playerCount;
        propHash["comCount" + "" + deviceNumber.ToString()] = comCount;
        PhotonNetwork.CurrentRoom.SetCustomProperties(propHash);

        propHash.Clear();
    }
    [PunRPC]
    public void NameSet(int deviceNumber, string name)
    {
        propHash["name" +""+ deviceNumber.ToString()] = name;
        PhotonNetwork.CurrentRoom.SetCustomProperties(propHash);
        propHash.Clear();
    }
    public void Ready(int deviceNumber,int iNum)
    {
        propHash["ready" + "" + deviceNumber.ToString()] = iNum;
        propHash["Load" + "" + deviceNumber.ToString()] = false;
        PhotonNetwork.CurrentRoom.SetCustomProperties(propHash);
        propHash.Clear();
    }
    public void Character(int deviceNumber,int iNum)
    {
        propHash["character" + "" + deviceNumber.ToString()] = iNum;
        PhotonNetwork.CurrentRoom.SetCustomProperties(propHash);
    }
    public void SetLocalData(int playerCount, int comCount)
    {
        playerNumber = playerCount;
        comNumber = comCount;
    }
    public int GetPlayerCount()
    {
        return playerNumber;
    }
    public int GetComCount()
    {
        return comNumber;
    }
     
}
