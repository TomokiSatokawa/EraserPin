using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PointerControl : MonoBehaviour
{
    public bool isActive = false;
    public Camera mainCamera;
    public GameObject pointerObject;
    public float maxRayDistance;
    public GameObject hitMarker;
    public GameObject targetEraser;
    public GameObject nextButton;
    private int turn;
    private Vector3 hitPosition;
    private Vector3 vector;
    private RaycastHit hitEraser;
    // Start is called before the first frame update
    void Start()
    {
        nextButton.SetActive(false);
        hitMarker.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            Vector3 mousePositin =Vector3.zero;
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(mouseRay, out RaycastHit mouseHit))
            {
                mousePositin = mouseHit.point; // ヒットした場所のワールド座標
            }
            mousePositin.y = targetEraser.transform.position.y;
            pointerObject.transform.position = mousePositin;
            pointerObject.transform.LookAt(targetEraser.transform.position);

            Ray ray = new Ray(pointerObject.transform.position,pointerObject.transform.forward);
 
            Debug.DrawRay(pointerObject.transform.position, pointerObject.transform.forward * maxRayDistance, Color.red);
            hitPosition = Vector3.zero;
            foreach (RaycastHit hit in Physics.RaycastAll(ray,maxRayDistance))
            {
                GameObject hitObject = hit.collider.gameObject.transform.parent.gameObject;
                if(hitObject.GetComponent<EraserControlBase>() == null)
                {
                    continue;
                }
                int hitPlayerNumber = hitObject.GetComponent<EraserControlBase>().playerNumber;
                if (hitPlayerNumber == targetEraser.GetComponent<EraserControlBase>().playerNumber )
                {
                    if(hitPosition == Vector3.zero || Vector3.Distance(pointerObject.transform.position,hit.point) < Vector3.Distance(pointerObject.transform.position,hitPosition))
                    {
                        //Debug.Log("D");
                        hitPosition = hit.point;
                        Vector3 position = hitPosition;
                        position.y = targetEraser.GetComponent<EraserControlBase>().GetTopPosition();
                        hitMarker.transform.position = position;
                        hitEraser = hit;
                        continue;
                    }
                   
                } 
                
            }
                
        }
        hitMarker.SetActive(isActive);


        if(hitEraser.collider != null)
        {
            Debug.DrawRay(hitEraser.point, hitEraser.normal.normalized * -10,Color.blue);
        }
    }
    public void Active(bool a,GameObject eraserObject,int deveiceNumber)
    {
        isActive = a && deveiceNumber == PlayerPrefs.GetInt("Dnumber");
        targetEraser = eraserObject;
        nextButton.SetActive(true);
    }
    public void Stop()
    {
        isActive = false;
        nextButton.SetActive(false);
        FindAnyObjectByType<GameManager>().Power();
        Vector3 local = hitEraser.normal; //pointerObject.transform.forward;
        vector = local.normalized;
        vector *= -1;
    }
    public Vector3 GetData()
    {
        return vector;
    }
    public Vector3 GetHitPosition()
    {
        return hitPosition;
    }
}
