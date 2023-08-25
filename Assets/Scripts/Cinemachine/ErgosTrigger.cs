using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErgosTrigger : MonoBehaviour
{
    public CinemachineFlux cinema;

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("erdos"))
        {
            cinema.SequenceActivate();
            Debug.Log("Trigger Disparado");
            Destroy(gameObject);
        }
    }
}
