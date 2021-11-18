using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Control of speedy/protective spawning conditions
 * -Assign to ObjectSpawner in hierarchy
 */
public class BoosterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefabTimey, prefabProtective, prefabSpeedy;
    
    private float timer;
    private GameObject[] spawnPoint;
    private int numOfSpawnPoints;

    void Start()
    {
        timer = 10f; //Initially, attempt spawn after x seconds (12)

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
            timer = 15f; //Next spawn attempts come each x seconds (15)
        }
    }

    //--------------------
    //      SPAWNING
    //--------------------

    private void SpawnBooster()
    {
        //50% chance of spawning a booster
        if (Random.Range(0,2) != 0)     //[inclusive, exclusive]
        {
            //33% chance timey - 33% chance protective - 33% chance speedy
            int randombooster = Random.Range(0, 3);     //[inclusive, exclusive]

            if (randombooster == 0)
            {
                Instantiate(prefabTimey, spawnPoint[Random.Range(0, numOfSpawnPoints)].transform.position, Quaternion.identity);
            }
            else if (randombooster == 1)
            {
                Instantiate(prefabProtective, spawnPoint[Random.Range(0, numOfSpawnPoints)].transform.position, Quaternion.identity);
            }
            else if (randombooster == 2)
            {
                Instantiate(prefabSpeedy, spawnPoint[Random.Range(0, numOfSpawnPoints)].transform.position, Quaternion.identity);
            }
        }
    }
}
