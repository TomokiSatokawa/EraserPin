using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(EraserControlBase),true)]//�g������N���X���w��
public class EraserControlBaseEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("�I�u�W�F�N�g�̈ʒu�𒲐�"))
        {
            EraserControlBase baseClass = (EraserControlBase)target;
            baseClass.PositionAdjustment();
            EditorUtility.SetDirty(baseClass);
            SceneView.RepaintAll();
        }
    }


}
