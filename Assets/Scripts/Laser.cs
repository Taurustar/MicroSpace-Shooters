using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    public int damage;
    public bool playerLaser;
    public float playerShootingDelay = 0.5f;
    public bool damageDealed;
    public float laserSpeed = 1;
    public float lifeTime = 10;
    public AudioClip spawnSound;
    [Tooltip("If the laser is a player Laser, Indicates the canyon numbers it will use")]
    public int canyons;
    [Tooltip("If the laser is a player Laser, Indicates the amount of credits to buy this laser")]
    public int weaponCost;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        transform.Translate((Vector3.up / 100) * laserSpeed);

        lifeTime -=Time.deltaTime;

        if(lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    bool CheckForValidCollission(Collider other)
    {
        //Add as many conditions necessary
        return other.GetComponent<ScoreObject>() || other.GetComponent<ShipFollowZone>() || other.GetComponent<Rotator>() || other.GetComponent<ActivateShips>() || other.GetComponent<ActivateTurrets>() || other.GetComponent<DeactivateShips>() || other.GetComponent<DeactivateTurrets>() || other.GetComponent<PlayerControl>() || other.GetComponent<ShootLaser>() || other.GetComponent<EnemyShip>() || other.tag == "Obstacle";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (CheckForValidCollission(other))
        {            
            if(!(playerLaser && other.GetComponent<PlayerControl>()))
            {
                if (!damageDealed)
                {
                    
                    if (other.GetComponent<PlayerControl>() && !playerLaser)
                    {
                        damageDealed = true;
                        other.GetComponent<PlayerControl>().setHealth(other.GetComponent<PlayerControl>().getHealth() - damage);
                        other.GetComponent<PlayerControl>().healthText.text = " Health: " + other.GetComponent<PlayerControl>().getHealth().ToString();
                        Destroy(gameObject);
                    }
                    else if (other.GetComponent<ShootLaser>() && playerLaser)
                    {
                        damageDealed = true;
                        other.GetComponent<ShootLaser>().health -= damage;
                        Destroy(gameObject);
                    }
                    else if(other.GetComponent<EnemyShip>() && playerLaser)
                    {
                        damageDealed = true;
                        other.GetComponent<EnemyShip>().hp -= damage;
                        Destroy(gameObject);
                    }
                    
                }
            }
            
        }
        else
        {
            if (playerLaser) Debug.Log(other.gameObject.name);
            Destroy(gameObject);
        }
        
    }
}
