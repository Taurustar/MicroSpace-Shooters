using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLaser : MonoBehaviour
{
    public TurretConfigObject configObject;
    bool alive = true;
    public int health;
    public int scoreGiven;
    [Tooltip("Game Objects array where the lasers are spawned")]
    public GameObject[] laserPoints;
    [SerializeField]
    GameObject LaserObject;
    [SerializeField]
    int damage;
    [SerializeField]
    float speed = 1;
    [SerializeField]
    float frecuency = 1; //Shoots per second
    [SerializeField]
    float firstShotDelay;
    public AudioSource deathSound;
    public GameObject explosionParticle;
    public float explosionScale = 0.1f;
    // Start is called before the first frame update

    private void Awake()
    {
        health = configObject.health;
        scoreGiven = configObject.score;
        LaserObject = configObject.laserObject;
        damage = configObject.turretDamage;
        speed = configObject.isLaserSpeedRandom ? Random.Range(1, 5) : configObject.laserSpeed;
        frecuency = configObject.isRandomFrecuency ? Random.Range(0.2f, 0.8f) : configObject.frecuency;
        firstShotDelay = configObject.isRandomDelay ? Random.Range(0, 1) : configObject.startDelay;
        if (configObject.isMovable)
        {
            gameObject.AddComponent<TurretMovable>();
            gameObject.GetComponent<TurretMovable>().configObject = configObject;
        }
    }
    void Start()
    {
        
        StartCoroutine(StartShooting());
    }

    public IEnumerator StartShooting()
    {
        yield return new WaitForSeconds(firstShotDelay);
        StartCoroutine(Shooting());

    }

    private void Update()
    {
        if (health <= 0 && alive)
        {
            alive = false;
            GameObject explosion = Instantiate(explosionParticle, transform);
            explosion.transform.localScale *= explosionScale;
            explosion.transform.SetParent(null);
            deathSound.spatialBlend = 0;
            deathSound.volume = 0.5f;
            deathSound.Play();
            FindObjectOfType<PlayerControl>().score += scoreGiven;
            FindObjectOfType<PlayerControl>().scoreText.text = " Credits: " + FindObjectOfType<PlayerControl>().score.ToString();
            if(gameObject.GetComponent<Renderer>())
            {
                gameObject.GetComponent<Renderer>().enabled = false;
            }

                DisableRendererRecursively(transform);
            
            foreach(Collider col in gameObject.GetComponents<Collider>())
            {
                Destroy(col);
            }            
            Destroy(gameObject.GetComponent<TurretMovable>());
            
        }
    }


    private void DisableRendererRecursively(Transform parent)
    {
        // Disable Renderer component if exists
        Renderer renderer = parent.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = false;
        }

        // Disable Renderer component in all children and subchildren
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            DisableRendererRecursively(child);
        }
    }


    public IEnumerator Shooting()
    {
        if(alive)
        {
            gameObject.GetComponent<AudioSource>().Play();
            foreach (GameObject point in laserPoints)
            {
                GameObject newLaser = Instantiate(LaserObject, point.transform);
                newLaser.gameObject.transform.parent = null;
                newLaser.gameObject.transform.localScale = new Vector3(0.012f, 0.002f, 0.012f);
                newLaser.GetComponent<Laser>().damage = damage;
                newLaser.GetComponent<Laser>().laserSpeed = speed;
            }
            yield return new WaitForSeconds(1 / frecuency);
            StartCoroutine(Shooting());
        }

    }

    
}
