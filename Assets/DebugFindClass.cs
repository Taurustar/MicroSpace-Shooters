using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugFindClass : MonoBehaviour
{
    public string className;
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerControl>())
        {
            Type classType = Type.GetType(className);
            List<object> objects = new List<object>(FindObjectsOfType(classType));

            foreach(object obj in objects)
            {
                GameObject go = obj as GameObject;
                Debug.Log(go.name);
            }

            
        }
    }
}
