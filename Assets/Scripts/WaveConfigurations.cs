using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Creating Scriptable Object with custom name
[CreateAssetMenu(menuName = "Enemy Wave Config")]

public class WaveConfigurations : ScriptableObject
{
    // Making menu more readable by adding a header
    [Header("Wave Configurations")]

    // Creating and initializing modifiable variables
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] int quantityOfEnemies = 5;
    [SerializeField] float movingSpeedOfEnemies = 1.0f;
    [SerializeField] float periodBetweenSpawning = 1.0f;
    [SerializeField] float spawningRandomFactor = 1.0f;

    // Getters
    public List<Transform> GetWaypoints()
    {
        List<Transform> waveWaypoints = new List<Transform>();
        foreach (Transform child in pathPrefab.transform)
        { 
            waveWaypoints.Add(child);
        }
        return waveWaypoints;
    }

    public GameObject GetEnemyPrefab()
    {
        return enemyPrefab;
    }

    public float GetPeriodBetweenSpawning()
    {
        return periodBetweenSpawning;
    }
  
    public float GetSpawningRandomFactor()
    {
        return spawningRandomFactor;
    }

    public int GetQuantityOfEnemies()
    {
        return quantityOfEnemies;
    }

    public float GetMovingSpeedOfEnemies()
    {
        return movingSpeedOfEnemies;
    }
}
