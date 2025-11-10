using System.Collections;
using UnityEngine;

public class SpawnManager_sc : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject enemyContainer;

    [SerializeField] private GameObject healthItemPrefab;
    [SerializeField] private float spawnInterval = 3.0f;

    [SerializeField] private bool stopSpawning = false;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }
    public void OnPlayerDeath()
    {
        stopSpawning = true;
    }

    public void StopSpawning()
    {
        stopSpawning = true;
    }

    IEnumerator SpawnRoutine()
    {
        while (stopSpawning == false)
        {
            Vector3 position = new Vector3(Random.Range(-9.5f, 9.5f), 7.4f, 0);
            GameObject enemy = Instantiate(enemyPrefab, position, Quaternion.identity);
            enemy.transform.parent = enemyContainer.transform;

            if (healthItemPrefab != null && Random.value < 0.25f)
            {
                Vector3 healthPos = new Vector3(Random.Range(-9.5f, 9.5f), 7.4f, 0);
                Instantiate(healthItemPrefab, healthPos, Quaternion.identity);
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
