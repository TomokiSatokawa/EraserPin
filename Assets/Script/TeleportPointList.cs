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
        // 0�`��-1�܂ł̎q�����Ԃɔz��Ɋi�[
        for (var i = 0; i < children.Length; ++i)
        {
            // Transform����Q�[���I�u�W�F�N�g���擾���Ċi�[
            _pointList.Add(_parent.transform.GetChild(i).gameObject);
        }
    }
}
