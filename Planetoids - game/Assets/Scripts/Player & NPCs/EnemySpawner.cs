using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Control of enemy spawning conditions
 * -Assign to ObjectSpawner in hierarchy
 */
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private float timer;
    [SerializeField] private int initialAmount;

    private float timerAux;
    private GameObject[] spawnPoint;

    private void Start()
    {
        timerAux = timer;

        //Spawn point initialization
        spawnPoint = new GameObject[3];

        for(int i=0; i<spawnPoint.Length; i++)
        {
            spawnPoint[i] = GameObject.Find("Spawner" + (i+1).ToString());
        }

        //Initial spawn
        SpawnEnemy(initialAmount);
    }

    private void Update()
    {
        //Spawn enemies as game progresses
        timer = timer - 1 * Time.deltaTime;

        if(timer <= 0f)
        {
            SpawnEnemy(1);
            timer = timerAux;
        }
    }

    //------------------
    //     SPAWNING
    //------------------

    public void SpawnEnemy(int numToSpawn)
    {
        if(prefab == null)
        {
            Debug.LogWarning("Prefab not found in EnemySpawner script: SpawnEnemy function");
            return;
        }

        StartCoroutine(SpawnDelay(numToSpawn));
    }

    IEnumerator SpawnDelay(int numToSpawn)
    {
        for (int i = 0; i < numToSpawn; i++)
        {
            Instantiate(prefab, spawnPoint[Random.Range(0, 3)].transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
    }

}
