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
        // 0`ŒÂ”-1‚Ü‚Å‚Ìq‚ğ‡”Ô‚É”z—ñ‚ÉŠi”[
        for (var i = 0; i < children.Length; ++i)
        {
            // Transform‚©‚çƒQ[ƒ€ƒIƒuƒWƒFƒNƒg‚ğæ“¾‚µ‚ÄŠi”[
            _pointList.Add(_parent.transform.GetChild(i).gameObject);
        }
    }
}
