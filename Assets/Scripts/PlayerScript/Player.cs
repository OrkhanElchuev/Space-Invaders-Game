using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Configuration parameters
    [SerializeField] GameObject playerLaserObject;
    private float laserSpeed = 20.0f;
    private int numberOfLasers = 1;
    private Coroutine shootingCoroutine;
    private float laserShootingPeriod = 0.3f;

    // Headers for readability in unity
    [Header("Particle Effect")]
    [SerializeField] GameObject deathVFXObject;
    [SerializeField] GameObject sceneLoaderObject;
    private float durationOfExplosion = 0.4f;

    // Player configuration variables
    private int movingSpeedOfPlayer = 18;
    private int playerHealthPoints = 4;
    private int maxHealthPoints = 5;
    private Transform HealthBar;
    [SerializeField] GameObject PlayerInfo;
    private GameStatus gameStatus;
    private bool canShoot = true;

    // Camera Configuration Variables
    private float xMin;
    private float xMax;
    private float yMin;
    private float yMax;

    private void Awake()
    {
        gameStatus = transform.parent.Find("GameStatus").GetComponent<GameStatus>();
        HealthBar = transform.parent.Find("GameCanvas").Find("HealthBar");
    }

    // Start is called before the first frame update
    void Start()
    {
        ShowHealthBarIcons(playerHealthPoints);
        SetMovementLimitsForPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        Shoot();
    }

    // Show correct amount of health icons based on healthpoints
    private void ShowHealthBarIcons(int healthPoints)
    {
        for (int i = 0; i < healthPoints; i++)
        {
            HealthBar.GetChild(i).GetComponent<Image>().enabled = true;
        }
        for (int i = healthPoints; i < maxHealthPoints; i++)
        {
            HealthBar.GetChild(i).GetComponent<Image>().enabled = false;
        }
    }

    // Get player health (will be used for displaying)
    public int GetHealth()
    {
        return playerHealthPoints;
    }

    private void Shoot()
    {
        // When space keyboard is pressed start shooting
        if (Input.GetButtonDown("Attack") && canShoot == true)
        {   
            canShoot = false;
            // After a delay(shooting Period) enable shooting
            Invoke("EnableShooting", laserShootingPeriod);
            shootingCoroutine = StartCoroutine(ShootContinuously());
        }
        // If keyboard is released stop shooting
        if (Input.GetButtonUp("Attack"))
        {
            StopCoroutine(shootingCoroutine);
        }
    }

    private void DisablePlayerComponents()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Player>().enabled = false;
        GetComponent<PolygonCollider2D>().enabled = false;
    }

    // Increase Score from "ScorePowerUp"
    private void IncreaseScore()
    {
        int currentScore = gameStatus.GetScore();
        // Get a value from 500 up to 1000(only hundreds)
        int addScore = Random.Range(5, 11) * 100;
        // Increase the weigh of Power Up according to Current Score Value
        if (currentScore < 5000)
        {
            gameStatus.AddToScore(addScore);
        }
        else if (currentScore >= 5000 && currentScore < 10000)
        {
            gameStatus.AddToScore(addScore * 2);
        }
        else if (currentScore >= 10000 && currentScore < 25000)
        {
            gameStatus.AddToScore(addScore * 3);
        }
        else if (currentScore >= 25000 && currentScore < 50000)
        {
            gameStatus.AddToScore(addScore * 5);
        }
        else
        {
            gameStatus.AddToScore(addScore * 10);
        }
    }

    // Increase Attack Speed from "AttackSpeedPowerUp"
    private void IncreaseAttackSpeed()
    {
        // Limit max attack speed
        float maxAttackSShootingPeriod = 0.15f;
        if (laserShootingPeriod > maxAttackSShootingPeriod)
        {
            laserShootingPeriod /= 1.2f;
        }
    }

    // Increase number of Lasers from "WeaponPowerUp"
    private void IncreaseNumberOfLasers()
    {
        if (numberOfLasers < 5)
        {
            numberOfLasers++;
        }
    }

    // Increase health points from "HealthPowerUp"
    private void IncreasePlayerHealth()
    {
        if (playerHealthPoints < 5)
        {
            playerHealthPoints++;
            ShowHealthBarIcons(playerHealthPoints);
        }
    }

    // Executing relevant upgrade according to powerUp
    private void UpgradePlayer(string powerUpType)
    {
        switch (powerUpType)
        {
            case "HealthPowerUp":
                IncreasePlayerHealth();
                break;
            case "AttackSpeedPowerUp":
                IncreaseAttackSpeed();
                break;
            case "ScorePowerUp":
                IncreaseScore();
                break;
            case "WeaponPowerUp":
                IncreaseNumberOfLasers();
                break;
        }
    }

    // When destroyed degrade player current upgrades by one 
    private void DegradePlayer()
    {
        if (numberOfLasers > 1)
        {
            numberOfLasers--;
        }
        if (laserShootingPeriod < 0.3f)
        {
            laserShootingPeriod *= 1.2f;
        }
    }

    // On collision of enemy or enemy bullet with player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If collision object is PowerUP do not destroy Player
        if (collision.gameObject.tag == "PowerUp")
        {
            UpgradePlayer(collision.gameObject.GetComponent<PowerUps>().GetPowerUpType());
            // Destroy PowerUp Object
            Destroy(collision.gameObject);
        }
        else
        {
            DegradePlayer();
            playerHealthPoints--;
            DisablePlayerComponents();
            // If shooting is pressed before the player is dead
            if (shootingCoroutine != null)
            {
                // Stop shooting coroutine until the next shooting input from user
                StopCoroutine(shootingCoroutine);
            }
            // Delay for Respawn and ColliderEnabling methods
            Invoke("Respawn", 0.6f);
            Invoke("ColliderEnabling", 1.0f);
            RunExplosionEffect();
            ShowHealthBarIcons(playerHealthPoints);
            Destroy(collision.gameObject);
            if (playerHealthPoints <= 0)
            {
                PlayerInfo.GetComponent<PlayerInfo>().SetScore(gameStatus.GetScore());
                PlayerInfo.GetComponent<PlayerInfo>().SavePlayer();
                DestroyPlayer();
                sceneLoaderObject.GetComponent<SceneLoader>().LoadGameOver();
            }
        }
    }

    private void ColliderEnabling()
    {
        // Enable collider polygon on Player
        GetComponent<PolygonCollider2D>().enabled = true;
    }

    // Reset values after respawning		
    private void Respawn()
    {
        // Enable Sprite and Player 
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Player>().enabled = true;
        // Respawn player in the middle of the screen
        transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0, 100));
    }


    // Execute Explosion Effect
    private void RunExplosionEffect()
    {
        GameObject explosion = Instantiate(deathVFXObject, transform.position,
        transform.rotation);
        Destroy(explosion, durationOfExplosion);
    }

    // Instantiating laser object with flexible position and direction
    private void InstantiateLaser(string direction, float position)
    {
        // Create laser object when player shoots
        // + new Vector3(position,1,0) for shifting laser projectile up
        GameObject laser = Instantiate(playerLaserObject,
                 transform.position + new Vector3(position, 1, 0),
                    Quaternion.identity) as GameObject;
        // Setting velocity for laser
        laser.GetComponent<PlayerLaser>().CreateItself(laserSpeed, direction);
    }

    // Enable shooting 
    private void EnableShooting()
    {
        canShoot = true;
    }

    // To shoot while the key is pressed and deal with number of Lasers player has
    IEnumerator ShootContinuously()
    {
        while (true)
        {
            switch (numberOfLasers)
            {
                case 1:
                    InstantiateLaser("straight", 0);
                    break;
                case 2:
                    InstantiateLaser("straight", -0.3f);
                    InstantiateLaser("straight", 0.3f);
                    break;
                case 3:
                    InstantiateLaser("straight", 0f);
                    InstantiateLaser("left", -0.5f);
                    InstantiateLaser("right", 0.5f);
                    break;
                case 4:
                    InstantiateLaser("leftCorner", -0.5f);
                    InstantiateLaser("left", -0.2f);
                    InstantiateLaser("right", 0.2f);
                    InstantiateLaser("rightCorner", 0.5f);
                    break;
                case 5:
                    InstantiateLaser("straight", 0f);
                    InstantiateLaser("left", -0.2f);
                    InstantiateLaser("leftCorner", -0.5f);
                    InstantiateLaser("right", 0.2f);
                    InstantiateLaser("rightCorner", 0.5f);
                    break;
            }
            // Create a delay between  next shot
            yield return new WaitForSeconds(laserShootingPeriod);
        }
    }


    // Setting boundaries for moving the object
    private void SetMovementLimitsForPlayer()
    {
        float padding = 1.2f;
        int yMaxPadding = 8;
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - yMaxPadding;
    }

    // Destroy the Player object
    private void DestroyPlayer()
    {
        Destroy(gameObject);
    }

    // Frame Rate independent 2D object moving function
    private void MovePlayer()
    {
        // Horizontal and Vertical allows moving object with WASD or arrows
        float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * movingSpeedOfPlayer;
        float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * movingSpeedOfPlayer;
        // Getting X and Y positions, clamping Horizontal and Vertical movement
        // to avoid leaving the boundaries of screen
        float newXPosition = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        float newYPosition = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        // Setting X and Y positions to the object
        transform.position = new Vector2(newXPosition, newYPosition);
    }
}
