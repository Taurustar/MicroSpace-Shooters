using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Health Item Object", menuName = "ScriptableObjects/HealthItem", order = 4)]
public class HealthItemConfig : ScriptableObject
{
    public int healthAmmount;
    public bool regen;
    [Header("Regeneration Options")]
    public int healthPerSecond;
    public float seconds;
   
}
