using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    private List<Transform> wayPoints; 
    private WaveConfigurations waveConfigurations;
    private int wayPointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Get the relevant waypoints from Wave Config
        wayPoints = waveConfigurations.GetWaypoints();
        transform.position = wayPoints[wayPointIndex].position;
    }

    // Update is called once per frame
    void Update()
    {
        MovementOfEnemy();
    }

    // Setter for Wave Configuration
    public void SetWaveConfig(WaveConfigurations waveConfig)
    {
        waveConfigurations = waveConfig;
    }

    // Handling Movement of Enemy
    private void MovementOfEnemy()
    {
        // Go through the last Waypoint
        if (wayPointIndex <= wayPoints.Count - 1)
        {
            // Assign target position 
            Vector3 targetPosition = wayPoints[wayPointIndex].position;
            float movementFrame = waveConfigurations.GetMovingSpeedOfEnemies() * Time.deltaTime;
            // Smoothly move towards targeted waypoint's position 
            transform.position = Vector2.MoveTowards
                (transform.position, targetPosition, movementFrame);
            // When arrived to targeted waypoint increment the index
            if (transform.position == targetPosition)
            {
                wayPointIndex++;
            }
        }
        // When arrived to the last waypoint destroy itself
        else
        {
            Destroy(gameObject);
        }
    }

}
