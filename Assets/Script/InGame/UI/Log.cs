using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Log : MonoBehaviour
{
    public GameObject prefab;
    public static GameObject _prefab;
    public GameObject content;
    public static GameObject _content;
    public static List<GameObject> clonedObj = new List<GameObject>();
    public GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
        _prefab = prefab;
        _content = content;
        obj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            obj.SetActive(!obj.activeSelf);
        }
    }
    public static void text(object text)
    {
        GameObject newObj = Instantiate(_prefab, _content.transform);
        newObj.SetActive(true);
        newObj.GetComponent<TextMeshProUGUI>().text = text.ToString();
        clonedObj.Add(newObj);
    }
    public void OnClick()
    {
        foreach (GameObject obj in clonedObj)
        {
            Destroy(obj);
        }
        clonedObj.Clear();
    }
}
