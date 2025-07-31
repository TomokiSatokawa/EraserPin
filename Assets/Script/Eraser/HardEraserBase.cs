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
    protected Vector3 eraserPosition;
    public enum Timing
    {
        Before, After
    }
    public Timing effectTiming = Timing.After;
    public override void DataReset()
    {
        SliderActive(false);
        moveAmount = 0;
        eraserPosition = this.transform.position;
        moveAmountSlider.value = 0;
        moveAmountSlider.maxValue = maxAmount;
    }
    public override void StopProcess()
    {
        SliderActive(true);
        moveAmount += Vector3.Distance(eraserPosition, this.transform.position);
        if(moveAmount >= maxAmount)
        {
            moveAmount = maxAmount;
        }
        moveAmountSlider.DOValue(moveAmount, 1f).SetDelay(1f).OnComplete(() => ValueCheck(Timing.After));

    }
    public override void MyTurn()
    {
        ValueCheck(Timing.Before);
    }
    public void ValueCheck(Timing t)
    {
        SliderActive(false);
        if (moveAmount >= maxAmount && t == effectTiming)
        {
            photonView.RPC(nameof(EraserEffect), RpcTarget.All, playerNumber);
        }
        eraserPosition = this.transform.position;
        if (t == Timing.After)
        {
            FindAnyObjectByType<StopCheck>().EffectCheck(playerNumber);

        }
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
    public void SliderActive(bool b)
    {
        moveAmountSlider.gameObject.SetActive(b);
    }
}
