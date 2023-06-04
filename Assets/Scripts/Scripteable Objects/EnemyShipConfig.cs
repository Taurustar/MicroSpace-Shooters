using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyShip", menuName = "ScriptableObjects/Create EnemyShip Config", order = 5)]
public class EnemyShipConfig : ScriptableObject
{
    [Tooltip("Health points of the EnemyShip")]
    public int health;
    [Tooltip("Score points given to Player after he destroys the Ship")]
    public int score;
    [Tooltip("Ship speed")]
    public float speed;
    [Tooltip("GameObject or Prefab of the Laser Object to be spawned")]
    public GameObject laserObject;
    [Range(5, 25)]
    [Tooltip("Ship's Laser Damage")]
    public int laserDamage = 5;
    [Range(0, 1)]
    [Tooltip("Number of seconds of Shoot delay after the Ship is activated")]
    public float startDelay = 0;
    [Range(0.2f, 0.8f)]
    [Tooltip("Number of shots per second")]
    public float frecuency = 0.3f;
    [Range(1, 5)]
    [Tooltip("Ship's Laser Speed")]
    public float laserSpeed = 1;
    [Tooltip("Set's the Ship Laser Speed as Random ignoring the Ship Laser Speed value set")]
    public bool isLaserSpeedRandom;
    [Tooltip("Set's the Start Delay as Random ignoring the Start Delay value set")]
    public bool isRandomDelay;
    [Tooltip("Set's the Ship frencuency as Random ignoring the frecuency value set")]
    public bool isRandomFrecuency;
    [Tooltip("Indicates the max lenght for Pan movement")]
    public float maxPanValue;
    [Range(0, 5)]
    [Tooltip("Number of seconds before the first pan the Ship is activated")]
    public float panDelay;
    [Tooltip("Set's if the pan delay is random")]
    public bool panDelayRandom;


    public enum Direction
    {
        FRONT,
        FOLLOWS,
        PAN,
        FOLLOWS_PAN
    };
    [Tooltip("APPLIES ONLY IF \"IS MOVABLE\" IS ON. Sets the turret movement direction")]
    public Direction movementDirection;


    public void ReturnDefaultValues()
    {
        laserObject = null;
        laserDamage = 5;
        startDelay = 0;
        frecuency = 0.3f;
        laserSpeed = 1;
        isLaserSpeedRandom = false;
        isRandomDelay = false;
        isRandomFrecuency = false;
        movementDirection = Direction.FRONT;
    }
}
