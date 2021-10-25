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
        timer = 20f; //Initially, attempt spawn after x seconds (20)

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
            timer = 30f; //Next spawn attempts come each y seconds (30)
        }
    }

    //--------------------
    //      SPAWNING
    //--------------------

    private void SpawnBooster()
    {
        //66% chance of spawning a booster
        if (Random.Range(0,3) != 0)
        {
            //33% chance timey - 33% chance protective - 33% chance speedy
            int randombooster = Random.Range(0, 2);

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
