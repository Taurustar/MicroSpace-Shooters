using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ErgosTrigger : MonoBehaviour
{
    public CinemachineFlux cinema;

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("erdos"))
        {
            Debug.Log("Activando Trigger");
            //cinema.SequenceActivate();
            cinema.camera.GetComponent<PlayableDirector>().Play();
        }
    }
}
