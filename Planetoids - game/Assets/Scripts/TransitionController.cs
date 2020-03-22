using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionController : MonoBehaviour
{

    private bool collisionControl;
    public GameObject victory;

    void Start()
    {
        collisionControl = true;
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collisionControl)
        {
            victory.SetActive(true);
            Invoke("load", 2.0f);

            //collisionControl = false;
            
            //FindObjectOfType<LevelChanger>().FadeToLevel();
        }
        
    }

    private void load()
    {
        SceneManager.LoadScene(0);
    }
}
