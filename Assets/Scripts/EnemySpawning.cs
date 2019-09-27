using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    [SerializeField] List<WaveConfigurations> waveConfigurations;
    private int startWave = 0;
    private float delayBetweenWaves = 6.0f;

    // Start is called before the first frame update  
    IEnumerator Start()
    {
         yield return StartCoroutine(SpawnAllWaves());
    }

    // For spawning all waves
    private IEnumerator SpawnAllWaves()
    {
        // Iterating through waves and activating coroutine
        for (int waveIndex = startWave; waveIndex < waveConfigurations.Count; waveIndex++)
        {
            WaveConfigurations currentWave = waveConfigurations[waveIndex];
            yield return DelayBetweenWaves(currentWave);
        }
    }

    // Put a delay between waves spawning
     private IEnumerator DelayBetweenWaves(WaveConfigurations currentWave)
    {   
        StartCoroutine(SpawnAllEnemies(currentWave));
        // Wait for all enemies to appear, then add delay
        yield return new WaitForSeconds(delayBetweenWaves + 
        currentWave.GetQuantityOfEnemies() * 
        currentWave.GetPeriodBetweenSpawning());
    }

    // For spawning all enemies in current wave
    private IEnumerator SpawnAllEnemies(WaveConfigurations waveConfig)
    {
        // i corresponds to enemy count
        for (int i = 0; i < waveConfig.GetQuantityOfEnemies(); i++)
        {
            // Creating enemies and spawning after specified delay
            GameObject newEnemy = Instantiate(waveConfig.GetEnemyPrefab(),
                waveConfig.GetWaypoints()[0].transform.position,
                Quaternion.identity);
            newEnemy.GetComponent<EnemyPath>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetPeriodBetweenSpawning());
        }
    }
}
