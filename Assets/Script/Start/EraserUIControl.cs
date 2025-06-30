using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EraserUIControl : MonoBehaviour
{
    public int playerNumber;
    public ColorData colorData;
    public Image boody;
    public Image package;
    public GameObject cpuText;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        Active(playerNumber == 1);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    public void Active(bool b)
    {
        if (b)
        {
            boody.color = colorData.activeColorBody;
            package.color = colorData.activeColorPackage[playerNumber - 1];
        }
        else
        {
            boody.color = colorData.hiddenColorBody;
            package.color = colorData.hiddenColorPackage;
        }
    }
    public void IsCom(bool b)
    {
        cpuText.SetActive(b);
    }
}
