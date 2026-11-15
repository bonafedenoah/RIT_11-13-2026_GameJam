using UnityEngine;
using System.Collections.Generic;

public class BuildingSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    [SerializeField] private GameObject buildingPrefab;
    [SerializeField] private int maxBuildings = 15;
    private int currentBuildings = 0;

    public int CurrentBuildings
    {
        get
        {
            return currentBuildings;
        }

        set
        {
            currentBuildings = value;
        }
    }

    [Header("Spawn Area Bounds")]
    [SerializeField] private float minX = 4f;
    [SerializeField] private float maxX = 8f;
    [SerializeField] private float minY = -4f;
    [SerializeField] private float maxY = 4f;

    [Header("Spacing Prevention")]
    [SerializeField] private float minDistanceBetweenBuildings = 1.6f;

    private List<Vector2> spawnedPositions = new List<Vector2>();

    void Start()
    {
        SpawnAllBuildings();
        currentBuildings = spawnedPositions.Count;
    }

    void SpawnAllBuildings()
    {
        int spawnedCount = 0;
        int maxAttemptsTotal = 1000;
        int totalAttempts = 0;

        while (spawnedCount < maxBuildings && totalAttempts < maxAttemptsTotal)
        {
            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(minY, maxY);
            Vector2 spawnPos = new Vector2(randomX, randomY);

            bool tooClose = false;
            foreach (Vector2 pos in spawnedPositions)
            {
                if (Vector2.Distance(spawnPos, pos) < minDistanceBetweenBuildings)
                {
                    tooClose = true;
                    break;
                }
            }

            if (tooClose == false)
            {
                if (buildingPrefab != null)
                {
                    Instantiate(buildingPrefab, new Vector3(spawnPos.x, spawnPos.y, 1f), Quaternion.identity);
                    spawnedPositions.Add(spawnPos);
                    spawnedCount++;
                }
            }

            totalAttempts++;
        }
    }
}