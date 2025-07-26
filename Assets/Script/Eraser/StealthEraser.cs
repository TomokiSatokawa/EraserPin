using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class StealthEraser : HardEraserBase
{
    public Color alpha;
    public Material normal;
    public Material stealth;
    public override void MyTurn()
    {
        CoverAlphaChange(1);
        IsTriggerChange(false);
        this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }
    [PunRPC]
    public override void EraserEffect(int number)
    {
        base.EraserEffect(number);
        CoverAlphaChange(alpha.a);
        IsTriggerChange(true);
        this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }
    public void CoverAlphaChange(float a)
    {

        MeshRenderer mr = coverObject.GetComponent<MeshRenderer>();
        Color color = mr.materials[0].color;
        color.a = a;
        Material material;
        if (a < 1)
        {
            material =  new Material(stealth);
        }
        else
        {
            material = new Material(normal);
        }
        material.color = color;
        mr.materials[0] = material;
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
}
