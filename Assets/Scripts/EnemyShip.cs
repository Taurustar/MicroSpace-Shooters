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
    public float panFactor;
    Vector3 panPoint;
    public float firstPanDelay;
    public EnemyShipConfig config;
    public List<GameObject> particlePoints;
    public float particleScale = 0.1f;
    public AudioClip bossMusic;

    private void Awake()
    {
        hp = config.health;
        alive = true;
        scoreGiven = config.score;
        speed = config.isRandomSpeed ? Random.Range(1, config.speed) : config.speed;
        laserSpeed = config.isLaserSpeedRandom ? Random.Range(1, 5) : config.laserSpeed;
        frecuency = config.isRandomFrecuency ? Random.Range(0.2f, 0.8f) : config.frecuency;
        firstShotDelay = config.isRandomDelay ? Random.Range(0, 1) : config.startDelay;
        panFactor = config.maxPanValue;
        firstPanDelay = config.panDelayRandom ? Random.Range(0, config.panDelay) : config.panDelay;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(StartShooting());
        StartCoroutine(StartPan());
        if(config.boss)
        {
            FindObjectOfType<PlayerControl>().music.Stop();
            FindObjectOfType<PlayerControl>().music.clip = bossMusic;
            FindObjectOfType<PlayerControl>().music.Play();
        }
    }

    public IEnumerator StartShooting()
    {
        yield return new WaitForSeconds(firstShotDelay);
        if(laserPoints != null && config.laserObject != null)
        {
            StartCoroutine(Shooting());
        }
        

    }

    public IEnumerator StartPan()
    {
        yield return new WaitForSeconds(firstPanDelay);
        if(config.movementDirection == EnemyShipConfig.Direction.FOLLOWS_PAN || config.movementDirection == EnemyShipConfig.Direction.PAN)
            StartCoroutine(PanCoroutine());
        


    }


    IEnumerator ExplosionSequence()
    {
        foreach (GameObject particle in particlePoints)
        {
            GameObject expl2 = Instantiate(explosionParticle, particle.transform);
            deathSound.Play();
            yield return new WaitForSeconds(0.33f);
        }
    }

    Rigidbody rb;
    private void Update()
    {
        if (hp <= 0 && alive)
        {
            alive = false;
            GameObject explosion = Instantiate(explosionParticle, transform);
            explosion.transform.localScale *= particleScale;
            explosion.transform.SetParent(null);
            deathSound.spatialBlend = 0;
            deathSound.volume = 0.5f;
            deathSound.Play();
            FindObjectOfType<PlayerControl>().score += scoreGiven;
            FindObjectOfType<PlayerControl>().scoreText.text = " Credits: " + FindObjectOfType<PlayerControl>().score.ToString();

            if(config.boss)
            {
                StartCoroutine(ExplosionSequence());
                FindObjectOfType<PlayerControl>().StartCoroutine(FindObjectOfType<PlayerControl>().PlayEndOfLevel());
            }
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
            if((config.movementDirection == EnemyShipConfig.Direction.FOLLOWS || config.movementDirection == EnemyShipConfig.Direction.FOLLOWS_PAN) && followZone)
            {

                Vector3 projectedOffset = config.movementDirection == EnemyShipConfig.Direction.FOLLOWS_PAN ? panPoint : transform.forward;
                if (followZone.GetComponent<ShipFollowZone>().detectedObject != null)
                {
                    projectedOffset = transform.forward + followVector;
                }
                rb.MovePosition(rb.position + (projectedOffset * speed * Time.deltaTime));
            }
            else
            {
                if(config.movementDirection == EnemyShipConfig.Direction.PAN)
                    rb.MovePosition(rb.position + (panPoint * speed * Time.deltaTime));
                else
                    rb.MovePosition(rb.position + (transform.forward * speed * Time.deltaTime));
            }
            
        }
    }

    IEnumerator PanCoroutine()
    {
        panPoint = transform.forward + (transform.position - (transform.position + (Vector3.right * panFactor)));
        yield return new WaitForSeconds(2);
        panFactor *= -1;
        StartCoroutine(PanCoroutine());
        
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
