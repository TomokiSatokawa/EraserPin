using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CardControl : MonoBehaviour
{
    public Button button;
    public GameObject outLine;
    public StageData stageData;
    public TextMeshProUGUI stageName;
    public TextMeshProUGUI playerCount;
    public TextMeshProUGUI isGimmick;
    public Image stageImage;
    // Start is called before the first frame update
    void Start()
    {
        if(stageData == null)
        {
            playerCount.text = "";
            isGimmick.text = "";
            return;
        }
        stageName.text = stageData.stageName;
        playerCount.text = stageData.miniPlayerCount + "～" + stageData.maxPlayerCount + "人";
        if (stageData.isGimmick)
        {
            isGimmick.text = "ギミックあり";
        }
        else
        {
            isGimmick.text = "ギミックなし";
        }
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
        CardControl[] cards = FindObjectsByType<CardControl>(FindObjectsSortMode.None);
        foreach(CardControl card in cards)
        {
            card.Active(false);
        }
        Active(true);
    }
    public void Active(bool a)
    {
        outLine.SetActive(a);
    }
}
