using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SocialPlatforms.Impl;
using Photon.Pun;
using UnityEngine.UIElements;
public class CameraWork : MonoBehaviourPunCallbacks
{
    public bool isFocus;
    public float cameraZoomDefault = 60;
    public GameObject topCameraPosition;
    public GameObject focusObject;
    public GameObject LeftPosition;
    public GameObject RightPosition;
    public GameManager gameManager;
    public EraserClone eraserClone;
    public PointerControl pointerControl;
    public HandControl handControl;
    private GameObject targetEraser;
    // Start is called before the first frame update
    void Start()
    {
        AnimationActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void AnimationActive(bool a)
    {
        this.gameObject.GetComponent<Animator>().enabled = a;
    }
    public void TopFocus()
    {
        //    this.transform.position = new Vector3(0, 4.0999999f, 7.77699995f);
        //    this.transform.eulerAngles = new Vector3(26.6469975f, 0, 0);
        //Debug.Log("a");
        Vector3 movePosition;
        if (isFocus)
        {
            movePosition = eraserClone.cloneEraserObjects[gameManager.turn - 1].transform.position;
            movePosition.y = topCameraPosition.transform.position.y;
        }
        else
        {
            movePosition = topCameraPosition.transform.position;
        }

        //Debug.Log(movePosition);

        Zoom(0, 2f,0.5f);
        this.gameObject.transform.DOMove(movePosition, 2f, false).SetDelay(0.5f);
        this.gameObject.transform.DORotate(topCameraPosition.transform.eulerAngles, 2f).SetDelay(0.5f);

    }
    [PunRPC]
    public void EraserFocus(GameObject target, bool isLeft)
    {
        focusObject.transform.position = target.transform.position;
        focusObject.transform.LookAt(target.transform.position + pointerControl.GetData(FindAnyObjectByType<PowerSlider>().GetData()) * 10);
        Vector3 rotation = focusObject.transform.eulerAngles;
        rotation.x = 0;
        rotation.y += 180;
        rotation.z = 0;
        focusObject.transform.eulerAngles = rotation;
        GameObject position;
        if (isLeft)
        {
            position = LeftPosition;
        }
        else
        {
            position = RightPosition;
        }
        targetEraser = target;
        Zoom(20, 2f);
        this.gameObject.transform.DOMove(position.transform.position, 2f, false);
        this.gameObject.transform.DORotate(position.transform.eulerAngles, 2f)
            .OnComplete(Hand);
    }
    public void Hand()
    {
        handControl.Active(true, targetEraser.GetComponent<EraserControl>());
    }
    public void Zoom(float value, float time,float dlay = 0)
    {
        DOTween.To(() => this.GetComponent<Camera>().fieldOfView, num => this.GetComponent<Camera>().fieldOfView = num,cameraZoomDefault + value,time ).SetDelay(dlay);
        
    }
    [PunRPC]
    public void Result()
    {
        Zoom(20, 2);
        this.gameObject.transform.DOMove(new Vector3(1.29999995f, 4.13727474f, 23.7900009f), 2f, false);
        this.gameObject.transform.DORotate(Vector3.zero, 2f)
            .OnComplete(() => gameManager.Ranking());
    }
}
