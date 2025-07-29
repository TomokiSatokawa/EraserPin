using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class EraserControlBase : MonoBehaviourPunCallbacks
{
    public int playerNumber;
    public GameObject topPosition;
    public GameObject rightPosition;
    public GameObject leftPosition;
    public GameObject backPosition;
    public List<MeshCollider> colliders = new List<MeshCollider>();
    public GameObject coverObject;
    public GameObject handPosition;
    private Rigidbody rb;
    private float maxSpeed = 10f;
    // Start is called before the first frame update
    public void Start()
    {
        DataReset();
        rb = GetComponent<Rigidbody>();
    }
    public virtual void DataReset() { }
    public int GetPlayerNumber()
    {
        return playerNumber;
    }
    public float GetTopPosition()
    {
        return topPosition.transform.position.y;
    }
    public virtual void StopProcess() { }
    public void ChangeColor(Color color)
    {
        coverObject.GetComponent<MeshRenderer>().material.color = color;
    }
    public virtual void MyTurn() { }
    public void PositionAdjustment()
    {
        //NullCheck
        if(colliders == null || colliders.Count == 0)
        {
            var children = new GameObject[transform.childCount];

            // 0Å`å¬êî-1Ç‹Ç≈ÇÃéqÇèáî‘Ç…îzóÒÇ…äiî[
            for (var i = 0; i < children.Length; ++i)
            {
                // TransformÇ©ÇÁÉQÅ[ÉÄÉIÉuÉWÉFÉNÉgÇéÊìæÇµÇƒäiî[
                children[i] = transform.GetChild(i).gameObject;
            }
            colliders = new List<MeshCollider> ();
            foreach(GameObject obj in children)
            {
                if(obj.GetComponent<MeshCollider>() == null)
                {
                   obj.AddComponent<MeshCollider>().convex = true;

                }
                if(obj == coverObject)
                {
                    continue;
                }
                colliders.Add(obj.GetComponent<MeshCollider>());
            }
        }

        if (topPosition == null || backPosition == null || rightPosition == null || leftPosition == null)
        {
            if (topPosition == null)
                topPosition = new GameObject("top");

            if (rightPosition == null)
                rightPosition = new GameObject("right");

            if(leftPosition == null)
                leftPosition = new GameObject("left");

            if (backPosition == null)
                backPosition = new GameObject("back");

            if (handPosition == null)
                handPosition = new GameObject("HandPosition");

            topPosition.transform.parent = this.transform;
            rightPosition.transform.parent = this.transform;
            leftPosition.transform.parent = this.transform;
            backPosition.transform.parent = this.transform;
            handPosition.transform.parent = this.transform;
            
        }
        if(coverObject == null)
        {
            Debug.LogError("cover Null");
            return;
        }
        //Top

        Vector3 position = Vector3.zero;
        position.x = coverObject.transform.position.x;
        position.z = coverObject.transform.position.z;
        Collider collider = coverObject.GetComponent<Collider>();
        position.y = collider.bounds.max.y;
        topPosition.transform.position = position;

        //left
        position.z = coverObject.transform.position.z;
        position.y = coverObject.transform.position.y;
        position.x = collider.bounds.max.x;
        leftPosition.transform.position = position;


        //right
        position.x = collider.bounds.min.x;
        rightPosition.transform.position = position;

        //back
        Vector3 direction = -coverObject.transform.up; // ãtï˚å¸Ç…ê›íË
        Vector3 origin = coverObject.transform.position;

        float maxProjection = float.MinValue;
        Vector3 bestPoint = Vector3.zero;

        foreach (Collider col in colliders)
        {
            Vector3 probePoint = origin + direction * 100f; // è\ï™âìÇ¢ì_
            Vector3 closest = col.ClosestPoint(probePoint);

            float projection = Vector3.Dot(closest - origin, direction);
            if (projection > maxProjection)
            {
                maxProjection = projection;
                bestPoint = closest;
            }
        }
        position.x = coverObject.transform.position.x;
        position.y = coverObject.transform.position.y;
        position.z = bestPoint.z;
        backPosition.transform.position = position;
        



        //hand
        position.x = coverObject.transform.position.x;
        position.y = coverObject.transform.position.y + 0.3640001f;
        position.z = coverObject.transform.position.z + 0.6414061f;
        handPosition.transform.position = position;
        handPosition.transform.eulerAngles = new Vector3(0, 180, 0);

        //tag
        foreach(Collider col in colliders)
        {
            col.gameObject.tag = "EraserMesh";
        }
        this.gameObject.tag = "Eraser";
    }

    void FixedUpdate()
    {
        if(rb == null)
        {
            return;
        }
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
}
