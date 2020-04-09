using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Control of target spawning conditions
 * -Assign to ObjectSpawner in hierarchy
 */
public class TargetSpawner : MonoBehaviour
{
    [SerializeField] public GameObject prefab;

    private GameObject[] spawnPoint;
    private bool spawnControl1, spawnControl2;

    private int[] horde;
    private bool[] hordeControl;

    void Start()
    {
        spawnControl1 = true;
        spawnControl2 = true;

        spawnPoint = new GameObject[3];

        for(int i=0; i<spawnPoint.Length; i++)
        {
            spawnPoint[i] = GameObject.Find("Spawner" + (i+1).ToString());
        }

        SpawnTarget(3);    //Spawn 3 at start
    }

    void Update()
    {
        if (FindObjectOfType<Counter>().getCounter() == 3 && spawnControl1 == true) 
        {
            //Spawn 4 after first 3
            SpawnTarget(4);
            spawnControl1 = false;
        }
        else if (FindObjectOfType<Counter>().getCounter() == 7 && spawnControl2 == true)
        {
            //Spawn the rest
            int numToSpawn = FindObjectOfType<Counter>().getTargetCount() - FindObjectOfType<Counter>().getCounter();

            SpawnTarget(numToSpawn);
            spawnControl2 = false;
        }
    }

    //------------------
    //     SPAWNING
    //------------------

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
