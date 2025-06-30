using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PowerSlider : MonoBehaviour
{
    public GameObject sliderObject;
    public GameObject stopButton;
    public Slider powerSlider;
    public float speed;
    private bool isMove = false;
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
    public void Active(bool a)
    {
        sliderObject.SetActive(a);
        stopButton.SetActive(a);
        powerSlider.value = powerSlider.maxValue;
        isMove = false;
    }
    public void SliderMove()
    {
        if (isMove)
        {
            return;
        }
        isMove = true;

        powerSlider.DOValue(powerSlider.maxValue, speed)
            .OnComplete(() => powerSlider.DOValue(powerSlider.minValue, speed)
            .OnComplete(() => isMove = false));
    }
    public void Stop()
    {
        powerSlider.DOKill();
        //Debug.Log(Mathf.Abs(powerSlider.value));
        //Debug.Log(powerSlider.maxValue - Mathf.Abs(powerSlider.value));
        powerData = powerSlider.maxValue - Mathf.Abs(powerSlider.value);
        powerData += 1f;
        Active(false);
        FindAnyObjectByType<GameManager>().EraserFocus();
    }
    public float GetData()
    {
        return powerData;
    }
}
