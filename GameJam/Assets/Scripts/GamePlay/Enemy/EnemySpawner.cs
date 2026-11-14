using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField, Range(0, 5)] private float maxSpawnInterval = 5.0f;
    [SerializeField, Range(0, 5)] private float minSpawnInterval = 1.0f;

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

            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

            float spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
