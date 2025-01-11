
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<int> enemiesQuantityToSpawn;
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private int maxEnemiesAtTheSameTime = 999;
    [SerializeField] private float timeBetweenSpawns = 4f;
    private GameManager gameManager;
    private int currentEnemiesOnLevel = 0;
    public int quantityEnemiesDestroyed = 0;
    private float lastEnemySpawnTime = 0f;
    private List<int> currentSpawnedEnemies;
    private int totalEnemiesToSpawn = 0;


    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        lastEnemySpawnTime = 0f;
        Spawn(1);
        currentSpawnedEnemies = new List<int>();
        if (enemiesQuantityToSpawn != null)
        {
            foreach (int enemyQuantity in enemiesQuantityToSpawn)
            {
                totalEnemiesToSpawn += enemyQuantity;
                currentSpawnedEnemies.Add(0);
            }
        }

    }

    void FixedUpdate()
    {
        if (Time.fixedTime - lastEnemySpawnTime > timeBetweenSpawns)
        {
            Spawn(1);
            lastEnemySpawnTime = Time.fixedTime;
        }
    }

    void Spawn(int quantity)
    {
        //&& currentEnemiesOnLevel < maxEnemiesAtTheSameTime
        if (quantityEnemiesDestroyed + currentEnemiesOnLevel < totalEnemiesToSpawn)
        {
            SpawnRandomEnemyRecursively(0);
            currentEnemiesOnLevel++;
        }
    }

    public void EnemyTakenDown()
    {
        currentEnemiesOnLevel -= 1;
        quantityEnemiesDestroyed += 1;
        if (quantityEnemiesDestroyed == totalEnemiesToSpawn)
        {
            gameManager.EndLevel();
        }
    }


    private void SpawnRandomEnemyRecursively(int minusIndex)
    {
        int index = Mathf.Abs(Random.Range(minusIndex, enemyPrefabs.Count));
        if (currentSpawnedEnemies[index] < enemiesQuantityToSpawn[index])
        {
            Instantiate(enemyPrefabs[index], spawnPoints[Random.Range(0, spawnPoints.Count)].position, Quaternion.identity);
            currentSpawnedEnemies[index] += 1;
        }
        else
        {
            SpawnRandomEnemyRecursively(minusIndex - 1);
        }
    }

}
