using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using DG.Tweening;
public class StealthEraser : HardEraserBase
{
    public Color alpha;
    public enum RenderingMode
    {
        Opaque,
        Cutout,
        Fade,
        Transparent
    }
    public override void MyTurn()
    {
        CoverAlphaChange(1);
        IsTriggerChange(false);
        this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }
    [PunRPC]
    public override void EraserEffect(int number)
    {
        Debug.Log(number +":"+ playerNumber);
        base.EraserEffect(number);
        CoverAlphaChange(alpha.a);
        IsTriggerChange(true);
        this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }
    public void CoverAlphaChange(float a)
    {
        MeshRenderer mr = coverObject.GetComponent<MeshRenderer>();
        Material mt = mr.materials[0];
        if (a < 1)
        {
            SetMaterialRenderingMode(mt, RenderingMode.Transparent);
        }
        else
        {
            SetMaterialRenderingMode(mt, RenderingMode.Opaque);
        }
        Color color = mt.color;
        color.a = 0.7f;
        mt.color = color;
        //UnityEditor.EditorApplication.isPaused = true;
        mt.DOFade(a, 1f);   
        mr.materials[0] = mt;
    }
    public void IsTriggerChange(bool b)
    {
        var children = new GameObject[transform.childCount];

        // 0`ŒÂ”-1‚Ü‚Å‚ÌŽq‚ð‡”Ô‚É”z—ñ‚ÉŠi”[
        for (var i = 0; i < children.Length; ++i)
        {
            // Transform‚©‚çƒQ[ƒ€ƒIƒuƒWƒFƒNƒg‚ðŽæ“¾‚µ‚ÄŠi”[
            children[i] = transform.GetChild(i).gameObject;
        }
        foreach (var child in children)
        {
            if (child.GetComponent<Collider>() != null)
            {
                child.GetComponent<Collider>().isTrigger = b;
            }
        }
       
    }
    public static void SetMaterialRenderingMode(Material material, RenderingMode mode)
    {
        switch (mode)
        {
            case RenderingMode.Opaque:
                material.SetFloat("_Mode", 0);
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                material.SetInt("_ZWrite", 1);
                material.DisableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = -1;
                break;

            case RenderingMode.Cutout:
                material.SetFloat("_Mode", 1);
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                material.SetInt("_ZWrite", 1);
                material.EnableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = 2450;
                break;

            case RenderingMode.Fade:
                material.SetFloat("_Mode", 2);
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                material.SetInt("_ZWrite", 0);
                material.DisableKeyword("_ALPHATEST_ON");
                material.EnableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = 3000;
                break;

            case RenderingMode.Transparent:
                material.SetFloat("_Mode", 3);
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                material.SetInt("_ZWrite", 0);
                material.DisableKeyword("_ALPHATEST_ON");
                material.DisableKeyword("_ALPHABLEND_ON");
                material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                material.renderQueue = 3000;
                break;
        }
    }
}

