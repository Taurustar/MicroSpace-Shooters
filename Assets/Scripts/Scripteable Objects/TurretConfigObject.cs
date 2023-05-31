using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Turret", menuName = "ScriptableObjects/Create Turret Config", order = 2)]
public class TurretConfigObject : ScriptableObject
{
    [Tooltip("Health points of the turret")]
    public int health;
    [Tooltip("Score points given to Player after he destroys the turret")]
    public int score;
    [Tooltip("GameObject or Prefab of the Laser Object to be spawned")]
    public GameObject laserObject;
    [Range(5 , 25)]
    [Tooltip("Turret's Laser Damage")]
    public int turretDamage = 5;
    [Range(0, 1)]
    [Tooltip("Number of seconds of Shoot delay after the turret is activated")]
    public float startDelay = 0;
    [Range(0.2f, 0.8f)]
    [Tooltip("Number of shots per second")]
    public float frecuency = 0.3f;
    [Range(1, 5)]
    [Tooltip("Turret's Laser Speed")]
    public float laserSpeed = 1;
    [Tooltip("Set's the Turret Laser Speed as Random ignoring the Turret Laser Speed value set")]
    public bool isLaserSpeedRandom;
    [Tooltip("Set's the Start Delay as Random ignoring the Start Delay value set")]
    public bool isRandomDelay;
    [Tooltip("Set's the Turret frencuency as Random ignoring the frecuency value set")]
    public bool isRandomFrecuency;
    [Tooltip("Set the turret as movable in the level (Adds a TurretMovable  Component to the GameObject with the direction parameter)")]
    public bool isMovable;
    [Tooltip("Set the limit of the movement for this turret type")]
    public float movementLimit = 3;
    public enum Direction
    {
        FRONT,
        REAR,
        LEFT,
        RIGHT

    };
    [Tooltip("APPLIES ONLY IF \"IS MOVABLE\" IS ON. Sets the turret movement direction")]
    public Direction movementDirection;


    public void ReturnDefaultValues()
    {
        laserObject = null;
        turretDamage = 5;
        startDelay = 0;
        frecuency = 0.3f;
        laserSpeed = 1;
        isLaserSpeedRandom = false;
        isRandomDelay = false;
        isRandomFrecuency = false;
        isMovable = false;
        movementDirection = Direction.FRONT;
        movementLimit = 3;
    }
}
