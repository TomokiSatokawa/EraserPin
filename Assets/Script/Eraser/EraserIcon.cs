using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EraserIcon : MonoBehaviour

{

    public GameObject body;
    public GameObject cover;
    public GameObject decoration;
    public GameObject Outline;
    private int playerNumber;
    // Start is called before the first frame update
    void Start()
    {
        Outline.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetData(Sprite b,Sprite c , Sprite d)
    {
        body.GetComponent<Image>().sprite = b;
        cover.GetComponent<Image>().sprite = c;
        decoration.GetComponent<Image>().sprite = d;
    }
    public void SetPlayerNumber(int number)
    {
        playerNumber = number;
    }
    public void ChangeColor(Color color)
    {
        cover.GetComponent<Image>().color = color;
    }
    public void ActiveOutline(int number)
    {
        Outline.SetActive(number == playerNumber);
    }
}
