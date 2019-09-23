using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    [SerializeField] List<WaveConfigurations> waveConfigurations;
    [SerializeField] int startWave = 0;
    [SerializeField] bool enemyLooping = false;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (enemyLooping);
    }

    // For spawning all waves
    private IEnumerator SpawnAllWaves()
    {
        // Iterating through waves and activating coroutine
        for (int waveIndex = startWave; waveIndex < waveConfigurations.Count; waveIndex++)
        {
            WaveConfigurations currentWave = waveConfigurations[waveIndex];
            yield return StartCoroutine(SpawnAllEnemies(currentWave));
        }
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
