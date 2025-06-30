using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStepper : StepperControl
{
    public CharacterDataList characterDataList;
    // Start is called before the first frame update
    void Start()
    {
        maxValue = characterDataList.dataList.Count -1;
        minValue = 0;
        ViewText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void ViewText()
    {
        text.text = characterDataList.dataList[value].name;
    }
}
