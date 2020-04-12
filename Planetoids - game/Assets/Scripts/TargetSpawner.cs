using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int[] hordeSize;

    private GameObject[] spawnPoint;

    private int counterAcum;
    private int spawnIndex;
    
    void Start()
    {
        counterAcum = 0;
        spawnIndex = 0;

        //Send target total to the counter
        int totalHordeSize = 0;

        foreach (int item in hordeSize)
        {
            totalHordeSize += item;
        }
        FindObjectOfType<Counter>().SetTargetCount(totalHordeSize);

        //Spawn point initialization
        spawnPoint = new GameObject[3];

        for(int i=0; i<spawnPoint.Length; i++)
        {
            spawnPoint[i] = GameObject.Find("Spawner" + (i+1).ToString());
        }

        //Initial spawn
        SpawnTarget(hordeSize[0]);
    }

    void Update()
    {
        AutoSpawn();
    }

    //------------------
    //     SPAWNING
    //------------------

    private void AutoSpawn()
    {
        //Spawn hordes as game progresses (if all spawned have been eaten and there is no risk of overflow)
        if (FindObjectOfType<Counter>().GetCounter() == (hordeSize[spawnIndex] + counterAcum) && (spawnIndex + 1) < hordeSize.Length)
        {
            counterAcum += hordeSize[spawnIndex];
            spawnIndex++;
            SpawnTarget(hordeSize[spawnIndex]);
        }
    }

    public void SpawnTarget(int numToSpawn)
    {
        if(prefab == null)
        {
            Debug.LogWarning("Prefab not found in TargetSpawner script: SpawnTarget function");
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
