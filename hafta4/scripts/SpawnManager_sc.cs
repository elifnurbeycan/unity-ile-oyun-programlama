using UnityEngine;
using System.Collections;

public class SpawnManager_sc : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private GameObject enemyContainer;

    [SerializeField]
    bool stopSpawning = false;


    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    void Update()
    {

    }

    IEnumerator SpawnRoutine()
    {
        while (stopSpawning == false)
        {
            Vector3 position = new Vector3(Random.Range(-9.5f, 9.5f),
                                                        7.4f,
                                                        0);
            GameObject enemy = Instantiate(enemyPrefab, position, Quaternion.identity);
            enemy.transform.parent = enemyContainer.transform;
            yield return new WaitForSeconds(3.0f);
        }
    }
    
    public void OnPlayerDeath()
    {
        stopSpawning = true;
    }
}
