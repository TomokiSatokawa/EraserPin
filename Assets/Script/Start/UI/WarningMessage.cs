using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WarningMessage : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public GameObject warningObject;
    void Start()
    {
        warningObject.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        warningObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        warningObject.SetActive(false);
    }
}
