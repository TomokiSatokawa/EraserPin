using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EraserControl : EraserControlBase
{
    public int playerPoitionX = 1;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
       //���͂����L�[�ɉ�����playerPosition��ς���
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            playerPoitionX++;
        }
        //�o��
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
