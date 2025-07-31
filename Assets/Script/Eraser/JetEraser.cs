using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class JetEraser : HardEraserBase
{
    public GameObject fireEffect;
    public override void DataReset()
    {
        base.DataReset();
        fireEffect.SetActive(false);
    }
    public override void StopProcess()
    {
        base.StopProcess();
        fireEffect.SetActive(false);
    }
    [PunRPC]
    public override void EraserEffect(int number)
    {
        base.EraserEffect(number);
        FindAnyObjectByType<PowerSlider>().Jet();
        fireEffect.SetActive(true);
    }
}
