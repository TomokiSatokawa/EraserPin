using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;
using static UnityEngine.UI.Image;
public class InputPass : MonoBehaviour
{
    public string pass;
    public TextMeshProUGUI passText;
    public Button nextButton;
    // Start is called before the first frame update
    void Start()
    {
        passText.SetText("ルームパスを入力");
    }

    // Update is called once per frame
    void Update()
    {
        nextButton.interactable = pass.Length == 4;
    }
    public void OnClick(int a)
    {
        pass += a.ToString();
        if (pass.Length > 4)
        {
            pass = "";
        }
        LoadText();
    }
    public void delete()
    {
        if (pass.Length == 0)
        {
            return;
        }
        pass = pass.Remove(pass.Length - 1);
        if (pass.Length > 4)
        {
            pass = "";
        }
        LoadText();
    }
    public void LoadText()
    {
        if (pass.Length == 1)
        {
            passText.SetText(pass + "XXX");
        }
        else if (pass.Length == 2)
        {
            passText.SetText(pass + "XX");
        }
        else if (pass.Length == 3)
        {
            passText.SetText(pass + "X");
        }
        else if (pass.Length == 4)
        {
            passText.SetText(pass);
        }
        else if (pass.Length == 0)
        {
            passText.SetText("ルームパスを入力");
        }
    }
    public string GetPass()
    {
        return pass;
    }
}