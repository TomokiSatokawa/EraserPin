using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class ComMove : MonoBehaviour
{
    public PowerSlider powerSlider;
    public PointerControl pointerControl;
    private EraserClone eraserClone;
    // Start is called before the first frame update
    void Start()
    {
        eraserClone = FindAnyObjectByType<EraserClone>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Input(GameObject comEraser)
    {
        GameObject near = null;
        int i = 0;
        foreach(GameObject obj in eraserClone.cloneEraserObjects)
        {
            if(i == 0)
            {
                near = obj;
                continue;
            }
            if (obj == near)
            {
                continue;
            }
            float distance1 = Vector3.Distance(near.transform.position,comEraser.transform.position);
            float distance2 = Vector3.Distance(obj.transform.position,comEraser.transform.position);
            if(distance1 > distance2)
            {
                near = obj;
            }
        }
        Debug.Log(near.name);
        Vector3 direction = near.transform.position - comEraser.transform.position; 
        EraserControlBase eraserControl = comEraser.GetComponent<EraserControlBase>();
        Vector3 startPosition = -direction * Vector3.Distance(comEraser.transform.position, eraserControl.backPosition.transform.position) * 2;
        Vector3 goalPosition = near.transform.position;
        RaycastHit? hit = Ray(startPosition, goalPosition,comEraser);
        
    }
    public RaycastHit? Ray(Vector3 s, Vector3 g,GameObject t)
    {
        Ray ray = new Ray(s, g);
        Vector3 hitPosition = Vector3.zero;
        RaycastHit? hitEraser = null;
        foreach(RaycastHit hit in Physics.RaycastAll(ray))
        {
            if (!hit.collider.gameObject.CompareTag("EraserMesh"))
            {
                continue;
            }
            GameObject hitObject = hit.collider.gameObject.transform.parent.gameObject;
            if (hitObject.GetComponent<EraserControlBase>() == null)
            {
                continue;
            }
            int hitPlayerNumber = hitObject.GetComponent<EraserControlBase>().playerNumber;
            if (hitPlayerNumber == t.GetComponent<EraserControlBase>().playerNumber)
            {
                if (hitPosition == Vector3.zero || Vector3.Distance(s, hit.point) < Vector3.Distance(pointerObject.transform.position, hitPosition))
                {
                    //Debug.Log("D");
                    hitPosition = hit.point;
                    hitEraser =  hit;
                }

            }
        }
        return hitEraser;
    }
}
