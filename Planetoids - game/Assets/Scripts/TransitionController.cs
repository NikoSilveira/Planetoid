using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionController : MonoBehaviour
{

    private bool collisionControl;
    // Start is called before the first frame update
    void Start()
    {
        collisionControl = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collisionControl)
        {
            //collisionControl = false;
            SceneManager.LoadScene(0);
        }
        
    }
}
