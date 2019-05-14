using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public Transform enemyPrefab;
    public Transform spawnPoint;

    public bool waveActive = false;
    public float timeBetweenWaves = 5f;
    private float countdown = 2f;
    private int numEnemiesSpawned = 0;


    private int waveNumber = 1;

    // Update is called once per frame
    void Update()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;
    }

    IEnumerator SpawnWave()
    {
        //for (int i = 0; i < waveNumber; i++)
        for (int i = 0; i < 1; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }

        waveNumber++;
    }

    void SpawnEnemy()
    {
        numEnemiesSpawned++;
        Transform curr = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        curr.gameObject.name = "Enemy" + numEnemiesSpawned;
    }
}
