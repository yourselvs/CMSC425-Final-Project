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
            Debug.Log("Spawning wave " + waveNumber);
            switch (waveNumber)
            {
                case 0:
                    for (int i = 0; i < 3; i++)
                    {
                        SpawnScout();
                        yield return new WaitForSeconds(0.75f);
                    }
                    break;
                case 1:
                    for (int i = 0; i < 4; i++)
                    {
                        if (i < 3)
                            SpawnScout();
                        else
                            SpawnGuard();
                        yield return new WaitForSeconds(1f);
                    }
                    break;
                case 2:
                    for (int i = 0; i < 5; i++)
                    {
                        SpawnScout();
                        yield return new WaitForSeconds(0.5f);
                    }
                    break;
                case 3:
                    for (int i = 0; i < 5; i++)
                    {
                        if (i < 3)
                            SpawnInvader();
                        else
                            SpawnGuard();
                        yield return new WaitForSeconds(0.75f);
                    }
                    break;
                case 4:
                    for (int i = 0; i < 9; i++)
                    {
                        if (i < 3)
                            SpawnScout();
                        else if (i < 5)
                            SpawnGuard();
                        else if (i < 7)
                            SpawnInvader();
                        else
                            SpawnCollector();
                        yield return new WaitForSeconds(0.9f);
                    }
                    break;
                //case 5:
                //    for (int i = 0; i < 9; i++)
                //    {
                //        if (i < 3)
                //            SpawnScout();
                //        else if (i < 5)
                //            SpawnGuard();
                //        else if (i < 6)
                //            SpawnInvader();
                //        else
                //            SpawnCollector();
                //        yield return new WaitForSeconds(0.5f);
                //    }
                //    break;
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

        if(!spawning && numEnemiesAlive <= 0)
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
        Debug.Log("Spawning scout");
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
        Debug.Log("Spawning guard");
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
        Debug.Log("Spawning collector");
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
        Debug.Log("Spawning invader");
        numEnemiesSpawned++;
        numEnemiesAlive++;
        GameObject curr = Instantiate(enemyInvader, spawnPoint.position,
            spawnPoint.rotation);
        curr.gameObject.name = "Enemy" + numEnemiesSpawned;

        EnemyHealth health = curr.GetComponent<EnemyHealth>();
        health.spawner = this;
    }

}