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
  [SerializeField] int playerHealthPoints = 4;
  private int maxHealthPoints = 5;
  private Transform HealthBar;

  [SerializeField] GameObject PlayerInfo;

  [Header("Shooting")]
  [SerializeField] float laserSpeed = 10.0f;
  [SerializeField] float laserShootingPeriod = 0.2f;
  [SerializeField] GameObject playerLaserObject;

  [Header("Particle Effect")]
  [SerializeField] GameObject deathVFXObject;
  [SerializeField] float durationOfExplosion = 1.0f;
  [SerializeField] GameObject sceneLoaderObject;

  private GameStatus gameStatus;

  private Coroutine shootingCoroutine;

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
      GameObject laser = Instantiate(playerLaserObject,
       transform.position + new Vector3(0, 1, 0),
          Quaternion.identity) as GameObject;
      // Setting velocity for laser
      laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
      // Create a delay between next shot
      yield return new WaitForSeconds(laserShootingPeriod);
    }
  }

  // On collision of enemy or enemy bullet with player
  private void OnTriggerEnter2D(Collider2D collision)
  {
    playerHealthPoints--;
    GetComponent<SpriteRenderer>().enabled = false;
    GetComponent<Player>().enabled = false;
    if (shootingCoroutine != null)
    {
      StopCoroutine(shootingCoroutine);
    }
    Invoke("Respawn", 0.6f);
    GameObject explosion = Instantiate(deathVFXObject, transform.position, transform.rotation);
    Destroy(explosion, durationOfExplosion);
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

  private void Respawn()
  {
    GetComponent<SpriteRenderer>().enabled = true;
    GetComponent<Player>().enabled = true;
    transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0, 100));
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
    GameObject explosion = Instantiate(deathVFXObject, transform.position, transform.rotation);
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
