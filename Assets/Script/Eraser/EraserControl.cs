using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EraserControl : EraserControlBase
{
    public int playerPoitionX = 1;
    // Update is called once per frame
    void Update()
    {
        base.Update();

        //“ü—Í‚µ‚½ƒL[‚É‰‚¶‚ÄplayerPosition‚ğ•Ï‚¦‚é
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            playerPoitionX++;
        }
        //o—Í
        for (int i = 0; i < 10; i++)
        {

        }
        //Debug.Log(MapStr);
    }
    public override void StopProcess()
    {
        FindAnyObjectByType<StopCheck>().EffectCheck(playerNumber);
    }
}
