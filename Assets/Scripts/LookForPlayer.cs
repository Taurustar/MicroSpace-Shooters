using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookForPlayer : MonoBehaviour
{

    public GameObject canyon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPostition = new Vector3(FindObjectOfType<PlayerControl>().transform.position.x,
                                       canyon.transform.position.y,
                                       FindObjectOfType<PlayerControl>().transform.position.z);

        canyon.transform.LookAt(targetPostition);
        
    }
}
