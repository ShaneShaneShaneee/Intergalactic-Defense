using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public static int enemiesAlive = 0;
    public Wave[] waves;
    [SerializeField] float TimeBetweenWaves = 10f;
    float countdown = 3f;
    int waveIndex = 0;
    public Text waveText;
    [SerializeField] Transform Spawnpoint;
    [SerializeField] LifeMonitor _LifeMonitor;
    // Update is called once per frame
    void Update()
    {
        if(enemiesAlive > 0)
        {
            return;
        }

        if (waveIndex == waves.Length)
        {
            _LifeMonitor.WinLevel();
        }

        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = TimeBetweenWaves;
            return;
        }

        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
        waveText.text = string.Format("{0:00.00}", countdown);
    }

    IEnumerator SpawnWave()
    {
        PlayerStats.waves++;

        Wave wave = waves[waveIndex];

        enemiesAlive = wave.count;

        for (int i = 0; i < wave.count; i++)
        {
            spawnEnemy(wave.Enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }

        waveIndex++;
    }

    void spawnEnemy(GameObject enemyPrefab)
    {
        Instantiate(enemyPrefab, Spawnpoint.position, Spawnpoint.rotation);
    }


}
