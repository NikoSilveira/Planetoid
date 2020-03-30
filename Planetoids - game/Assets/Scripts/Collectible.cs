using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{

    private float rotateSpeed = 8f;

    private void Start()
    {
        LeanTween.rotateAround(gameObject, Vector3.right, 360, rotateSpeed).setLoopClamp();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player")
        {
            Destroy(gameObject);
            FindObjectOfType<Score>().setScore(100, false);
        }
    }
}
