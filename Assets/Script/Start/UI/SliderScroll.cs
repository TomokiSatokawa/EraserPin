using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class SliderScroll : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public UnityEvent up;
    public UnityEvent down;
    private bool isPointerStay = false;
    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerStay = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerStay = false;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isPointerStay)
        {
            float scroll = Input.mouseScrollDelta.y;

            if (scroll > 0f)   // 上方向に1ノッチ
            {
                up.Invoke();
            }
            else if (scroll < 0f) // 下方向に1ノッチ
            {
                down.Invoke();
            }
        }
    }
}

