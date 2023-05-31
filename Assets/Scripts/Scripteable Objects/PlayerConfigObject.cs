using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Player", menuName = "ScriptableObjects/Create Player", order = 2)]
public class PlayerConfigObject : ScriptableObject
{
    [Range(10,100)]
    [Tooltip("Starting health value")]
    public int startingHealth = 100;
    [Range(1, 50)]
    [Tooltip("Sets the normal player's speed")]
    public float normalAcceleration = 1;
    [Range(2, 100)]
    [Tooltip("Indicates the speed variation when the player uses the accelerate button")]
    public float maxAcceleration = 3;


    
    
    public void ReturnDefaultValues()
    {
        startingHealth = 100;
        normalAcceleration = 1;
        maxAcceleration = 3;
    }
   
}
