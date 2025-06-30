using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StepperControl : MonoBehaviour
{
    public int value;
    public int maxValue;
    public int minValue;
    public TextMeshProUGUI text;
    public Button upButton;
    public Button downButton;
    public bool activeButton = true;
    public bool isLoop = false;
    // Start is called before the first frame update
    void Start()
    {
        value = minValue;
        ViewText();
    }

    // Update is called once per frame
    void Update()
    {
        if (activeButton && isLoop)
        {
            upButton.interactable = true;
            downButton.interactable = true;
        }
        else if (activeButton && !isLoop)
        {
            upButton.interactable = value < maxValue;
            downButton.interactable = value > minValue;
        }
        else
        {
            upButton.interactable = false;
            downButton.interactable = false;
        }

    }
    public void OnClick(bool up)
    {
        if (up)
        {
            if (value + 1 > maxValue)
            {
                if (isLoop)
                {
                    value = minValue;

                }
                else
                {
                    value = maxValue;
                }

                ViewText();
                return;
            }
            value++;
            ViewText();
            return;
        }
        else
        {
            if (value - 1 < minValue)
            {
                if (isLoop)
                {
                    value = maxValue;

                }
                else
                {

                    value = minValue;
                }
                
                ViewText();
                return;
            }
            value--;
            ViewText();
            return;
        }
    }
    public void Active(bool a)
    {
        activeButton = a;
    }
    public virtual void ViewText()
    {
        text.SetText(value + "l");
    }
    public int GetData()
    {
        return value;
    }
}
