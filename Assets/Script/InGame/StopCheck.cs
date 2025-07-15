using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class StopCheck : MonoBehaviour
{
    public EraserClone eraserClone;
    public List<Vector3> beforeFramePositions = new List<Vector3>();
    public GameObject gameMasterPhotonView;
    public List<bool> effectCheck = new List<bool>();
    public bool isStop = false;
    public bool isCheck = false;
    public bool effectWait = false;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isCheck)
        {
            return;
        }
        isEraserStop();
        if (!isStop)
        {
            timer = 0f;
            return;
        }

        if (timer < 1f)
        {
            timer += Time.deltaTime;
            return;
        }

        

        if (!effectWait)//効果実行
        {
            effectWait = true;
            timer = 0f;
            isStop = false;
            ////gameMasterPhotonView.GetComponent<PhotonView>().RPC("NextTurn", RpcTarget.All);
            foreach (GameObject eraser in eraserClone.cloneEraserObjects)
            {
                if (eraser == null)
                {
                    continue;
                }
                eraser.GetComponent<EraserControlBase>().StopProcess();
                
            }
            return;

           
        }
        AliveCheck();
        bool checker = true;
        foreach (bool b in effectCheck)
        {
            if (!b)
            {
                checker = false;
            }
        }

        if (checker)//ターンエンド
        {
            FindAnyObjectByType<GameManager>().NextTurn();
            isCheck = false;
            return;
        }

    }
    public void Check()
    {
        isCheck = true;
        effectWait = false;
        timer = 0f;
        effectCheck.Clear();
        foreach (GameManager.PlayerData data in FindAnyObjectByType<GameManager>().playerList)
        {
            effectCheck.Add(!data.isAlive);
        }
    }
    public void AliveCheck()
    {
        int i = 0;
        foreach (GameManager.PlayerData data in FindAnyObjectByType<GameManager>().playerList)
        {
            if (!data.isAlive)
            {
                effectCheck[i] = true;
                timer = 0f;
            }
            i++;
        }
        }
    public void EffectCheck(int playerNumber)
    {
        effectCheck[playerNumber -1] = true;
        timer = 0f;
    }
    public  void isEraserStop()
    {


        bool Stop = true;
        int i = 0;
        foreach (GameObject eraser in eraserClone.cloneEraserObjects)
        {
            if (beforeFramePositions.Count == 0)
            {
                Stop = false;
                break;
            }
            Vector3 eraserPosition;
            if (eraser == null)
            {
                eraserPosition = Vector3.zero;
            }
            else
            {
                eraserPosition = eraser.transform.position;
            }
            bool a = Vector3.Distance(beforeFramePositions[i], eraserPosition) == 0f;
            if (a)
            {
                //Debug.Log(eraser.name + "" + Vector3.Distance(beforeFramePositions[i], eraser.transform.position));
            }
            Stop = Stop && a;
            if (!isStop)
            {
                //Debug.Log(eraser);
            }
            i++;
        }

        beforeFramePositions.Clear();
        foreach (GameObject eraser in eraserClone.cloneEraserObjects)
        {
            if (eraser == null)
            {
                beforeFramePositions.Add(Vector3.zero);
                continue;
            }
            beforeFramePositions.Add(eraser.transform.position);
        }

        isStop = Stop;
    }

}