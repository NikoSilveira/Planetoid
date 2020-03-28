using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Control of enemy spawning conditions
 * -Assign to ObjectSpawner in hierarchy
 */
public class EnemySpawner : MonoBehaviour
{

    public GameObject prefab;
    private GameObject[] spawnPoint;

    private void Start()
    {
        spawnPoint = new GameObject[3];

        for(int i=0; i<spawnPoint.Length; i++)
        {
            spawnPoint[i] = GameObject.Find("Spawner" + (i+1).ToString());
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnEnemy();
        }
    }

    //Function to create an enemy
    public void SpawnEnemy()
    {
        if(prefab == null)
        {
            Debug.LogWarning("Prefab not found in EnemySpawner script: SpawnEnemy function");
            return;
        }

        int randomSpawn = Random.Range(0, 3);
        Instantiate(prefab, spawnPoint[randomSpawn].transform.position, Quaternion.identity);
    }
}
