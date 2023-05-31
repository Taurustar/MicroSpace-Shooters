using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TurretConfigObject))]
public class TurretConfigEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();


        TurretConfigObject playerConfig = (TurretConfigObject)target;
        if (GUILayout.Button("Reset Default Values"))
        {
            playerConfig.ReturnDefaultValues();
        }

    }
}
