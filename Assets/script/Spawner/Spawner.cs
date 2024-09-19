using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnType // Define o tipo de spawn.
{
    Fixed,
    Random
}

public class Spawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    [SerializeField] private SpawnType spawnType = SpawnType.Fixed; // Define o tipo de spawn.
    [SerializeField] private int enemyCount = 10; // Define o número de inimigos.
    [SerializeField] private float delayBtwWaves = 1f; // Define o delay entre waves.

    [Header("Fixed Spawn")]
    [SerializeField] private float delayBtwSpawns; // Define o delay entre spawns.

    [Header("Random Spawn")]
    [SerializeField] private float minRandomDelay; // Define o mínimo de delay aleatório.
    [SerializeField] private float maxRandowDelay; // Define o máximo de delay aleatório.

    private float _spawnTimer; // Define o timer de spawn.
    private int _enemiesSpawned; // Define o número de inimigos spawnados.
    private int _enemiesRemaining; // Define o número de inimigos restantes.

    private ObjectPooler _pooler;// Define o pooler como o componente ObjectPooler.

    private Waypoint _waypoint; // Define o waypoint.

    private void Start()
    {
        _pooler = GetComponent<ObjectPooler>(); // Define o pooler como o componente ObjectPooler.  
        _waypoint = GetComponentInParent<Waypoint>(); // Define o waypoint como o componente Waypoint.

        _enemiesRemaining = enemyCount; // Define o número de inimigos restantes como o número de inimigos.
    }

    // Update is called once per frame
    void Update()
    {
        _spawnTimer -= Time.deltaTime; // Decrementa o timer de spawn.
        if (_spawnTimer < 0) // Se o timer de spawn for menor que 0,
        {
            _spawnTimer = GetSpawnDelay(); // Define o timer de spawn como o delay de spawn.
            if (_enemiesSpawned < enemyCount) // Se o número de inimigos spawnados for menor que o número de inimigos,
            {
                _enemiesSpawned++; // Incrementa o número de inimigos spawnados.
                SpawnEnemy(); // Spawna um inimigo.
            }
        }
    }

    private void SpawnEnemy()
    {
        GameObject newInstance = _pooler.GetInstanceFromPool(); // Instancia um novo GameObject.
        Enemy enemy = newInstance.GetComponent<Enemy>(); // Define o inimigo como o componente Enemy.
        enemy.Waypoint = _waypoint; // Define o waypoint do inimigo como o waypoint.
        enemy.ResetEnemy(); // Reseta o inimigo.

        enemy.transform.localPosition = transform.position; // Define a posição do inimigo como a posição do spawner.
        newInstance.SetActive(true); // Ativa o GameObject.
    }

    private float GetSpawnDelay()
    {
        float delay = 0; // Define o delay como 0.
        if (spawnType == SpawnType.Fixed) // Se o tipo de spawn for fixo,
        {
            delay = delayBtwSpawns; // Define o delay como o delay entre spawns.
        }
        else // Se o tipo de spawn for aleatório,
        {
            delay = GetRandomDelay(); // Define o delay como um delay aleatório.
        }
        return delay; // Retorna o delay.
    }

    private float GetRandomDelay()
    {
        float randomTimer = Random.Range(minRandomDelay, maxRandowDelay); // Define o timer aleatório como um valor aleatório entre o mínimo e o máximo.
        return randomTimer; // Retorna o timer aleatório.
    }

    private IEnumerator StartNextWave()
    {
        yield return new WaitForSeconds(delayBtwWaves); // Aguarda o delay entre waves.
        _enemiesRemaining = enemyCount; // Define o número de inimigos restantes como o número de inimigos.
        _spawnTimer = 0f; // Define o timer de spawn como 0.
        _enemiesSpawned = 0; // Define o número de inimigos spawnados como 0.
    }
    private void RecordEnemyEndReached()
    {
        _enemiesRemaining--; // Decrementa o número de inimigos restantes.
        if (_enemiesRemaining <= 0) // Se o número de inimigos restantes for menor ou igual a 0,
        {
            StartCoroutine(StartNextWave()); // Inicia a próxima wave.
        }
    }
    
    private void OnEnable()
    {
        Enemy.OnEndReached += RecordEnemyEndReached; // Adiciona o evento de fim de caminho.
    }

    private void OnDisable()
    {
        Enemy.OnEndReached -= RecordEnemyEndReached; // Remove o evento de fim de caminho.
    }
}
