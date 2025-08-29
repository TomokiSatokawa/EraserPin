using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using Photon.Pun;

[DisallowMultipleComponent]
public class ChatManager : MonoBehaviourPunCallbacks
{
    [Header("éQè∆ê›íË")]
    [SerializeField] private RectTransform contentParent;
    [SerializeField] private GameObject messagePrefab;

    [Header("ê›íË")]
    [SerializeField] private int maxMessages = 50;
    [SerializeField] private float lifeTime = 3f;
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private float verticalSpacing = 6f;
    [SerializeField] private float extraHeightPadding = 6f;
    [SerializeField] private float fontSize = 24f; 
    private class MsgItem
    {
        public GameObject go;
        public RectTransform rt;
        public CanvasGroup cg;
        public float height;
    }

    private readonly List<MsgItem> messages = new List<MsgItem>();

    [PunRPC]
    public void ChatMessage(string message)
    {
        if (messagePrefab == null || contentParent == null) return;

        GameObject inst = Instantiate(messagePrefab, contentParent);
        inst.SetActive(true);

        RectTransform rt = inst.GetComponent<RectTransform>();
        TMP_Text tmp = inst.GetComponentInChildren<TMP_Text>(true);

        if (rt == null || tmp == null)
        {
            Destroy(inst);
            return;
        }

        CanvasGroup cg = inst.GetComponent<CanvasGroup>();
        if (cg == null) cg = inst.gameObject.AddComponent<CanvasGroup>();
        cg.alpha = 1f;

        tmp.text = message;
        tmp.enableWordWrapping = true;
        tmp.overflowMode = TextOverflowModes.Overflow;
        tmp.alignment = TextAlignmentOptions.TopLeft;
        tmp.fontSize = fontSize;
        // â°ïùÇêeÇ…çáÇÌÇπÇÈ
        rt.anchorMin = new Vector2(0f, 0f);
        rt.anchorMax = new Vector2(1f, 0f);
        rt.pivot = new Vector2(0.5f, 0f);
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, contentParent.rect.width);

        // çÇÇ≥åvéZ
        Vector2 pref = tmp.GetPreferredValues(message, contentParent.rect.width, 0f);
        float height = pref.y + extraHeightPadding;
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);

        MsgItem item = new MsgItem { go = inst, rt = rt, cg = cg, height = height };
        messages.Add(item);

        while (messages.Count > maxMessages)
            RemoveOldestImmediate();

        ArrangeAllItems();

        DOTween.Sequence()
            .AppendInterval(lifeTime)
            .Append(item.cg.DOFade(0f, fadeDuration))
            .OnComplete(() =>
            {
                RemoveItemAndArrange(item);
            });
    }

    private void RemoveOldestImmediate()
    {
        if (messages.Count == 0) return;
        MsgItem oldest = messages[0];
        messages.RemoveAt(0);
        if (oldest.go != null) Destroy(oldest.go);
    }

    private void RemoveItemAndArrange(MsgItem item)
    {
        if (messages.Contains(item))
        {
            messages.Remove(item);
            if (item.go != null) Destroy(item.go);
            ArrangeAllItems();
        }
    }

    private void ArrangeAllItems()
    {
        float offsetY = 0f;

        for (int i = messages.Count - 1; i >= 0; i--)
        {
            MsgItem item = messages[i];
            item.rt.localPosition = new Vector3(0f, offsetY, 0f);
            offsetY += item.height + verticalSpacing;
        }
    }

    public void ClearAll()
    {
        foreach (var item in messages)
            if (item.go != null) Destroy(item.go);
        messages.Clear();
    }
}