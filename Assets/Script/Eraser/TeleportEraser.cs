using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class TeleportEraser : HardEraserBase
{
    [PunRPC]
    public override void EraserEffect(int number)
    {
        base.EraserEffect(number);
        List<GameObject> list = FindAnyObjectByType<TeleportPointList>().pointList;
        int randomIndex = Random.Range(0, list.Count - 1);
        Vector3 position = list[randomIndex].transform.position;
        position.y = this.transform.position.y;
        this.transform.position = position;
        eraserPosition = position;
    }
}
