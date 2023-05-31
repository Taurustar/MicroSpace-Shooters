using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCheck : MonoBehaviour
{

    public GameObject[] androidComponents;
    // Start is called before the first frame update
    void Start()
    {
#if !UNITY_ANDROID

        for(int i = 0; i < androidComponents.Length ; i++)
        {
            Destroy(androidComponents[i]);
        }

#endif


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
