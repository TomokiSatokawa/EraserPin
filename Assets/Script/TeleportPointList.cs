using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPointList : MonoBehaviour
{
    [SerializeField]
    public GameObject _parent;
    [SerializeField]
    private List<GameObject> _pointList;
    [SerializeField]
    public List<GameObject> pointList
    {
        get {  return _pointList; }
    }
    public void PositionAdjustment()
    {
        //_pointList = new List<GameObject>();
        _pointList.Clear();
        var children = new GameObject[_parent.transform.childCount];
        // 0`Β-1άΕΜqπΤΙzρΙi[
        for (var i = 0; i < children.Length; ++i)
        {
            // Transform©ηQ[IuWFNgπζΎ΅Δi[
            _pointList.Add(_parent.transform.GetChild(i).gameObject);
        }
    }
}
