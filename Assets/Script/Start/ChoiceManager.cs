using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceManager : MonoBehaviour
{
    public List<GameObject> panelList;
    public int deviceNumber;
    public Button nextButton;
    public GameObject waitObj;
    public bool isClick = false;
    // Start is called before the first frame update
    void Start()
    {
        deviceNumber = PlayerPrefs.GetInt("Dnumber");
        ViewPanel();
        nextButton.interactable = true;
        waitObj.SetActive(false);
        FindAnyObjectByType<PlaySettings>().Ready(deviceNumber, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (isClick)
        {
            bool b = true;
            for (int i = 1; i <= PhotonNetwork.CurrentRoom.PlayerCount; i++)
            {
                int ready = (PhotonNetwork.CurrentRoom.CustomProperties["ready" + "" + i.ToString()] is int a) ? a : 3;
                Debug.Log(i + "" + ready);
                if (ready == 0)
                {
                    b = false;
                    continue;
                }
            }
            waitObj.SetActive(!b);
            if (b)
            {
                //保存
                int gameMode = (PhotonNetwork.CurrentRoom.CustomProperties["mode"] is int c) ? c : 0;
                string characterCode = "";
                if (gameMode == 0)
                {
                    characterCode += "A";
                }
                else
                {
                    characterCode += "B";
                }

                int playerCount = (PhotonNetwork.CurrentRoom.CustomProperties["playerCount" + "" + deviceNumber.ToString()] is int p) ? p : 0;
                int comCount = (PhotonNetwork.CurrentRoom.CustomProperties["comCount" + "" + deviceNumber.ToString()] is int w) ? w : 0;
                int a = 1;
                foreach (GameObject panel in panelList)
                {
                    if (a <= playerCount + comCount)
                    {
                        ChoicePanel choicePanel = panel.GetComponent<ChoicePanel>();

                        FindAnyObjectByType<PlaySettings>().Character(choicePanel.GetPlayerNumber(), characterCode + choicePanel.CharacterCode().ToString());
                        //タイミングを画面移動時にする
                    }
                    else
                    {

                        panel.SetActive(false);
                    }
                    a++;
                }


                //移動
                FindAnyObjectByType<ScreenChange>().OnClick(5);
                playerCount = 0;
                for (int i = 1; i <= PhotonNetwork.CurrentRoom.PlayerCount; i++)
                {
                    playerCount += (PhotonNetwork.CurrentRoom.CustomProperties["playerCount" + "" + i] is int op) ? op : 0;
                    playerCount += (PhotonNetwork.CurrentRoom.CustomProperties["comCount" + "" + i] is int oc) ? oc : 0;
                }
                PlayerPrefs.SetInt("AllPlayer", playerCount);
                FindAnyObjectByType<StageView>().View(playerCount);
            }
        }
    }
    public void ViewPanel()
    {
        int otherPlayerCount = 0;
        for(int i = 1;i < deviceNumber ;i++)
        {
            otherPlayerCount += (PhotonNetwork.CurrentRoom.CustomProperties["playerCount" + "" + i] is int op) ? op : 0;
            otherPlayerCount += (PhotonNetwork.CurrentRoom.CustomProperties["comCount" + "" + i] is int oc) ? oc : 0;
        }

        int playerCount = (PhotonNetwork.CurrentRoom.CustomProperties["playerCount" + "" + deviceNumber.ToString()] is int p) ? p : 0;
        int comCount = (PhotonNetwork.CurrentRoom.CustomProperties["comCount" + "" + deviceNumber.ToString()] is int c) ? c : 0;
        int a = 1;
        foreach (GameObject panel in panelList)
        {
            if (a <= playerCount + comCount)
            {
                panel.SetActive(true);
                panel.GetComponent<ChoicePanel>().SetName(otherPlayerCount + a);

            }
            else
            {

                panel.SetActive(false);
            }
            a++;
        }
    }
    public void OnClick()
    {
        nextButton.interactable = false;
        isClick = true;
        FindAnyObjectByType<PlaySettings>().Ready(deviceNumber, 1);
    }
}