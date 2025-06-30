using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ScreenChange : MonoBehaviourPunCallbacks
{
    public GameObject title;
    public GameObject room;
    public GameObject inRoom;
    public GameObject playerCount;
    public GameObject errorObject;
    public GameObject characterChoice;
    public GameObject stageSelect;
    private void Awake()
    {
        title.SetActive(true);
        room.SetActive(false);
        inRoom.SetActive(false);
        playerCount.SetActive(false);
        errorObject.SetActive(false);
        characterChoice.SetActive(false);
        stageSelect.SetActive(false);
    }
    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    [PunRPC]
    public void OnClick(int a)
    {
        Active();
        switch (a)
        {
            case 0:
                title.SetActive(true);
                break;
            case 1:
                room.SetActive(true);
                break;
            case 2:
                inRoom.SetActive(true);
                break;
            case 3:
                playerCount.SetActive(true);
                break;
            case 4:
                characterChoice.SetActive(true);
                break;
            case 5:
                stageSelect.SetActive(true);
                break;
            case 10:
                errorObject.SetActive(false);
                break;
        }
    }
    public void Active()
    {
        title.SetActive(false);
        room.SetActive(false);
        inRoom.SetActive(false);
        playerCount.SetActive(false);
        characterChoice.SetActive(false);
        stageSelect.SetActive(false);
    }
}
