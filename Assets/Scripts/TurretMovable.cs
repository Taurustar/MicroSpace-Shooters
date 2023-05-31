using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMovable : MonoBehaviour
{
    [Tooltip("Sets the configObject")]
    public TurretConfigObject configObject;
    [SerializeField]
    TurretConfigObject.Direction localDirection;
    /// <summary>
    /// Check the distance that the turret has moved. resets after it reaches the limit
    /// </summary>
    [SerializeField]
    float distanceMoved;
    float previous;

    // Start is called before the first frame update
    void Start()
    {
        distanceMoved = 0;
        localDirection = configObject.movementDirection;
        if (localDirection == TurretConfigObject.Direction.FRONT || localDirection == TurretConfigObject.Direction.REAR)
        { previous = transform.position.z; }
        else
        { previous = transform.position.x; }
        
    }

    // Update is called once per frame
    void Update()
    {
        //Movement functions for FRONT & REAR Directions
        if (localDirection == TurretConfigObject.Direction.FRONT || localDirection == TurretConfigObject.Direction.REAR)
        {
            if (localDirection == TurretConfigObject.Direction.FRONT)
            {

                transform.Translate(Vector3.forward / 100);
                distanceMoved += 0.01f;
                if (distanceMoved > configObject.movementLimit)
                {
                    localDirection = TurretConfigObject.Direction.REAR;
                    distanceMoved = 0;
                    
                }
            } else

            if (localDirection == TurretConfigObject.Direction.REAR)
            {

                transform.Translate((Vector3.forward / 100) * -1);
                distanceMoved += 0.01f;
                if (distanceMoved > configObject.movementLimit)
                {
                    localDirection = TurretConfigObject.Direction.FRONT;
                    distanceMoved = 0;
                }
            }

            previous = transform.position.z;
        }

        //Movement Functions for RIGHT & LEFT Directions
        if (localDirection == TurretConfigObject.Direction.RIGHT || localDirection == TurretConfigObject.Direction.LEFT)
        {
            if (localDirection == TurretConfigObject.Direction.RIGHT)
            {

                transform.Translate(Vector3.right / 100);
                distanceMoved += 0.01f;
                if (distanceMoved > configObject.movementLimit)
                {
                    localDirection = TurretConfigObject.Direction.LEFT;
                    distanceMoved = 0;
                }
            }else

            if (localDirection == TurretConfigObject.Direction.LEFT)
            {

                transform.Translate((Vector3.right / 100) * -1);
                distanceMoved += 0.01f;
                if (distanceMoved > configObject.movementLimit)
                {
                    localDirection = TurretConfigObject.Direction.RIGHT;
                    distanceMoved = 0;
                }
            }

            previous = transform.position.x;
        }
    }
}
