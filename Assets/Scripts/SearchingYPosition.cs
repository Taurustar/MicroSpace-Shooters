using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchingYPosition : MonoBehaviour
{

    public GameObject target;
    public Transform setPosition;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player");
        setPosition = this.gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(setPosition != null)
        {
            setPosition.position = new Vector3(transform.position.x, target.transform.position.y, transform.position.z);
        }
    }
}
