using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject enemyScout;
    public GameObject enemyInvader;
    public GameObject enemyCollector;
    public GameObject enemyGuard;
    public Transform spawnPoint;
    public Transform destination;

    public bool isTesting = true;
    public bool waveActive = false;
    public bool spawning = false;
    public int numEnemiesAlive;
    public float timeBetweenWaves = 5f;
    private int numEnemiesSpawned = 0;


    private int waveNumber = 1;

    // Update is called once per frame
    void Update()
    {

    }

    public void StartWave()
    {
        StartCoroutine(SpawnWave());
        waveActive = true;
        spawning = true;
    }

    IEnumerator SpawnWave()
    {
        if (isTesting)
        {
            //for (int i = 0; i < waveNumber; i++)
            for (int i = 0; i < 1; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(0.5f);
            }
        }
        else
        {
            switch (waveNumber)
            {
                case 0:
                    for (int i = 0; i < 4; i++)
                    {
                        SpawnScout();
                        yield return new WaitForSeconds(0.75f);
                    }
                    break;
                case 1:
                    for (int i = 0; i < 8; i++)
                    {
                        SpawnScout();
                        yield return new WaitForSeconds(0.75f);
                    }
                    break;
                case 2:
                    for (int i = 0; i < 8; i++)
                    {
                        if (i < 6)
                            SpawnScout();
                        else
                            SpawnGuard();
                        yield return new WaitForSeconds(0.5f);
                    }
                    break;
                case 3:
                    for (int i = 0; i < 10; i++)
                    {
                        if (i < 4)
                            SpawnScout();
                        else
                            SpawnGuard();
                        yield return new WaitForSeconds(0.75f);
                    }
                    break;
                case 4:
                    for (int i = 0; i < 6; i++)
                    {
                        SpawnInvader();
                        yield return new WaitForSeconds(0.9f);
                    }
                    break;
                case 5:
                    for (int i = 0; i < 16; i++)
                    {
                        if (i < 4)
                            SpawnScout();
                        else if (i < 12)
                            SpawnGuard();
                        else
                            SpawnInvader();
                        yield return new WaitForSeconds(0.5f);
                    }
                    break;
                case 6:
                    for (int i = 0; i < 10; i++)
                    {
                        SpawnInvader();
                        yield return new WaitForSeconds(0.5f);
                    }
                    break;
                case 7:
                    for (int i = 0; i < 30; i++)
                    {
                        SpawnScout();
                        yield return new WaitForSeconds(0.5f);
                    }
                    break;
                case 8:
                    for (int i = 0; i < 25; i++)
                    {
                        if (i < 10)
                            SpawnGuard();
                        else if (i < 20)
                            SpawnInvader();
                        else
                            SpawnCollector();
                        yield return new WaitForSeconds(0.5f);
                    }
                    break;
                case 9:
                    for (int i = 0; i < 35; i++)
                    {
                        if (i < 15)
                            SpawnScout();
                        else if (i < 25)
                            SpawnGuard();
                        else if (i < 30)
                            SpawnInvader();
                        else
                            SpawnCollector();
                        yield return new WaitForSeconds(0.5f);
                    }
                    break;
                case 10:
                    for (int i = 0; i < 15; i++)
                    {
                        SpawnCollector();
                        yield return new WaitForSeconds(0.5f);
                    }
                    break;
                case 11:
                    for (int i = 0; i < 40; i++)
                    {
                        SpawnGuard();
                        yield return new WaitForSeconds(0.5f);
                    }
                    break;
                case 12:
                    for (int i = 0; i < 70; i++)
                    {
                        SpawnScout();
                        yield return new WaitForSeconds(0.3f);
                    }
                    break;
                case 13:
                    for (int i = 0; i < 30; i++)
                    {
                        if (i < 15)
                            SpawnInvader();
                        else
                            SpawnCollector();
                        yield return new WaitForSeconds(0.4f);
                    }
                    break;
                case 14:
                    for (int i = 0; i < 100; i++)
                    {
                        if (i < 25)
                            SpawnScout();
                        else if (i < 50)
                            SpawnGuard();
                        else if (i < 75)
                            SpawnInvader();
                        else
                            SpawnCollector();
                        yield return new WaitForSeconds(0.4f);
                    }
                    break;
                default:
                    break;
            }
        }
        waveNumber++;
        spawning = false;
    }

    public void RemoveEnemy()
    {
        numEnemiesAlive--;

        if (!spawning && numEnemiesAlive <= 0)
        {
            waveActive = false;
        }
    }

    void SpawnEnemy()
    {
        numEnemiesSpawned++;
        numEnemiesAlive++;
        GameObject curr = Instantiate(enemyPrefab, spawnPoint.position,
            spawnPoint.rotation);
        curr.gameObject.name = "Enemy" + numEnemiesSpawned;

        EnemyHealth health = curr.GetComponent<EnemyHealth>();
        health.spawner = this;
    }

    void SpawnScout()
    {
        numEnemiesSpawned++;
        numEnemiesAlive++;
        GameObject curr = Instantiate(enemyScout, spawnPoint.position,
            spawnPoint.rotation);
        curr.gameObject.name = "Enemy" + numEnemiesSpawned;

        EnemyHealth health = curr.GetComponent<EnemyHealth>();
        health.spawner = this;
    }

    void SpawnGuard()
    {
        numEnemiesSpawned++;
        numEnemiesAlive++;
        GameObject curr = Instantiate(enemyGuard, spawnPoint.position,
            spawnPoint.rotation);
        curr.gameObject.name = "Enemy" + numEnemiesSpawned;

        EnemyHealth health = curr.GetComponent<EnemyHealth>();
        health.spawner = this;
    }

    void SpawnCollector()
    {
        numEnemiesSpawned++;
        numEnemiesAlive++;
        GameObject curr = Instantiate(enemyCollector, spawnPoint.position,
            spawnPoint.rotation);
        curr.gameObject.name = "Enemy" + numEnemiesSpawned;

        EnemyHealth health = curr.GetComponent<EnemyHealth>();
        health.spawner = this;
    }

    void SpawnInvader()
    {
        numEnemiesSpawned++;
        numEnemiesAlive++;
        GameObject curr = Instantiate(enemyInvader, spawnPoint.position,
            spawnPoint.rotation);
        curr.gameObject.name = "Enemy" + numEnemiesSpawned;

        EnemyHealth health = curr.GetComponent<EnemyHealth>();
        health.spawner = this;
    }

}