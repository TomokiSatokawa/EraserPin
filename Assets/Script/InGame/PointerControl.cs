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
    public DirectionRotation directionRotation;
    private int turn;
    private Vector3 hitPosition;
    private Vector3 vector;
    private RaycastHit hitEraser;
    // Start is called before the first frame update
    void Start()
    {
        nextButton.SetActive(false);
        hitMarker.SetActive(false);
        hitMarker.GetComponent<MeshRenderer>().materials[0].color = Color.black;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            Vector3 mousePositin = Vector3.zero;
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(mouseRay, out RaycastHit mouseHit))
            {
                mousePositin = mouseHit.point; // ヒットした場所のワールド座標
            }
            mousePositin.y = targetEraser.transform.position.y;
            pointerObject.transform.position = mousePositin;
            pointerObject.transform.LookAt(targetEraser.transform.position);

            


            Ray ray = new Ray(pointerObject.transform.position, pointerObject.transform.forward);

            Debug.DrawRay(pointerObject.transform.position, pointerObject.transform.forward * maxRayDistance, Color.red);
            hitPosition = Vector3.zero;
            if (hitEraser.collider != null)
            {
                Debug.DrawRay(hitEraser.point, hitEraser.normal.normalized * -10, Color.blue);
            }
            foreach (RaycastHit hit in Physics.RaycastAll(ray, maxRayDistance))
            {
                if(!hit.collider.gameObject.CompareTag("EraserMesh"))
                {
                    continue;
                }
                GameObject hitObject = hit.collider.gameObject.transform.parent.gameObject;
                EraserControlBase hitBase = hitObject.GetComponent<EraserControlBase>();
                if (hitBase == null)
                {
                    continue;
                }

                float pointerDistance = Vector3.Distance(mousePositin, hitObject.transform.position);
                if(pointerDistance <= Vector3.Distance(hitObject.transform.position,hitBase.backPosition.transform.position) * 1.05f)
                {
                    return;
                }

                int hitPlayerNumber = hitObject.GetComponent<EraserControlBase>().playerNumber;
                if (hitPlayerNumber == targetEraser.GetComponent<EraserControlBase>().playerNumber)
                {
                    if (hitPosition == Vector3.zero || Vector3.Distance(pointerObject.transform.position, hit.point) < Vector3.Distance(pointerObject.transform.position, hitPosition))
                    {
                        //Debug.Log("D");
                        RaycastHit? h = NormalRay(hit);
                        if(h  ==  null)
                        {
                            //pointerObject.SetActive(false);
                            return;
                        }
                        
                        RaycastHit hit2 = (RaycastHit)h;
                        if (hit.collider.gameObject.name == "円柱")
                        {
                            hit2 = hit;
                        }
                        hitPosition = hit2.point;
                        Vector3 position = hitPosition;
                        position.y = targetEraser.GetComponent<EraserControlBase>().GetTopPosition();
                        hitMarker.transform.position = position;
                        hitEraser = hit2;
                        directionRotation.DataSet(hitEraser, pointerObject.transform.forward,1);
                        continue;
                    }

                }

            }

        }
        hitMarker.SetActive(isActive);


        
    }
    public RaycastHit? NormalRay(RaycastHit r)
    {
        Ray ray = new Ray(pointerObject.transform.position, -r.normal);
        Debug.DrawRay(pointerObject.transform.position, -r.normal * maxRayDistance, Color.yellow);
        foreach (RaycastHit hit in Physics.RaycastAll(ray, maxRayDistance))
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
            if (hitPlayerNumber == targetEraser.GetComponent<EraserControlBase>().playerNumber)
            {
                if (hitPosition == Vector3.zero || Vector3.Distance(pointerObject.transform.position, hit.point) < Vector3.Distance(pointerObject.transform.position, hitPosition))
                {
                    
                    return hit;
                }   

            }
        }
        return null;
    }
    public void Active(bool a,GameObject eraserObject,int deveiceNumber)
    {
        Log.text("Pointer");
        isActive = a && deveiceNumber == PlayerPrefs.GetInt("Dnumber");
        targetEraser = eraserObject;
        nextButton.SetActive(true);
    }
    public void Stop()
    {
        if (!pointerObject.activeSelf)
        {
            return;
        }
        isActive = false;
        nextButton.SetActive(false);
        FindAnyObjectByType<GameManager>().Power();
        Vector3 local = hitEraser.normal; //pointerObject.transform.forward;
        vector = local.normalized;
        vector *= -1;
    }
    public Vector3 GetData(float Power)
    {
        directionRotation.DataSet(hitEraser, pointerObject.transform.forward,Power);
        return directionRotation.GetDirection();
    }
    public void ComData(RaycastHit hit,Vector3 rayDirection)
    {
        hitEraser = hit;
        Debug.Log(rayDirection);
        pointerObject.transform.forward = rayDirection;
    }
    public Vector3 GetRotate(float Power)
    {
        directionRotation.DataSet(hitEraser, pointerObject.transform.forward, Power);
        return directionRotation.GetRotation();
    }
    public float GetPower(float Power)
    {
        directionRotation.DataSet(hitEraser, pointerObject.transform.forward,Power);
        return  directionRotation.Power(Power);
    }
    public Vector3 GetHitPosition()
    {
        return hitPosition;
    }
}
