using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
public class StepperMove : MonoBehaviour
{
    public List<GameObject> elements;
    public GameObject backgroundObject;
    public int value;
    public  GameObject inputField; 
    // Start is called before the first frame update
    void Start()
    {
        value = 0;
        backgroundObject.transform.position = elements[0].transform.position;
    }

    public void OnClick(int a)
    {
        value = a;
        backgroundObject.transform.DOMove(elements[a].transform.position,0.25f);
        if(inputField != null)
        {
        inputField.SetActive(value == 0);

        }
    }
    public int GetData()
    {
        return value;
    }
}
