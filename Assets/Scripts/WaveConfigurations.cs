﻿using System.Collections;
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
    public GameObject GetEnemyPrefab()
    {
        return enemyPrefab;
    }

    public float GetTimeBetweenSpawns()
    {
        return periodBetweenSpawning;
    }

    public float GetSpawnRandomFactor()
    {
        return spawningRandomFactor;
    }

    public int GetNumberOfEnemies()
    {
        return quantityOfEnemies;
    }

    public float GetMoveSpeed()
    {
        return movingSpeedOfEnemies;
    }
}
