using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TargetCollision : MonoBehaviour
{

    public GameObject victory;
    private bool singleCollision;

    void Start()
    {
        singleCollision = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (singleCollision)
        {
            singleCollision = false;
            victory.SetActive(true);
            StartCoroutine(LoadLevel());
        }
    }

    IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(1.5f);
        FindObjectOfType<LevelLoader>().LoadTargetLevel(0);
    }
}
