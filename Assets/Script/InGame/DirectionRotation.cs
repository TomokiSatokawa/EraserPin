using UnityEngine;

public class DirectionRotation : MonoBehaviour
{
    public Vector3 outputDirection;
    public Vector3 outputRotation;
    public Vector3 inputDirection;
    public float distance;
    public enum HitSide
    {
        Left, Right, back, front
    }
    public HitSide hitSide;
    private float rotatePower;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void DataSet(RaycastHit hit,Vector3 rayDirection)
    {
        inputDirection = hit.normal; //pointerObject.transform.forward;
        inputDirection = inputDirection.normalized;
        inputDirection *= -1;

         rotatePower = 0f;
        if (hit.collider.gameObject.name == "‰~’Œ")
        {
            rotatePower = hit.collider.transform.InverseTransformPoint(hit.point).x;
            rotatePower = Mathf.Abs(rotatePower);
            rotatePower *= 10;
            hitSide = HitSide.front;
        }
        else
        {
            rotatePower = Vector3.Distance(GetCenter(hit), hit.point);
            rotatePower *= 10;
        }
        rotatePower = Normalize(rotatePower);

        outputDirection = inputDirection.normalized + rayDirection.normalized * (rotatePower/10);
        outputDirection = outputDirection.normalized;

        outputRotation = RotationDirection(hit) * rotatePower;
    }
    //AI
    public Vector3 GetCenter(RaycastHit hit)
    {
        Vector3 localDirection = hit.collider.gameObject.transform.InverseTransformDirection(inputDirection);
        EraserControlBase eraserControl = hit.collider.transform.parent.gameObject.GetComponent<EraserControlBase>();
        if (localDirection == new Vector3(0, 1, 0))
        {
            hitSide = HitSide.back;
            return eraserControl.backPosition.transform.position;
        }
        else if (localDirection == new Vector3(1, 0, 0))
        {
            hitSide = HitSide.Right;
            return eraserControl.rightPosition.transform.position;
        }
        else if (localDirection == new Vector3(-1, 0, 0))
        {
            hitSide = HitSide.Left;
            return eraserControl.leftPosition.transform.position;
        }
        else
        {
            return Vector3.zero;
        }
    }
    public float Normalize(float value)
    {
        switch (hitSide)
        {
            case HitSide.front:
                value *= 10;
                value /= 2;
                break;

            case HitSide.Left:
            case HitSide.Right:
                value -= 4;
                value *= value;
                value *= 5;
                break;

            case HitSide.back:
                value *= value;
                value /= 1.7f;
                break;
        }
        return value * 5;
    }
    public Vector3 RotationDirection(RaycastHit hit)
    {
        Vector3 localHitPoint = hit.collider.gameObject.transform.InverseTransformDirection(hit.point);
        bool isClock = false;
        switch (hitSide)
        {
            case HitSide.front:
                isClock = localHitPoint.x < 0;
                break;

            case HitSide.Left:
                isClock = localHitPoint.x > 2.3;
                break;
            case HitSide.Right:
               
                isClock = localHitPoint.x < 2.3;
                break;

            case HitSide.back:
                isClock = localHitPoint.x > 0;
                break;
            default:
                Debug.LogError("HitSide");
                break;
        }
        if(isClock)
        {
            return new Vector3(0, 1, 0);
        }
        else
        {
            return new Vector3(0, -1, 0);
        }
    }
    public Vector3 GetDirection()
    {
        return outputDirection;
    }
    public Vector3 GetRotation()
    {
        return outputRotation;
    }
    public float Power(float power)
    {
        return power - rotatePower;
    }
}

