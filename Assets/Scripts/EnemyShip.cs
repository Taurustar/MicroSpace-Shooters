using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyShip : MonoBehaviour
{
    public int hp;
    public int scoreGiven;
    public bool alive;
    public float speed, firstShotDelay;
    public float laserSpeed, frecuency;
    public List<GameObject> shipBody;
    public List<GameObject> laserPoints;
    public GameObject explosionParticle;
    public AudioSource deathSound;
    public GameObject followZone;
    public Vector3 followVector;
    public EnemyShipConfig config;

    private void Awake()
    {
        hp = config.health;
        alive = true;
        scoreGiven = config.score;
        speed = config.speed;
        laserSpeed = config.isLaserSpeedRandom ? Random.Range(1, 5) : config.laserSpeed;
        frecuency = config.isRandomFrecuency ? Random.Range(0.2f, 0.8f) : config.frecuency;
        firstShotDelay = config.isRandomDelay ? Random.Range(0, 1) : config.startDelay;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(StartShooting());
    }

    public IEnumerator StartShooting()
    {
        yield return new WaitForSeconds(firstShotDelay);
        StartCoroutine(Shooting());

    }


    Rigidbody rb;
    private void Update()
    {
        if (hp <= 0 && alive)
        {
            alive = false;
            GameObject explosion = Instantiate(explosionParticle, transform);
            explosion.transform.localScale *= 0.1f;
            deathSound.Play();
            FindObjectOfType<PlayerControl>().score += scoreGiven;
            FindObjectOfType<PlayerControl>().scoreText.text = " Credits: " + FindObjectOfType<PlayerControl>().score.ToString();
            foreach (Collider col in gameObject.GetComponents<Collider>())
            {
                Destroy(col);
            }
            foreach(GameObject bodyPart in shipBody)
            {
                Destroy(bodyPart);
            }
        }
        else if (alive)
        {
            if(config.movementDirection == EnemyShipConfig.Direction.FOLLOWS && followZone)
            {
                Vector3 projectedOffset = transform.forward;
                if (followZone.GetComponent<ShipFollowZone>().detectedObject != null)
                {
                    projectedOffset = transform.forward + followVector;
                }
                rb.MovePosition(rb.position + (projectedOffset * speed * Time.deltaTime));
            }
            else
            {
                rb.MovePosition(rb.position + (transform.forward * speed * Time.deltaTime));
            }
            
        }
    }

    public IEnumerator Shooting()
    {
        if (alive)
        {
                       
            foreach (GameObject point in laserPoints)
            {
                GameObject newLaser = Instantiate(config.laserObject, point.transform);
                newLaser.gameObject.transform.parent = null;
                newLaser.gameObject.transform.localScale = new Vector3(0.012f, 0.002f, 0.012f);
                newLaser.GetComponent<Laser>().damage = config.laserDamage;
                newLaser.GetComponent<Laser>().laserSpeed = config.laserSpeed;
                point.GetComponent<AudioSource>().clip = config.laserObject.GetComponent<Laser>().spawnSound;
                point.GetComponent<AudioSource>().Play();
            }
            yield return new WaitForSeconds(1 / frecuency);
            StartCoroutine(Shooting());
        }

    }


}
