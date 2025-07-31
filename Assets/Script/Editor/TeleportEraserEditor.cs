using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(TeleportPointList), true)]//�g������N���X���w��
public class TeleportEraserEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("�q�I�u�W�F�N�g����"))
        {
            TeleportPointList baseClass = (TeleportPointList)target;
            baseClass.PositionAdjustment();
            EditorUtility.SetDirty(baseClass);
            SceneView.RepaintAll();
        }
        if (GUILayout.Button("����"))
        {
            TeleportPointList baseClass = (TeleportPointList)target;
            baseClass._parent = null;
            baseClass.PositionAdjustment();
            EditorUtility.SetDirty(baseClass);
            SceneView.RepaintAll();
        }
    }
}
