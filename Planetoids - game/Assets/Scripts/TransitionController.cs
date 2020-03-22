using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionController : MonoBehaviour
{

    public GameObject victory;

    void Start()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        victory.SetActive(true);
        Invoke("load", 2.0f);
    }

    private void load()
    {
        FindObjectOfType<LevelLoader>().loadTargetLevel(0);
    }
}
