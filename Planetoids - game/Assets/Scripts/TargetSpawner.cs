using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Control of target spawning conditions
 * -Assign to ObjectSpawner in hierarchy
 */
public class TargetSpawner : MonoBehaviour
{

    public GameObject prefab;       //Assign in inspector
    private GameObject[] spawnPoint;
    private bool spawnControl1, spawnControl2;

    void Start()
    {
        spawnControl1 = true;
        spawnControl2 = true;

        spawnPoint = new GameObject[3];

        for(int i=0; i<spawnPoint.Length; i++)
        {
            spawnPoint[i] = GameObject.Find("Spawner" + (i+1).ToString());
        }

        StartCoroutine(SpawnControl(3));    //Spawn 3 at start
    }

    void Update()
    {
        if (FindObjectOfType<Counter>().getCounter() == 3 && spawnControl1 == true) 
        {
            //Spawn 4 after first 3
            StartCoroutine(SpawnControl(4));
            spawnControl1 = false;
        }
        else if (FindObjectOfType<Counter>().getCounter() == 7 && spawnControl2 == true)
        {
            //Spawn the rest
            int numToSpawn = FindObjectOfType<Counter>().getTargetCount() - FindObjectOfType<Counter>().getCounter();

            StartCoroutine(SpawnControl(numToSpawn));
            spawnControl2 = false;
        }
    }

    //Function to create a target
    public void SpawnTarget()
    {
        if(prefab == null)
        {
            Debug.LogWarning("Prefab not found in TargetSpawner script: SpawnTarget function");
            return;
        }

        int randomSpawn = Random.Range(0,3);
        Instantiate(prefab, spawnPoint[randomSpawn].transform.position, Quaternion.identity);
    }

    //Function to control spawn mechanics
    IEnumerator SpawnControl(int numToSpawn)
    {
        for (int i = 0; i < numToSpawn; i++)
        {
            SpawnTarget();
            yield return new WaitForSeconds(0.5f);
        }
    }
}
