using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FrameControl : MonoBehaviour
{
    public List<GameObject> frameObjects;
    public GameObject playerText;
    public float alpha;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        Active(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Active(bool a)
    {
        foreach(GameObject obj in frameObjects)
        {
            obj.SetActive(a);
        }
        playerText.SetActive(a);
        anim.enabled = a;
        if (a)
        {
            anim.Play("move", 0, 0.0f);
        }
    }
    public void ChangeColor(Color color, int playerNumber)
    {
        color.a = alpha;
        foreach (GameObject obj in frameObjects)
        {
            obj.GetComponent<Image>().color = color;
        }
        playerText.GetComponent<TextMeshProUGUI>().SetText(playerNumber +"P");
    }
}
