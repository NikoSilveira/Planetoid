using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCamera : MonoBehaviour
{

    private float rotateSpeed = 150f;    //Lower value - faster

    void Start()
    {
        LeanTween.rotateAround(gameObject, Vector3.up, 360, rotateSpeed).setLoopClamp();
    }
}
