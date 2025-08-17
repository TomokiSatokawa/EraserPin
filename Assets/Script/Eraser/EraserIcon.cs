using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EraserIcon : MonoBehaviour
{
    public GameObject body;
    public GameObject cover;
    public GameObject decoration;
    public Color dropoutColor;
    private int playerNumber;
    private Animator anim;
    private Color coverColor;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetData(Sprite b, Sprite c, Sprite d)
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
        coverColor = color;
    }
    public void ActiveOutline(int number)
    {
        anim.enabled = false;
        anim.enabled = number == playerNumber;
        if (number == playerNumber)
        {
            Debug.Log("trun");
            anim.Play("Blink", 0, 0f);
        }
    }
    public void Active(bool b)
    {
        Debug.Log(b);
        if (!b)
        {
            body.GetComponent<Image>().color = Color.white;
            cover.GetComponent<Image>().color = coverColor;
            decoration.GetComponent<Image>().color = Color.white;
        }
        else
        {
            body.GetComponent<Image>().color = dropoutColor;
            cover.GetComponent<Image>().color = AddColor(dropoutColor, coverColor);
            decoration.GetComponent<Image>().color = dropoutColor;
        }
    }
    public Color AddColor(Color drop,Color def)
    {
        float h1;
        float s1;
        float v1;
        Color.RGBToHSV(drop, out h1, out s1, out v1);

        float h2;
        float s2;
        float v2;
        Color.RGBToHSV(def, out h2, out s2, out v2);

        h2 -= h1;
        s2 -= s1;
        v2 -= v1;
       return  Color.HSVToRGB(h2, s2, v2);
    }

}