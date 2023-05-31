using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreObject : MonoBehaviour
{
    public ScoreConfigObject configObject;

    private void Start()
    {
        GetComponentInChildren<Light>().color = configObject.lightColor;
        GetComponent<MeshRenderer>().material = configObject.material;
    }

    private void Update()
    {
        transform.Rotate(new Vector3(Random.value, Random.value, Random.value));
    }
}
