using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Control of target spawning conditions
 * -Assign to ObjectSpawner in hierarchy
 */
public class TargetSpawner : MonoBehaviour
{

    public GameObject prefab;
    private GameObject[] spawnPoint;

    void Start()
    {
        spawnPoint = new GameObject[3];

        for(int i=0; i<spawnPoint.Length; i++)
        {
            spawnPoint[i] = GameObject.Find("Spawner" + (i+1).ToString());
        }        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            SpawnTarget();
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
}
