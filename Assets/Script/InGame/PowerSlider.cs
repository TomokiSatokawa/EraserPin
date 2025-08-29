using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PowerSlider : MonoBehaviour
{
    public GameObject sliderObject;
    public GameObject stopButton;
    public Slider powerSlider;
    public GameObject jetToggle;
    private bool isJet;
    public float speed;
    private bool isUp;
    private float powerData;
    // Start is called before the first frame update
    void Start()
    {
        Active(false);
    }

    // Update is called once per frame
    void Update()
    {
        SliderMove();
    }
    public void Active(bool a, int deviceNumber = 0)
    {
        if (deviceNumber != 0 && deviceNumber != PlayerPrefs.GetInt("Dnumber"))
        {
            return;
        }
        sliderObject.SetActive(a);
        stopButton.SetActive(a);
        powerSlider.value = powerSlider.maxValue;
        jetToggle.SetActive(isJet && a);
    }
    public void SliderMove()
    {
        
        if (powerSlider.value >= powerSlider.maxValue)
        {
            isUp = false;
        }
        else if(powerSlider.value <= powerSlider.minValue)
        {
            isUp  = true;
        }

        if (isUp)
        {
            powerSlider.value += speed *  Time.deltaTime;
        }
        else
        {
            powerSlider.value -= speed * Time.deltaTime;
        }

    }
    public void Stop()
    {
        powerSlider.DOKill();
        //Debug.Log(Mathf.Abs(powerSlider.value));
        //Debug.Log(powerSlider.maxValue - Mathf.Abs(powerSlider.value));
        powerData = powerSlider.maxValue - Mathf.Abs(powerSlider.value);
        powerData += 1f;
        if (isJet)
        {
            powerData *= 10;
            isJet = false;
        }
        Active(false);
        FindAnyObjectByType<GameManager>().EraserFocus();
    }
    public void Jet()
    {
        isJet = true;
    }
    public void ComPowerData(float value)
    {
        powerSlider.value = value;
    }
    public float GetData()
    {
        return powerData;
    }
    public void ComInput(float p)
    {
        powerData = p;
    }
}
