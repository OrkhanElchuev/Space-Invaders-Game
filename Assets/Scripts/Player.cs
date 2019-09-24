using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Configuration parameters
    // Headers for readability in unity
    [Header("Player Configurations")] 
    [SerializeField] float movingSpeedOfPlayer = 10.0f;

    [Header("Projectile")] 
    [SerializeField] float laserSpeed = 10.0f;
    [SerializeField] float laserShootingPeriod = 0.2f;
    [SerializeField] GameObject laserPrefab;

    Coroutine shootingCoroutine;

    private float xMin;
    private float xMax;
    private float yMin;
    private float yMax;

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
        if (Input.GetButtonDown("Fire1"))
        {
            shootingCoroutine = StartCoroutine(ShootContinuously());
        }
        // If keyboard is released stop shooting
        if (Input.GetButtonUp("Fire1"))
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
            GameObject laser = Instantiate(laserPrefab, transform.position,
                Quaternion.identity) as GameObject;
            // Setting velocity for laser
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
            // Create a delay between next shot
            yield return new WaitForSeconds(laserShootingPeriod);
        }
    }

    // Frame Rate independent 2D object moving function
    private void MovePlayer()
    {
        // Horizontal and Vertical allows moving object with WASD or arrows
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * movingSpeedOfPlayer;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * movingSpeedOfPlayer;
        // Getting X and Y positions, clamping Horizontal and Vertical movement
        // to avoid leaving the boundaries of screen
        var newXPosition = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPosition = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        // Setting X and Y positions to the object
        transform.position = new Vector2(newXPosition, newYPosition);
    }
}
