using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ScoreConfigObject))]
public class ScoreConfigEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();


        ScoreConfigObject playerConfig = (ScoreConfigObject)target;
        if (GUILayout.Button("Reset Default Values"))
        {
            playerConfig.ResetDefaultValues();
        }

    }
}
