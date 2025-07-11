using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using static GameManager;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;
public class ScrollbarControl : MonoBehaviourPunCallbacks
{
    public ScrollRect scrollRect;
    public GameObject content;
    public GameObject clonePrefab;
    public List<GameObject> clonedObject;
    public GameObject backButton;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        content.SetActive(false);
        gameManager = FindAnyObjectByType<GameManager>();
        backButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void View(List<PlayerData> playerList)
    {
        //scrollRect.enabled = false;
        content.SetActive(true);
        List<int> ranking = new List<int>();
        for (int i = 1; i <= gameManager.playerList.Count; i++)
        {
            int inum = (PhotonNetwork.CurrentRoom.CustomProperties["ranking" + "" + i.ToString()] is int a) ? a : 0;
            ranking.Add(inum);
        }
        Clone(playerList,ranking);

        scrollRect.verticalNormalizedPosition = 0;

       backButton.SetActive(true);
    }
    public void Clone(List<PlayerData> playerList , List<int> ranking)
    {
        clonedObject = new List<GameObject>();
        int a = 0;
        foreach (int i in ranking)
        {
            GameObject newObject = Instantiate(clonePrefab, content.transform);
            clonedObject.Add(newObject);
            newObject.GetComponent<RankingPanelControl>().SetData(a + 1, ranking[a]);
            a++;
        }
        GameObject newObject_ = Instantiate(clonePrefab, content.transform);
        newObject_.GetComponent<RankingPanelControl>().Hidden();
        newObject_ = Instantiate(clonePrefab, content.transform);
        newObject_.GetComponent<RankingPanelControl>().Hidden(); 
        newObject_ = Instantiate(clonePrefab, content.transform);
        newObject_.GetComponent<RankingPanelControl>().Hidden();
    }
    public void Back()
    {
        Debug.Log("êÿífíÜ");
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.Disconnect();
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("êÿífäÆóπ");
        SceneManager.LoadScene("Start");
    }
}