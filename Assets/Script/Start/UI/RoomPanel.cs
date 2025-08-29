using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomPanel : MonoBehaviour
{
    public TextMeshProUGUI roomName;
    public TextMeshProUGUI roomPlayerCount;
    public Button JoinButton;
    public TextMeshProUGUI buttonText;
    public RoomManager roomManager;
    public Sprite joinImage;
    public Sprite playingImage;
    private string pass;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DataSet(RoomInfo roomData)
    {
        roomName.text = roomData.Name;
        roomPlayerCount.text = roomData.PlayerCount + "/" + roomData.MaxPlayers;

        if (roomData.IsOpen)
        {
            JoinButton.interactable = true;
            JoinButton.gameObject.GetComponent<Image>().sprite = joinImage;
        }
        else
        {
            JoinButton.interactable= false;
            JoinButton.gameObject.GetComponent<Image>().sprite = playingImage;
        }
        pass = roomData.Name;
    }
    public void OnClick()
    {
        InputPass inputPass = new InputPass();
        inputPass.pass = pass;
        roomManager.InRoom(inputPass);
    }
}
