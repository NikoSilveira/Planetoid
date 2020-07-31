using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Control of speedy/protective spawning conditions
 * -Assign to ObjectSpawner in hierarchy
 */
public class BoosterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefabTimey, prefabProtective;
    
    private float timer;
    private GameObject[] spawnPoint;
    private int numOfSpawnPoints;

    void Start()
    {
        timer = 10f;

        numOfSpawnPoints = 5; //Edit if num of spawn points changes

        //Spawn point initialization
        spawnPoint = new GameObject[numOfSpawnPoints];

        for (int i = 0; i < spawnPoint.Length; i++)
        {
            spawnPoint[i] = GameObject.Find("Spawner" + (i + 1).ToString());
        }
    }

    void Update()
    {
        timer = timer - 1 * Time.deltaTime;

        if(timer <= 0f)
        {
            SpawnBooster();
            timer = 40f;
        }
    }

    //--------------------
    //      SPAWNING
    //--------------------

    private void SpawnBooster()
    {
        //66% chance of spawn
        if(Random.Range(0,20) != 0)
        {
            //66% chance timey - 33% chance protective
            if(Random.Range(0,2) != 0)
            {
                Instantiate(prefabTimey, spawnPoint[Random.Range(0, numOfSpawnPoints)].transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(prefabProtective, spawnPoint[Random.Range(0, numOfSpawnPoints)].transform.position, Quaternion.identity);
            }
        }
    }
}
