using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] int enemyHealthPoints = 100;
    [SerializeField] int scoreValue = 100;

    [Header("Shooting")]
    [SerializeField] float minPeriodBetweenShots = 0.2f;
    [SerializeField] float maxPeriodBetweenShots = 3.0f;
    [SerializeField] GameObject enemyLaserObject;
    [SerializeField] float laserSpeed = 10.0f;
    private float shotCounter;

    [Header("Explosion Effect")]
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplosion = 0.3f;

    [Header("PowerUps")]
    [SerializeField] GameObject powerUpObject;
    private string[] powerUpsArray = { "HealthPowerUp", "AttackSpeedPowerUp", "ScorePowerUp", "WeaponPowerUp" };

    // Start is called before the first frame update
    void Start()
    {
        // Giving shotCounter random value in start in the range between min and max 
        shotCounter = Random.Range(minPeriodBetweenShots, maxPeriodBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    // Enemy shooting after random amount of time passed in a range(min, max)
    public void CountDownAndShoot()
    {
        // Shot counter decrement time that one frame takes 
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0.0f)
        {
            Shoot();
            // Reset shot Counter to new random value
            shotCounter = Random.Range(minPeriodBetweenShots, maxPeriodBetweenShots);
        }
    }

    // Method for enemy shooting
    private void Shoot()
    {
        // Create laser object from enemy's position
        GameObject enemyLaser = Instantiate(enemyLaserObject,
            transform.position, Quaternion.identity) as GameObject;
        // -laserSpeed to shoot downwards
        enemyLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -laserSpeed);
    }

    // Decrease Health of Enemy
    private void ProcessHit(PlayerLaser laserDamage)
    {
        enemyHealthPoints -= laserDamage.GetDamage();
        laserDamage.Hit();
        // Destroy object when health <= 0
        if (enemyHealthPoints <= 0)
        {
            DestroyEnemy();
        }
    }

    // When player laser hits enemy deal damage
    private void OnTriggerEnter2D(Collider2D collider)
    {
        PlayerLaser laserDamage = collider.gameObject.GetComponent<PlayerLaser>();
        // Avoid Null reference Exception
        if (!laserDamage)
        {
            return;
        }
        ProcessHit(laserDamage);
    }

    // Drop Power Up from enemy
    private void DropPowerUp()
    {
        // Randomly get power up type
        int randomPowerUpType = Random.Range(0, 4);
        // PowerUp instantiation
        GameObject powerUp = Instantiate(powerUpObject,
        transform.position, Quaternion.identity) as GameObject;
        // PowerUp size 
        RectTransform rt = (RectTransform)powerUp.transform;
        // Check the position of enemy carrying powerup
        Vector3 set = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 
        Screen.height / 2, Camera.main.nearClipPlane));
        // If object is in the right side
        if (powerUp.transform.position.x > set.x - rt.rect.width / 2)
        {
            powerUp.transform.position = new Vector3(set.x - rt.rect.width / 2,
             powerUp.transform.position.y, powerUp.transform.position.z);
        }
        // If object is in the left side
        else if (powerUp.transform.position.x < -set.x + rt.rect.width / 2)
        {
            powerUp.transform.position = new Vector3(-set.x + rt.rect.width / 2,
             powerUp.transform.position.y, powerUp.transform.position.z);
        }
        PowerUps powerUpScript = powerUp.GetComponent<PowerUps>();
        powerUpScript.SetPowerUpType(powerUpsArray[randomPowerUpType]);
    }

    private void BossDestroyed()
    {
        // Add score value to score field
        FindObjectOfType<GameStatus>().AddToScore(scoreValue);
        // Make boss invisible after it is killed
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<PolygonCollider2D>().enabled = false;
        GetComponent<Enemy>().enabled = false;
        // When boss destroyed create explosion effect
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, durationOfExplosion);
    }

    // Load win menu
    public void LoadGameFinished()
    {
        SceneManager.LoadScene("GameFinished");
    }

    // Destroy the enemy object
    private void DestroyEnemy()
    {
        // If final boss is killed load win menu after delay
        if (gameObject.tag == "Boss")
        {
            BossDestroyed();
            Invoke("LoadGameFinished", 2.5f);
        }
        else
        {
            int probabilityOfPowerUp = Random.Range(1, 101);
            // 10 percent probability of dropping power up
            if (probabilityOfPowerUp <= 10)
            {
                DropPowerUp();
            }
            // Add score value to score field
            FindObjectOfType<GameStatus>().AddToScore(scoreValue);
            Destroy(gameObject);
            // When enemy destroyed create explosion effect
            GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
            Destroy(explosion, durationOfExplosion);
        }
    }
}
