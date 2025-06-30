using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardControl : MonoBehaviour
{
    public Button button;
    public GameObject outLine;
    public string stageSceneName;
    public TextMeshProUGUI textMeshProUGUI;
    // Start is called before the first frame update
    void Start()
    {
        textMeshProUGUI.text = this.gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ClickActive(bool a)
    {
        button.enabled = a;
    }
    public void OnClick()
    {
        FindAnyObjectByType<MoveScene>().Select(this.gameObject.GetComponent<CardControl>());
    }
}
