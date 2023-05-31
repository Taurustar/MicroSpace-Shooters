using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipFollowZone : MonoBehaviour
{
    public Transform detectedObject; // Reference to the detected object
    private Vector3 initialPosition; // Initial position of the parent GameObject


    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerControl>())
        {
            detectedObject = other.transform;
            initialPosition = transform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerControl>())
        {
            detectedObject = null;
        }
    }


    private void Update()
    {
        // Apply the translation modification based on the detected object position
        if (detectedObject != null)
        {
            // Calculate the translation offset based on the detected object's position relative to the initial position
            Vector3 offset = detectedObject.position - initialPosition;

            offset = new Vector3(offset.x, 0, offset.z);
            gameObject.GetComponentInParent<EnemyShip>().followVector = offset;
        }
    }
}
