using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class StartCameraWork : MonoBehaviour
{
    public GameObject mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera.transform.position = new Vector3(0f, 24.5699997f, -44.8100014f);
        mainCamera.transform.eulerAngles = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartPosition()
    {
        mainCamera.transform.DOMove(new Vector3(0f, 24.5699997f, -44.8100014f), 1f);
        mainCamera.transform.DORotate(Vector3.zero, 1f);
    }
    public void TableFocus()
    {
        mainCamera.transform.DOMove(new Vector3(2.102f, 24.5510006f, -46.3540001f),1f);
        mainCamera.transform.DORotate(new Vector3(25.8609924f, 297.965759f, -3.79516086e-06f), 1f);
    }
}
