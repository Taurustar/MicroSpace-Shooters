using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AccelerationButtonScript : MonoBehaviour
{

    public PlayerControl control;


    public void Accelerate()
    {
        control.accelerate = true;
    }

    public void Deaccelerate()
    {
        control.accelerate = false;   
    }
}
