using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoubleClickPrevention : MonoBehaviour
{
    private Button button;
    // Start is called before the first frame update
    void Start()
    {
        button.interactable = true;

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnClick()
    {
        button.interactable = false;
    }
    public void OnEnable()
    {
        button = this.gameObject.GetComponent<Button>();
        button.interactable = true;
    }
}