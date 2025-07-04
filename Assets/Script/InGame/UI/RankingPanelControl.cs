using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RankingPanelControl : MonoBehaviour
{
    public TextMeshProUGUI rankingText;
    public TextMeshProUGUI playerNameText;
    public Color fastColor;
    public Color secondColor;
    public Color thirdColor;
    public Color otherColor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetData(int ranking,int playerName)
    {

        switch (ranking)
        {
            case 1:
                rankingText.color = fastColor;
                break;
            case 2:
                rankingText.color = secondColor;
                break;
            case 3:
                rankingText.color = thirdColor;
                break;
            default:
                rankingText.color = otherColor;
                break;
        }
        rankingText.text = ranking.ToString() + "ˆÊ";
        playerNameText.text = playerName + "P";

    }
    public void Hidden()
    {
        this.gameObject.GetComponent<Image>().color = new Color(0,0,0,0);
        rankingText.color = new Color(0, 0, 0, 0);
        playerNameText.color = new Color(0, 0, 0, 0);
    }
}
