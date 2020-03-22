using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TargetCollision : MonoBehaviour
{

    public GameObject victory;

    void Start()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        victory.SetActive(true);
        StartCoroutine(loadLevel());
    }

    IEnumerator loadLevel()
    {
        yield return new WaitForSeconds(2.0f);
        FindObjectOfType<LevelLoader>().loadTargetLevel(0);
    }
}
