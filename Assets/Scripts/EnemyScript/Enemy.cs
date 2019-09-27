using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] float durationOfExplosion = 1.0f;

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
        // Create laser object from enemie's position
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

    // Destroy the enemy object
    private void DestroyEnemy()
    {
        // Add score value to score field
        FindObjectOfType<GameStatus>().AddToScore(scoreValue);
        Destroy(gameObject);
        // When enemy destroyed create explosion effect
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, durationOfExplosion);
    }
}
