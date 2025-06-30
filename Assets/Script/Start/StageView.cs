using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class StageView : MonoBehaviourPunCallbacks
{
    public List<GameObject> smallStage;
    public List<GameObject> mediumStage;
    public List<GameObject> largeStage;
    public List<GameObject> bigStage;
    // Start is called before the first frame update
    void Start()
    {
        int playerNumebr = PlayerPrefs.GetInt("Dnumber");
        CardControl[] cardControls = FindObjectsOfType<CardControl>(); 
        foreach (CardControl cardControl in cardControls)
        {
            cardControl.ClickActive(playerNumebr == 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void View(int playerCount)
    {
        foreach (GameObject obj in smallStage)
        {
            obj.SetActive(playerCount <= 5);
        }

        foreach (GameObject obj in mediumStage)
        {
            obj.SetActive(playerCount >= 4 && playerCount <= 8);
        }

        foreach (GameObject obj in largeStage)
        {
            obj.SetActive(playerCount >= 6 && playerCount <= 12);
        }

        foreach (GameObject obj in bigStage)
        {
            obj.SetActive(playerCount >= 10 && playerCount <= 16);
        }
    }
}
