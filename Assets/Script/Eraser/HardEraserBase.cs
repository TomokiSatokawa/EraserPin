using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Photon.Pun;
public class HardEraserBase : EraserControlBase
{
    public float moveAmount;
    public float maxAmount;
    public Slider moveAmountSlider;
    private Vector3 eraserPosition;
    public override void DataReset()
    {
        moveAmount = 0;
        eraserPosition = this.transform.position;
        moveAmountSlider.value = 0;
        moveAmountSlider.maxValue = maxAmount;
    }
    public override void StopProcess()
    {
        moveAmount += Vector3.Distance(eraserPosition, this.transform.position);
        moveAmountSlider.DOValue(moveAmount, 1f).OnComplete(ValueCheck);
    }
    public void ValueCheck()
    {
        if (moveAmount > maxAmount)
        {
            photonView.RPC(nameof(EraserEffect), RpcTarget.All, playerNumber);
        }
        eraserPosition = this.transform.position;
        FindAnyObjectByType<StopCheck>().EffectCheck(playerNumber);
    }
    [PunRPC]
    public virtual void EraserEffect(int number)
    {
        if (playerNumber != number)
        {
            return;
        }
        DataReset();
    }
}
