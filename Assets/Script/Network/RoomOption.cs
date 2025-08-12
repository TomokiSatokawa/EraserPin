using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class RoomOption : MonoBehaviour
{
    public StepperMove modeStepper;
    public StepperMove accessStepper;
    public TMP_InputField roomName;
    public RoomManager roomManager;
    public GameObject button;
    // Start is called before the first frame update
    void Start()
    {
        roomName.ActivateInputField();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnClick()
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4;
        options.IsOpen = true;
        options.IsVisible = accessStepper.GetData() == 0;

        RoomManager.mode gameMode;
        if (modeStepper.GetData() == 0)
        {
            gameMode = RoomManager.mode.Normal;
        }
        else
        {
            gameMode = RoomManager.mode.Hard;
        }
        if (accessStepper.GetData() == 0)
        {
            if (roomManager.CreateOpenRoom(options, gameMode, roomName.text))
            {
                button.SetActive(false);
                button.SetActive(true);
            }

        }
        else
        {
            roomManager.CreateRandomRoom(options, gameMode);
        }

    }
}