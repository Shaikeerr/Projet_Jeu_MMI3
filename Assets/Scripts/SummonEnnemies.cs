using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonEnnemies : MonoBehaviour
{
    public GameObject LVL1EnemyPrefab;
    public GameObject LV2EnemyPrefab;
    public GameObject LV3EnemyPrefab;

    [Header("Wave Settings")]
    public int currentWave = 0;
    public float spawnInterval = 10f;
    public int timeBetweenWaves = 0;
    public int timeBetweenWavesAfterFirst = 30; // Set to public because used in the CooldownBar script
    public int timeSinceLastWave = 0; // Set to public because used in the CooldownBar script

    [Header("Spawn Settings")]
    public float maxRadius = 20f;
    public float minRadius = 10f;
    public int ennemyCount = 5; 


    void Start()
    {
        if (spawnInterval != 0f)
        {
            SpawnEnnemy();
            InvokeRepeating("SpawnEnnemy", spawnInterval, spawnInterval);
            StartCoroutine(StartChrono());
        }
    }

    void SpawnEnnemy()
    {

        if (currentWave == 0)
        {
            return; // No enemies in the first wave (Wave 0)
        }

        if (currentWave >= 4)
        {
            ennemyCount++; // Increase enemy count for waves 4 and above

        }

        for (int i = 0; i < ennemyCount; i++)
        {
            Vector3 spawnPosition = GetRandomPosition();


            switch(currentWave)
            {
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
        randomPosition = Random.insideUnitSphere * maxRadius; // Get a random point inside a sphere with radius maxRadius

        randomPosition.y = 0;

        randomPosition = new Vector3(
            transform.position.x + randomPosition.x,
            0,
            transform.position.z + randomPosition.z
        );
    }
    while (randomPosition.magnitude < minRadius || !IsWithinMapBounds(randomPosition)); // If the position is too close to the center or outside the map bounds, get a new position

    return randomPosition;
}

    bool IsWithinMapBounds(Vector3 position) // Check if the position is within the map bounds
{
    float mapMinX = -50f;
    float mapMaxX = 50f;
    float mapMinZ = -50f;
    float mapMaxZ = 50f;

    return position.x >= mapMinX && position.x <= mapMaxX && position.z >= mapMinZ && position.z <= mapMaxZ;
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
                    timeBetweenWaves = timeBetweenWavesAfterFirst; // Set the desired time between waves for subsequent waves
                }

                SpawnEnnemy();
            }
        }
    }
}