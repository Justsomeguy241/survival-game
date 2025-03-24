using System.Collections;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private GameObject SquareEnemyPrefab;
    [SerializeField] private GameObject CircleEnemyPrefab;
    [SerializeField] private GameObject HexagonEnemyPrefab;
    [SerializeField] private float SpawnMinimum = 1f;
    [SerializeField] private float SpawnMaximum = 3f;
    [SerializeField] private float SpawnRangeMinimum = 1f;
    [SerializeField] private float SpawnRangeMaximum = 5f;
    [SerializeField] private int SpawnBatchMinimum = 1;
    [SerializeField] private int SpawnBatchMaximum = 3;

    // Weights for enemy spawn probability (higher means more likely to spawn)
    [SerializeField] private int SquareEnemyWeight = 50; // 50% chance
    [SerializeField] private int CircleEnemyWeight = 30; // 30% chance
    [SerializeField] private int HexagonEnemyWeight = 20; // 20% chance

    void Start()
    {
        StartCoroutine(SpawnEnemy()); // Start the spawning loop once
    }

    private IEnumerator SpawnEnemy()
    {
        while (true) // Infinite loop to keep spawning at intervals
        {
            float interval = Random.Range(SpawnMinimum, SpawnMaximum);
            yield return new WaitForSeconds(interval); // Wait before spawning the next batch

            int enemyBatches = Random.Range(SpawnBatchMinimum, SpawnBatchMaximum);
            for (int i = 0; i < enemyBatches; i++)
            {
                Vector2 spawnOffset = Random.insideUnitCircle * Random.Range(SpawnRangeMinimum, SpawnRangeMaximum);
                Vector2 spawnPosition = (Vector2)transform.position + spawnOffset;

                GameObject enemyToSpawn = GetRandomEnemy(); // Get a randomly selected enemy based on weights
                Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
            }
        }
    }

    private GameObject GetRandomEnemy()
    {
        // Create a weighted list
        int totalWeight = SquareEnemyWeight + CircleEnemyWeight + HexagonEnemyWeight;
        int randomValue = Random.Range(0, totalWeight);

        if (randomValue < SquareEnemyWeight)
        {
            return SquareEnemyPrefab; // More likely to spawn
        }
        else if (randomValue < SquareEnemyWeight + CircleEnemyWeight)
        {
            return CircleEnemyPrefab;
        }
        else
        {
            return HexagonEnemyPrefab;
        }
    }
}
