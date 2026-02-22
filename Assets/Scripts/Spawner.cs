using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public Transform[] spawnPoints;
    public float spawnInterval = 2f;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnEnemy()
    {
        int randomLane = Random.Range(0, spawnPoints.Length);
        int randomEnemy = Random.Range(0, enemyPrefabs.Length);

    GameObject enemy = Instantiate(
    enemyPrefabs[randomEnemy],
    spawnPoints[randomLane].position,
    spawnPoints[randomLane].rotation
    );

    enemy.GetComponent<Enemy>().colorIndex = randomEnemy;
    }
}