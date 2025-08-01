using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
public class ScreenChange : MonoBehaviourPunCallbacks
{
    public GameObject title;
    public GameObject room;
    public GameObject roomOption;
    public GameObject inRoom;
    public GameObject searchRoom;
    public GameObject playerCount;
    public GameObject errorObject;
    public GameObject characterChoice;
    public GameObject stageSelect;
    public GameObject locaPlayerCount;
    private void Awake()
    {
        Active();
        title.SetActive(true);
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
                inRoom.SetActive(false);
                roomOption.SetActive(false);
                searchRoom.SetActive(false);
                break;
            case 2:
                inRoom.SetActive(false);
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
            case 6:
                locaPlayerCount.SetActive(true);
                break;
            case 10:
                errorObject.SetActive(false);
                SceneManager.LoadScene("Start");
                break;
        }
    }
    public void Parallel(int a)
    {
        inRoom.SetActive(false);
        roomOption.SetActive(false);
        searchRoom.SetActive(false);
        switch (a)
        {
            case 0:
                inRoom.SetActive(true);
                break;
            case 1:
                roomOption.SetActive(true);
                break;
            case 2:
                searchRoom.SetActive(true);
                break;
        }
    }
    public void Active()
    {
        title.SetActive(false);
        room.SetActive(false);
        inRoom.SetActive(false);
        roomOption.SetActive(false);
        searchRoom.SetActive(false);
        playerCount.SetActive(false);
        characterChoice.SetActive(false);
        stageSelect.SetActive(false);
        locaPlayerCount.SetActive(false);
    }
}
