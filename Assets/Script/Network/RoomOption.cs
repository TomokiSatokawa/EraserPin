using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class RoomOption : MonoBehaviour
{
    public StepperMove modeStepper;
    public StepperMove accessStepper;
    public TMP_InputField roomName;
    public RoomManager roomManager;
    // Start is called before the first frame update
    void Start()
    {

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
            roomManager.CreateOpenRoom(options, gameMode, roomName.text);
        }
        else
        {
            roomManager.CreateRandomRoom(options, gameMode);
        }

    }
}
