using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    public float spawnInterval = 2f;

    void Start()
    {
        if (enemyPrefab == null || spawnPoint == null)
        {
            Debug.Log("prefab ou Spawn nao esta atribuido no GameManager.");
            return;
        }

        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (enemyPrefab != null && spawnPoint != null)
            {
                Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
