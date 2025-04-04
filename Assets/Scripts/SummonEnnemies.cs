using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonEnnemies : MonoBehaviour
{
    public GameObject LVL1EnemyPrefab;
    public GameObject LV2EnemyPrefab;
    public GameObject LV3EnemyPrefab;

    public int currentWave = 0;

    public float spawnInterval = 10f;
    public float maxRadius = 20f;
    public float minRadius = 10f;
    public int ennemyCount = 5; 

    public int timeBetweenWaves = 5; // Initial time between waves set to 5 seconds
    public int timeSinceLastWave = 0;

    void Start()
    {
        if (spawnInterval != 0f)
        {
            SpawnEnnemy();
            InvokeRepeating("SpawnEnnemy", 0f, spawnInterval);
            StartCoroutine(StartChrono());
        }
    }

    void SpawnEnnemy()
    {
        for (int i = 0; i < ennemyCount; i++)
        {
            Vector3 spawnPosition = GetRandomPosition();

            switch(currentWave)
            {
                case 0:
                case 1:
                    Instantiate(LVL1EnemyPrefab, spawnPosition, Quaternion.identity);
                    break;
                case 2:
                    Instantiate(LV2EnemyPrefab, spawnPosition, Quaternion.identity);
                    break;
                case > 2:
                    Instantiate(LV3EnemyPrefab, spawnPosition, Quaternion.identity);
                    break;
            }
        }
    }

    Vector3 GetRandomPosition()
    {
        Vector3 randomPosition;

        do
        {
            randomPosition = Random.insideUnitSphere * maxRadius;

            randomPosition.y = 0;
        }
        while (randomPosition.magnitude < minRadius);

        return new Vector3(
            transform.position.x + randomPosition.x,
            1, 
            transform.position.z + randomPosition.z
        );
    }

    IEnumerator StartChrono()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeSinceLastWave += 1;
            if (timeSinceLastWave >= timeBetweenWaves)
            {
                currentWave++;
                timeSinceLastWave = 0;

                // Update timeBetweenWaves after the first wave
                if (currentWave == 1)
                {
                    timeBetweenWaves = 30; // Set the desired time between waves for subsequent waves
                }
            }
        }
    }
}