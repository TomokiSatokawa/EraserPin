using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Photon.Pun.Demo.Cockpit;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class BackgroundScaler : ButtonScalerTween, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField, Header("ƒ{ƒ^ƒ“‚Æ“¯Žž‚É‘å‚«‚­‚·‚é")]
    private GameObject[] obj = new GameObject[1];

    private List<Vector3> originalScaleList = new List<Vector3>();
    private List<Tween> currentTweenList = new List<Tween>();
    // Start is called before the first frame update
    void Start()
    {
        originalScaleList.Clear();
        foreach (GameObject g in obj)
        {
            originalScaleList.Add(g.transform.localScale);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        foreach (Tween t in currentTweenList)
        {
            t?.Kill();
        }
        bool isAdd = currentTweenList.Count != obj.Length;
        int i = 0;
        foreach (GameObject g in obj)
        {
            Tween t = g.transform.DOScale(originalScaleList[i] * scaleUp, duration).SetEase(Ease.OutBack);
            if (isAdd)
            {
                currentTweenList.Add(t);
            }
            i++;
        }
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        foreach (Tween t in currentTweenList)
        {
            t?.Kill();
        }
        int i = 0;
        foreach (GameObject g in obj)
        {
            Tween t = g.transform.DOScale(originalScaleList[i], duration).SetEase(Ease.OutBack);
                i++;
        }
    }
}