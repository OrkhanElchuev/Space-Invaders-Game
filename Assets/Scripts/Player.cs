using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Configuration parameters
    // Headers for readability in unity
    [Header("Player Configurations")] 
    [SerializeField] float movingSpeedOfPlayer = 10.0f;

    [Header("Shooting")] 
    [SerializeField] float laserSpeed = 10.0f;
    [SerializeField] float laserShootingPeriod = 0.2f;
    [SerializeField] GameObject playerLaserObject;

    [Header("Particle Effect")]
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplosion = 1.0f;

    Coroutine shootingCoroutine;

    private float xMin;
    private float xMax;
    private float yMin;
    private float yMax;

    // Start is called before the first frame update
    void Start()
    {
        SetMovementLimitsForPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        Shoot();
    }

    // Method for player shooting
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

    // To shoot while the key is pressed
    IEnumerator ShootContinuously()
    {
        while (true)
        {
            // Quaternion corresponds to "no rotatition" for instantiated object
            GameObject laser = Instantiate(playerLaserObject, transform.position,
                Quaternion.identity) as GameObject;
            // Setting velocity for laser
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
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

    // Destroy the Player object and execute explosion effect
    private void DestroyPlayer()
    {
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, durationOfExplosion);
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
