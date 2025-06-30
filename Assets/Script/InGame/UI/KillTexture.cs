using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTexture : MonoBehaviour
{
    [SerializeField] private Camera renderCamera;
    private RenderTexture renderTexture;

    public RenderTexture RenderTexture
    {
        get
        {
            if (renderTexture == null)
            {
                renderTexture = new RenderTexture(512, 512, 0, RenderTextureFormat.ARGB32);
                renderTexture.Create();
            }
            return renderTexture;
        }
    }

    void Start()
    {
        renderCamera.targetTexture = RenderTexture;
    }
}
