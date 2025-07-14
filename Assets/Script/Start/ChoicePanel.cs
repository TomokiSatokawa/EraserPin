using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChoicePanel : MonoBehaviour
{
    public CharacterStepper stepper;
    public TextMeshProUGUI nameText;
    public int localPlayerNumber;
    public int playerNumber;
    // Start is called before the first frame update
    void Start()
    {
        stepper.playerNumber = localPlayerNumber;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetName(int number)
    {
        nameText.SetText(number.ToString() + "P");
        playerNumber = number;
    }
    public int GetPlayerNumber()
    {
        return playerNumber;
    }
    public int CharacterCode()
    {
        return stepper.GetData();
    }
}
