using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyShipConfig))]
public class EnemyShipEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();


        EnemyShipConfig playerConfig = (EnemyShipConfig)target;
        if (GUILayout.Button("Reset Default Values"))
        {
            playerConfig.ReturnDefaultValues();
        }

    }
}
