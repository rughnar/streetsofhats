
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyManager : MonoBehaviour
{
    public List<int> enemiesPriority;
    //public List<float> enemiesQuantity;
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private List<Transform> spawnPoints;

    //[SerializeField] private int maxEnemiesAtTheSameTime = 999;
    [SerializeField] private float timeBetweenSpawns = 4f;
    private GameManager gameManager;
    private int currentEnemiesOnLevel = 0;
    public int quantityEnemiesDestroyed = 0;
    private float lastEnemySpawnTime = 0f;
    private List<int> currentSpawnedEnemies;
    private int totalEnemiesToSpawn = 0;

    System.Random rnd = new System.Random();
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        lastEnemySpawnTime = 0f;
        Spawn(1);
        currentSpawnedEnemies = new List<int>();
        /*if (enemiesQuantity != null)
        {
            foreach (int enemyQuantity in enemiesQuantity)
            {
                totalEnemiesToSpawn += enemyQuantity;
                currentSpawnedEnemies.Add(0);
            }
        }*/

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
        //if (quantityEnemiesDestroyed + currentEnemiesOnLevel < totalEnemiesToSpawn)

        SpawnRandomEnemy();
        //currentEnemiesOnLevel++;

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


    private void SpawnRandomEnemy()
    {

        //int index = Mathf.Abs(Random.Range(0, enemyPrefabs.Count));
        GameObject enemy = Instantiate(GetObjectWithMaxProb(), spawnPoints[Random.Range(0, spawnPoints.Count)].position, Quaternion.identity);
        enemy.GetComponent<EnemyController>().FaceCenter();
    }
    /*
        private void SpawnRandomEnemyRecursively(int minusIndex)
        {
            int index = Mathf.Abs(Random.Range(minusIndex, enemyPrefabs.Count));
            if (currentSpawnedEnemies[index] < enemiesQuantity[index])
            {
                GameObject enemy = Instantiate(enemyPrefabs[index], spawnPoints[Random.Range(0, spawnPoints.Count)].position, Quaternion.identity);
                enemy.GetComponent<EnemyController>().FaceCenter();
                currentSpawnedEnemies[index] += 1;
            }
            else
            {
                SpawnRandomEnemyRecursively(minusIndex - 1);
            }
        }
    */
    GameObject GetObjectWithMaxProb()
    {
        int totalWeight = enemiesPriority.Sum(); // Using LINQ for suming up all the values
        int randomNumber = rnd.Next(0, totalWeight);

        GameObject myGameObject = null;
        for (int i = 0; i < enemyPrefabs.Count; i++)
        {
            if (randomNumber < enemiesPriority[i])
            {
                myGameObject = enemyPrefabs[i];
                break;
            }
            randomNumber -= enemiesPriority[i];
        }
        return myGameObject;
    }

}
