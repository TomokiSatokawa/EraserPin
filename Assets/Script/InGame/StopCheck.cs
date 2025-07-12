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

        if (timer < 0.5f)
        {
            timer += Time.deltaTime;
            return;
        }



        isStop = true;
        int i = 0;
        foreach (GameObject eraser in eraserClone.cloneEraserObjects)
        {
            if (beforeFramePositions.Count == 0)
            {
                isStop = false;
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
            isStop = isStop && a;
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

        if (isStop && timer < 2f)
        {
            timer += Time.deltaTime;
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

        if (isStop && checker)
        {
            FindAnyObjectByType<GameManager>().NextTurn();
            isCheck = false;
            return;
        }

        if (isStop && !effectWait)
        {
            ////gameMasterPhotonView.GetComponent<PhotonView>().RPC("NextTurn", RpcTarget.All);
            foreach (GameObject eraser in eraserClone.cloneEraserObjects)
            {
                if (eraser == null)
                {
                    continue;
                }
                eraser.GetComponent<EraserControlBase>().StopProcess();
                effectWait = true;
                timer = 0f;
            }
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
            }
            i++;
        }
        }
    public void EffectCheck(int playerNumber)
    {
        effectCheck[playerNumber -1] = true;
    }

}