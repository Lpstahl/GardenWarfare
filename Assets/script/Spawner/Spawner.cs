using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnType
{
    Fixed,
    Random
}

public class Spawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    [SerializeField] private SpawnType spawnType = SpawnType.Fixed;
    [SerializeField] private int enemyCount = 10;
    [SerializeField] private GameObject testGO;

    [Header("Fixed Spawn")]
    [SerializeField] private float delayBtwSpawns;

    [Header("Random Spawn")]
    [SerializeField] private float minRandomDelay;
    [SerializeField] private float maxRandowDelay;

    private float _spawnTimer;
    private int _enemiesSpawned;

    // Update is called once per frame
    void Update()
    {
        _spawnTimer -= Time.deltaTime;
        if (_spawnTimer < 0)
        {
            _spawnTimer = GetSpawnDelay();
            if (_enemiesSpawned < enemyCount)
            {
                _enemiesSpawned++;
                SpawnEnemy();
            }
        }
    }

    private void SpawnEnemy()
    {
        Instantiate(testGO, transform.position, Quaternion.identity);
    }

    private float GetSpawnDelay()
    {
        float delay = 0;
        if (spawnType == SpawnType.Fixed)
        {
            delay = delayBtwSpawns;
        }
        else if (spawnType == SpawnType.Random)
        {
            delay = GetRandomDelay();
        }
        return delay;
    }

    private float GetRandomDelay()
    {
        float randomTimer = Random.Range(minRandomDelay, maxRandowDelay);
        return randomTimer;
    }
}
