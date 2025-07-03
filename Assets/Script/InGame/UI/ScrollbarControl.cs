using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using static GameManager;

public class ScrollbarControl : MonoBehaviour
{
    public ScrollRect scrollRect;
    public GameObject content;
    public GameObject clonePrefab;
    public List<GameObject> clonedObject;
    // Start is called before the first frame update
    void Start()
    {
        content.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void View(List<PlayerData> playerList,List<int> ranking)
    {
        //scrollRect.enabled = false;
        content.SetActive(true);
        Clone(playerList, ranking);

        scrollRect.verticalNormalizedPosition = 0f;


    }
    public void FadeIn()
    {
        //    for(int i= ;)
    }
    public void Clone(List<PlayerData> playerList, List<int> ranking)
    {
        clonedObject = new List<GameObject>();
        foreach(int i in ranking)
        {
            GameObject newObject = Instantiate(clonePrefab,content.transform);
            clonedObject.Add(newObject);
        }
    }
}
