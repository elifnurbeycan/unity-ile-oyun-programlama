using UnityEngine;
using System.Collections;

public class SpawnManager_sc : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }
    void Update()
    {

    }
    
    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            Vector3 position = new Vector3(Random.Range(-9.5f, 9.5f),
                                                        7.4f,
                                                        0);

            GameObject enemy = Instantiate(enemyPrefab, position, Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
    }
}
