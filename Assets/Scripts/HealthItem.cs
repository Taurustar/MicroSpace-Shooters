using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour
{
    
    public HealthItemConfig configParamters;
    public AnimationCurve blobCurve;
    public float duration = 1f;
    public float speed = 1f;
    private Vector3 initialPosition;
    private float timer = 0f;

    public bool setTargetPosY;
    public Transform target;

    private void Start()
    {
        initialPosition = transform.localPosition;
    }

    private void Update()
    {
        timer += Time.deltaTime * speed;
        float t = timer / duration;

        if (t >= 1f)
        {
            // Reset the timer and scale to initial state
            timer = 0f;
            transform.localPosition = initialPosition;
            return;
        }

        // Apply the scale animation based on the animation curve
        float scale = blobCurve.Evaluate(t);
        if (!setTargetPosY)
        {
            transform.localPosition = new Vector3(initialPosition.x, initialPosition.y * scale, initialPosition.z);
        }
        else
        {
            if(target != null)
            {
                transform.localPosition = new Vector3(initialPosition.x, target.localPosition.y - 0.7f, initialPosition.z);
            }
        }
    }

}
