using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(TeleportPointList), true)]//拡張するクラスを指定
public class TeleportEraserEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("子オブジェクトを代入"))
        {
            TeleportPointList baseClass = (TeleportPointList)target;
            baseClass.PositionAdjustment();
            EditorUtility.SetDirty(baseClass);
            SceneView.RepaintAll();
        }
        if (GUILayout.Button("消す"))
        {
            TeleportPointList baseClass = (TeleportPointList)target;
            baseClass._parent = null;
            baseClass.PositionAdjustment();
            EditorUtility.SetDirty(baseClass);
            SceneView.RepaintAll();
        }
    }
}
