using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
public class NicknameManager : MonoBehaviour
{
    public TMP_InputField inputField;
    public TextMeshProUGUI errorText;
    public Button okButton;
    public GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnSubmit()
    {
        string text = inputField.text;
        Debug.Log("入力内容: " + text);
    }
    // Update is called once per frame
    void Update()
    {
        string st = CheckName(inputField.text);
        errorText.text = st;
        okButton.interactable = st == "";

        if(obj.activeSelf && Input.GetKeyDown(KeyCode.Return))
        {
            okButton.onClick.Invoke();
        }
    }
    public void OnClick()
    {
        PhotonNetwork.NickName = inputField.text;
        PlayerPrefs.SetString("Name", inputField.text);
        Active(false);
    }
    public void Active(bool a)
    {
        obj.SetActive(a);
    }
    public string CheckName(string input)
    {
        if(input.Length == 0)
        {
            return "  ";
        }
        if(input.Length > 10)
        {
            return "10文字以下にしてください。";
        }
        bool isNumberOnly = true;
        foreach(char c in input)
        {
            if(c == ' ' || c == '　')
            {
                return "空白を入れることはできません。";
            }
            if(c == '\\')
            {
                return "\\" + "は使えません。";
            }
            if (!char.IsDigit(c))
            {
                isNumberOnly = false;
            }
        }
        if (isNumberOnly)
        {
            return "数字のみ名前は使えません。";
        }
        return "";
    }
}
