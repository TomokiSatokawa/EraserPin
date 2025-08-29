using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class LocalPlayerCount : MonoBehaviourPunCallbacks
{
    public StepperControl playerStepper;
    public StepperControl comStepper;
    public StepperMove gameMode;
    public Button nextButton;
    public GameObject manyPlayers;
    public GameObject fewPlayers;
    private PreviewControl previewControl;
    public void Start()
    {
        previewControl = FindAnyObjectByType<PreviewControl>();
        int allPlayer = playerStepper.Value + comStepper.Value;
        nextButton.interactable = true;
        fewPlayers.SetActive(false);
        manyPlayers.SetActive(false);
        StepperClick();
    }
    public void OnClick()
    {
        RoomManager.mode mode;
        if (gameMode.value == 0)
        {
            mode = RoomManager.mode.Normal;

        }
        else
        {
            mode= RoomManager.mode.Hard;
        }
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 1;
        options.IsOpen = true;
        options.IsVisible = false;
        FindAnyObjectByType<RoomManager>().gamemode = mode;
        if (PhotonNetwork.InRoom)
        {
            Debug.LogError("InRoom");
            return;
        }
        PhotonNetwork.CreateRoom("Local");
       
    }
    public void StepperClick()
    {
        previewControl.Active(playerStepper.Value + comStepper.Value);
        int allPlayer = playerStepper.Value + comStepper.Value;
        nextButton.interactable = allPlayer > 1 && allPlayer < 5;
        fewPlayers.SetActive(allPlayer < 2);
        manyPlayers.SetActive(allPlayer > 4);
    }
    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.OfflineMode)
        {
            PlayerPrefs.SetInt("playerCount" + 1, playerStepper.Value);
            PlayerPrefs.SetInt("comCount" + 1, playerStepper.Value);
            PlayerPrefs.Save();
            FindAnyObjectByType<PlaySettings>().SetData(1, playerStepper.Value, comStepper.Value);
            FindAnyObjectByType<ScreenChange>().OnClick(4);
        }
    }
}
