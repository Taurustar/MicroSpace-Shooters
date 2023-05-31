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

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerControl>() || other.GetComponent<ShootLaser>() || other.GetComponent<EnemyShip>() || other.tag == "Obstacle")
        {
            if(!(playerLaser && other.GetComponent<PlayerControl>()))
            {
                if (!damageDealed)
                {
                    damageDealed = true;
                    if (other.GetComponent<PlayerControl>() && !playerLaser)
                    {
                        other.GetComponent<PlayerControl>().setHealth(other.GetComponent<PlayerControl>().getHealth() - damage);
                        other.GetComponent<PlayerControl>().healthText.text = " Health: " + other.GetComponent<PlayerControl>().getHealth().ToString();
                    }
                    else if (other.GetComponent<ShootLaser>() && playerLaser)
                    {
                        other.GetComponent<ShootLaser>().health -= damage;
                    }
                    else if(other.GetComponent<EnemyShip>() && playerLaser)
                    {
                        other.GetComponent<EnemyShip>().hp -= damage;
                    }
                    Destroy(gameObject);
                }
            }
            
        }
        
    }
}
