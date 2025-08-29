using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
public class MoveScene : MonoBehaviourPunCallbacks
{
    public CardControl selectCard = null;
    public Button nextButton;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        nextButton.interactable = selectCard != null;
    }
    public void Select(CardControl card)
    {
        selectCard = card;
    }
    public void NextButton()
    {
        FindAnyObjectByType<InGameNetworkManager>().SetData("RoomStatus", 1, 1);
        //Move(selectCard.stageData.sceneName);
        photonView.RPC(nameof(Move), RpcTarget.All, selectCard.stageData.sceneName);
    }
    [PunRPC]
    public void Move(string name)
    {
        //PhotonNetwork.LoadLevel(name);
        SceneManager.LoadScene(name);
    }
}
