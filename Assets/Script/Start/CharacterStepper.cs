using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class CharacterStepper : StepperControl
{
    public CharacterDataList characterDataList;
    public int playerNumber;
    private List<CharacterData> list;
    // Start is called before the first frame update
    void Start()
    {
        int gameMode = (PhotonNetwork.CurrentRoom.CustomProperties["mode"] is int c) ? c : 0;
        if (gameMode == 0)
        {
            list = characterDataList.normalEraser;
        }
        else
        {
            list = characterDataList.hardEraser;
        }
        maxValue = list.Count -1;
        minValue = 0;
        ViewText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void ViewText()
    {
        text.text = list[value].eraserName;
    }
}
