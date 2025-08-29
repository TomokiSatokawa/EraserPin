using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TutorialControl : MonoBehaviour
{
    public Image image;
    public Image image2;
    public GameObject panelObject;
    public float moveX = 1800;
    public float moveTime = 0.5f;
    public List<Sprite> images;
    private bool isMoving = false;
    public int _index;
    private int index
    {
        get { return _index; }
        set
        {
            if (value < 0)
                _index = images.Count - 1;
            else if (value >= images.Count)
                _index = 0;
            else
                _index = value;
        }
    }

    public void Active(bool a)
    {
        panelObject.SetActive(a);
        if (a)
        {
            index = 0;
            image.sprite = images[index];
            image.rectTransform.anchoredPosition = Vector2.zero;
            image.gameObject.SetActive(true);
            image2.gameObject.SetActive(false);
        }
    }

    public void OnClick(bool next)
    {
        if (isMoving) return;
        isMoving = true;

        float dir = next ? 1 : -1;

        // image2 を現在の画像として表示
        image2.gameObject.SetActive(true);
        image2.sprite = image.sprite;
        image2.rectTransform.anchoredPosition = Vector2.zero;

        // 新しい画像を image にセットし、左右から登場させる
        index += next ? 1 : -1;
        image.sprite = images[index];
        image.rectTransform.anchoredPosition = new Vector2(dir * moveX, 0);

        // アニメーション
        image.rectTransform.DOAnchorPos(Vector2.zero, moveTime).SetEase(Ease.OutCubic);
        image2.rectTransform.DOAnchorPos(new Vector2(-dir * moveX, 0), moveTime).SetEase(Ease.OutCubic)
            .OnComplete(() =>
            {
                image2.gameObject.SetActive(false);
                isMoving = false;
            });
    }
}
