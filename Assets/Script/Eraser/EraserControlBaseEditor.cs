using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(EraserControlBase),true)]//拡張するクラスを指定
public class EraserControlBaseEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("オブジェクトの位置を調整"))
        {
            EraserControlBase baseClass = (EraserControlBase)target;
            baseClass.PositionAdjustment();
            EditorUtility.SetDirty(baseClass);
            SceneView.RepaintAll();
        }
    }


}
