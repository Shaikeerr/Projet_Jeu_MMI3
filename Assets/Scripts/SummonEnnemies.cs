using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonEnnemies : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 2f;
    public float maxRadius = 20f;
    public float minRadius = 10f;
    public int ennemyCount = 5; 

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnnemy", 2f, spawnInterval);
    }

    void SpawnEnnemy()
    {
        for (int i = 0; i < ennemyCount; i++)
        {
            Vector3 spawnPosition = GetRandomPosition();

            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
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
}
