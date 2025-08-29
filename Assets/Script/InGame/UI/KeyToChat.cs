using System;
using UnityEngine;

[DisallowMultipleComponent]
public class KeyToChat : MonoBehaviour
{
    // 全キー監視（テスト用にシンプルに実装）
    private void Update()
    {
        if (!Input.anyKeyDown) return;

        foreach (KeyCode kc in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(kc))
            {
                // 変換は KeyCode.ToString() のみ
                FindAnyObjectByType<ChatManager>().ChatMessage(kc.ToString());
                break; // 1フレームにつき最初に検出したキーだけ処理
            }
        }
    }
}
