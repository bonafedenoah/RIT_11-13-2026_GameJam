using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField,  UnityEngine.Range(0, 5)] private float maxSpawnInterval = 5.0f;
    [SerializeField, UnityEngine.Range(0, 5)] private float minSpawnInterval = 1.0f;
    [SerializeField] private float minDistanceBetweenEnemies = 2f;

    List<Vector2> spawnPositions = new List<Vector2>();

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

            if (spawnPositions.Count > 5)
            {
                spawnPositions.Insert(0, spawnPos);
                spawnPositions.RemoveAt(5);
            }
            else
            {
                spawnPositions.Insert(0, spawnPos);
            }


            bool tooClose = false;
            for (int i = 0; i < spawnPositions.Count; i++)
            {
                if (Vector2.Distance(spawnPos, spawnPositions[i]) < minDistanceBetweenEnemies)
                {
                    xPos = -10f;
                    yPos = Random.Range(-4f, 4f);
                    spawnPos = new Vector2(xPos, yPos);

                    tooClose = true;
                }
            }

            if (!tooClose)
            {
                Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            }

            float spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
