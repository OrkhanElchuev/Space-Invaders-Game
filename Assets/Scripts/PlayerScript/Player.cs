using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Configuration parameters
    // Headers for readability in unity
    [Header("Player Configurations")]
    [SerializeField] float movingSpeedOfPlayer = 10.0f;
    private int playerHealthPoints = 4;
    private int maxHealthPoints = 5;
    private Transform HealthBar;

    [SerializeField] GameObject PlayerInfo;

    [Header("Shooting")]
    [SerializeField] float laserSpeed = 10.0f;
    [SerializeField] float laserShootingPeriod = 0.3f;
    [SerializeField] GameObject playerLaserObject;
    private int numberOfLasers = 1;
    private Coroutine shootingCoroutine;

    [Header("Particle Effect")]
    [SerializeField] GameObject deathVFXObject;
    private float durationOfExplosion = 0.4f;
    [SerializeField] GameObject sceneLoaderObject;

    private GameStatus gameStatus;
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
        if (Input.GetButtonDown("Attack"))
        {
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
        float maxAttackSShootingPeriod = 0.07f;
        if (laserShootingPeriod > maxAttackSShootingPeriod)
        {
            laserShootingPeriod /= 1.2f;
        }
        else
        {
            // If attack speed is max, consider the next ones as IncreaseScore 
            IncreaseScore();
        }
    }

    // Increase number of Lasers from "WeaponPowerUp"
    private void IncreaseNumberOfLasers()
    {
        if (numberOfLasers < 3)
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

    // On collision of enemy or enemy bullet with player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PowerUp")
        {
            UpgradePlayer(collision.gameObject.GetComponent<PowerUps>().GetPowerUpType());
            Destroy(collision.gameObject);
        }
        else
        {
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

    // To shoot while the key is pressed
    IEnumerator ShootContinuously()
    {
        while (true)
        {
            switch (numberOfLasers)
            {
                case 1:
                    // Create laser object when player shoots
                    // + new Vector3(0,1,0) for shifting laser projectile up
                    GameObject laser = Instantiate(playerLaserObject,
                     transform.position + new Vector3(0, 1, 0),
                        Quaternion.identity) as GameObject;
                    // Setting velocity for laser
                    laser.GetComponent<PlayerLaser>().CreateItself(laserSpeed, "straight");
                    break;
                case 2:
                    GameObject laser1 = Instantiate(playerLaserObject,
                     transform.position + new Vector3(-0.5f, 1, 0),
                        Quaternion.identity) as GameObject;
                    laser1.GetComponent<PlayerLaser>().CreateItself(laserSpeed, "straight");
                    GameObject laser2 = Instantiate(playerLaserObject,
                    transform.position + new Vector3(0.5f, 1, 0),
                       Quaternion.identity) as GameObject;
                    laser2.GetComponent<PlayerLaser>().CreateItself(laserSpeed, "straight");
                    break;
                case 3:
                    GameObject laser3 = Instantiate(playerLaserObject,
                     transform.position + new Vector3(0, 1, 0),
                        Quaternion.identity) as GameObject;
                    laser3.GetComponent<PlayerLaser>().CreateItself(laserSpeed, "straight");
                    GameObject laser4 = Instantiate(playerLaserObject,
                     transform.position + new Vector3(-0.5f, 1, 0),
                        Quaternion.identity) as GameObject;
                    laser4.GetComponent<PlayerLaser>().CreateItself(laserSpeed, "left");
                    GameObject laser5 = Instantiate(playerLaserObject,
                    transform.position + new Vector3(0.5f, 1, 0),
                       Quaternion.identity) as GameObject;
                    laser5.GetComponent<PlayerLaser>().CreateItself(laserSpeed, "right");
                    break;
            }
            // Create a delay between next shot
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
