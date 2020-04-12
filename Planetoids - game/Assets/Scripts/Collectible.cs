using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{

    private float rotateSpeed = 8f; //Lower value - faster

    private void Start()
    {
        LeanTween.rotateAround(gameObject, Vector3.right, 360, rotateSpeed).setLoopClamp();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player" && FindObjectOfType<LevelManager>().GetLevelIsActive())
        {
            Destroy(gameObject);
            FindObjectOfType<Score>().SetScore(100, false);
        }
    }
}
