using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField,  UnityEngine.Range(0, 5)] private float maxSpawnInterval = 3.0f;
    [SerializeField, UnityEngine.Range(0, 5)] private float minSpawnInterval = 1.0f;
    [SerializeField] private float minDistanceBetweenBuildings = .5f;

    List<Vector2> spawnPositions = new List<Vector2>();
    private const int maxStoredPositions = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            float xPos = -10f;
            float yPos = Random.Range(-4f, 4f);
            Vector2 spawnPos = new Vector2(xPos, yPos);

            if (CheckPosition(spawnPos))
            {
                Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
                
                spawnPositions.Insert(0, spawnPos);
                if (spawnPositions.Count > maxStoredPositions)
                {
                    spawnPositions.RemoveAt(spawnPositions.Count - 1);
                }
            }

            float spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private bool CheckPosition(Vector2 position)
    {
        for (int i = 0; i < spawnPositions.Count; i++)
        {
            if (Vector2.Distance(position, spawnPositions[i]) < minDistanceBetweenBuildings)
            {
                return false;
            }
        }

        return true;
    }
}
