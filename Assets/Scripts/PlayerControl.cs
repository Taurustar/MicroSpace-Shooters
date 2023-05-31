using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class PlayerControl : MonoBehaviour
{

    public PlayerConfigObject configObject;
    public List<GameObject> playerLaserWeapons;
    public List<GameObject> playerLaserPoints;
    bool shootingLasers;
    int currentWeapon = 0;
    [SerializeField]
    int health;
    [Tooltip("Indicates the text UI object that shows the Health Ammount")]
    public Text healthText;
    public int score = 0;
    [Tooltip("Indicates the text UI object that shows the Player's Score")]
    public Text scoreText;
    float acceleration = 1.75f;
    public bool accelerate;
    bool colliding;
    [Tooltip("The Canvas when the player reaches the end of the level")]
    public Canvas winMenu;
    [Tooltip("The Canvas when the player loses all its health")]
    public Canvas loseMenu;
    public bool end;
    public bool destroyed;
    public ParticleSystem fire;
    public GameObject explosionParticle;
    public AudioSource fireWeaponSound, gameOverSound, endLevelSound;
    public InputActionAsset inputAsset;
    public InputAction forwardMovement, horizontalMovement, accelerating, restartAction, returnAction, fireLaser, nextWeapon, previousWeapon;
    public Rigidbody rb;


    public AudioSource music;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        forwardMovement = inputAsset.FindActionMap("Movement").FindAction("Forward");
        horizontalMovement = inputAsset.FindActionMap("Movement").FindAction("Horizontal");
        accelerating = inputAsset.FindActionMap("Movement").FindAction("Accelerate");
        restartAction = inputAsset.FindActionMap("Movement").FindAction("RestartLevel");
        returnAction = inputAsset.FindActionMap("Movement").FindAction("ReturnMenu");
        fireLaser = inputAsset.FindActionMap("Weapon").FindAction("Fire");
        nextWeapon = inputAsset.FindActionMap("Weapon").FindAction("ChangeNext");
        previousWeapon = inputAsset.FindActionMap("Weapon").FindAction("ChangePrev");


        configObject = PersistentPlayerConfiguration.Instance.playerConfigurations[PersistentPlayerConfiguration.Instance.armorIndex];
        playerLaserWeapons.Clear();
        playerLaserWeapons.AddRange(PersistentPlayerConfiguration.Instance.playerWeapons);
        score = PersistentPlayerConfiguration.Instance.playerCredits;

        //explosionParticle.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        forwardMovement.Enable();
        horizontalMovement.Enable();
        accelerating.Enable();
        restartAction.Enable();
        returnAction.Enable();
        fireLaser.Enable();
        nextWeapon.Enable();
        previousWeapon.Enable();
    }

    private void OnDisable()
    {
        forwardMovement.Disable();
        horizontalMovement.Disable();
        accelerating.Disable();
        restartAction.Disable();
        returnAction.Disable();
        fireLaser.Disable();
        nextWeapon.Disable();
        previousWeapon.Disable();
    }


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        colliding = false;
        health = configObject.startingHealth;
        acceleration = configObject.normalAcceleration;
        fireWeaponSound.clip = playerLaserWeapons[currentWeapon].GetComponent<Laser>().spawnSound;


    }

    private void OnTriggerEnter(Collider other)
    {
        if (!colliding)
        {
            colliding = true;
            if (other.GetComponent<Rotator>())
            {
                if (other.GetComponent<Rotator>().direction == 0 && !other.GetComponent<Rotator>().done)
                {
                    StartCoroutine(TurnRight());
                    other.GetComponent<Rotator>().done = true;
                }


                if (other.GetComponent<Rotator>().direction == 1 && !other.GetComponent<Rotator>().done)
                {
                    StartCoroutine(TurnLeft());
                    other.GetComponent<Rotator>().done = true;
                }
            }

            if (other.GetComponent<ActivateTurrets>())
            {
                foreach (ShootLaser turret in other.GetComponent<ActivateTurrets>().turrets)
                {
                    turret.enabled = true;
                }
            }

            if (other.GetComponent<DeactivateTurrets>())
            {
                for (int i = 0; i < other.GetComponent<DeactivateTurrets>().turrets.Length; i++)
                {
                    if (other.GetComponent<DeactivateTurrets>().turrets[i] != null)
                        Destroy(other.GetComponent<DeactivateTurrets>().turrets[i].gameObject);

                }
            }

            if (other.GetComponent<ActivateShips>())
            {
                foreach (EnemyShip ships in other.GetComponent<ActivateShips>().ships)
                {
                    ships.enabled = true;
                }
            }

            if (other.GetComponent<DeactivateShips>())
            {
                for (int i = 0; i < other.GetComponent<DeactivateShips>().ships.Length; i++)
                {
                    if (other.GetComponent<DeactivateShips>().ships[i] != null)
                        Destroy(other.GetComponent<DeactivateShips>().ships[i].gameObject);

                }
            }

            if (other.GetComponent<ShootLaser>())
            {
                other.GetComponent<ShootLaser>().health = 0;
                health -= 25;
                healthText.text = " Health: " + health.ToString();
            }

            if (other.GetComponent<EnemyShip>())
            {
                other.GetComponent<EnemyShip>().hp = 0;
                health -= 25;
                healthText.text = " Health: " + health.ToString();
            }


            if (other.GetComponent<ScoreObject>())
            {
                Destroy(other.gameObject);
                score += other.GetComponent<ScoreObject>().configObject.scoreAmmount;
                scoreText.text = " Credits: " + score.ToString();
            }

            if (other.GetComponent<HealthItem>())
            {
                Destroy(other.gameObject);
                health += other.GetComponent<HealthItem>().configParamters.healthAmmount;
                if(other.GetComponent<HealthItem>().configParamters.regen)
                {
                    StartCoroutine(Regeneration(other.GetComponent<HealthItem>().configParamters.healthPerSecond, other.GetComponent<HealthItem>().configParamters.seconds));
                }
                healthText.text = " Health: " + health.ToString();
            }

            if (other.tag == "End Level")
            {
                music.Stop();
                //Time.timeScale = 0;
                end = true;
                acceleration = 0;
                winMenu.enabled = true;
                endLevelSound.Play();
            }

            colliding = false;
        }


        

    }

    IEnumerator Regeneration(int amount, float seconds)
    {
        float secondsRegen = 0;
        do
        {
            health += amount;
            if(health > configObject.startingHealth) health = configObject.startingHealth;
            healthText.text = " Health: " + health.ToString();
            yield return new WaitForSeconds(1);
            secondsRegen++;
        } while (secondsRegen < seconds);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            health -= 100;
            healthText.text = " Health: " + health.ToString();
        }
    }

    public IEnumerator FinishCoroutine()
    {
        gameOverSound.Play();
        yield return new WaitForSeconds(2);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0, UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

    public IEnumerator FiringLasers()
    {
        if(!shootingLasers)
        {
            fireWeaponSound.clip = playerLaserWeapons[currentWeapon].GetComponent<Laser>().spawnSound;
            fireWeaponSound.Play();
            shootingLasers = true;
            switch(playerLaserWeapons[currentWeapon].GetComponent<Laser>().canyons)
            {
                case 1:
                    Instantiate(playerLaserWeapons[currentWeapon], playerLaserPoints.Last().transform);
                    break;
                case 2:
                    for(int i = 0; i < 2; i++)
                    {
                        
                        Instantiate(playerLaserWeapons[currentWeapon], playerLaserPoints[i].transform);
                    }
                    break;
                case 3:
                    foreach (GameObject point in playerLaserPoints)
                    {
                        Instantiate(playerLaserWeapons[currentWeapon], point.transform);
                    }
                    break;
            }
            yield return new WaitForSeconds(playerLaserWeapons[currentWeapon].GetComponent<Laser>().playerShootingDelay);
            shootingLasers = false;
        }
    }

    // Update is called once per frame
    void Update()
    {


        if (health <= 0 && !destroyed)
        {
            health = 0;
            Instantiate(explosionParticle, transform);
            music.Stop();
            colliding = true;
            destroyed = true;
            gameObject.GetComponent<Renderer>().enabled = false;
            gameOverSound.Play();
            //StartCoroutine(FinishCoroutine());
            loseMenu.enabled = true;
        }
        else
        {
            if (!end && !destroyed)
            {

                if(fireLaser.phase == InputActionPhase.Performed)
                {
                    StartCoroutine(FiringLasers());
                }

                if (nextWeapon.phase == InputActionPhase.Performed)
                {
                    currentWeapon++;
                    if (currentWeapon >= playerLaserWeapons.Count - 1) currentWeapon = 0;
                }

                if (previousWeapon.phase == InputActionPhase.Performed)
                {
                    currentWeapon--;
                    if (currentWeapon <= 0) currentWeapon = playerLaserWeapons.Count - 1;
                }

                float acc;
                if (accelerating.phase == InputActionPhase.Performed)
                {
                    acc = configObject.maxAcceleration;
                    fire.gameObject.SetActive(true);
                }
                else
                {
                    acc = configObject.normalAcceleration;
                    fire.gameObject.SetActive(false);
                }

                //if (forwardMovement.phase == InputActionPhase.Performed)
                //{

                    rb.MovePosition(rb.position + (transform.forward * acc * Time.deltaTime));

                //}

                if( horizontalMovement.phase == InputActionPhase.Performed)
                {
                    rb.MovePosition(rb.position + (transform.right * horizontalMovement.ReadValue<float>() * acc * Time.deltaTime));
                }

            }
        }

        if(end)
        {
            if (restartAction.phase == InputActionPhase.Performed)
            {
                FinishLevel();
            }
            if (returnAction.phase == InputActionPhase.Performed)
            {
                Menu();
            }
        }

        if(destroyed)
        {
            if (restartAction.phase == InputActionPhase.Performed)
            {
                StartLevel();
            }
        }
    }

    public void StartLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(PersistentPlayerConfiguration.Instance.levelNames[PersistentPlayerConfiguration.Instance.currentPlayerLevel], UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

    public void FinishLevel()
    {
        PersistentPlayerConfiguration.Instance.currentPlayerLevel++;

        if (PersistentPlayerConfiguration.Instance.currentPlayerLevel >= PersistentPlayerConfiguration.Instance.levelNames.Count)
        {
            PersistentPlayerConfiguration.Instance.currentPlayerLevel = 0;
        }

        PersistentPlayerConfiguration.Instance.playerCredits = score;
        UnityEngine.SceneManagement.SceneManager.LoadScene("IntermissionLevel", UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

    public void Menu()
    {
        PersistentPlayerConfiguration.Instance.currentPlayerLevel = 0;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu", UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

    public IEnumerator TurnRight()
    {

        for(int i = 0; i < 45; i ++)
        {
            transform.Rotate(new Vector3(0, 1, 0));
            yield return new WaitForSeconds(0.01f);
        } 
    }

    public IEnumerator TurnLeft()
    {
        for (int i = 0; i < 45; i++)
        {
            transform.Rotate(new Vector3(0, -1, 0));
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void MoveLeft()
    {
        transform.Translate(Vector3.right / -100);
    }

    public void MoveRight()
    {
        transform.Translate(Vector3.right / 100);
    }



    public int getHealth()
    {
        return health;
    }

    public void setHealth(int newHealth)
    {
        health = newHealth;
    }

}
