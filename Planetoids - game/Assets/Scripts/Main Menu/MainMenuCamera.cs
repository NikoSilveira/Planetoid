using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCamera : MonoBehaviour
{
    [SerializeField] private Material[] skybox;

    private float rotateTime = 135f;    //Lower value - faster

    void Start()
    {
        RenderSettings.skybox = skybox[Random.Range(0,3)];
        LeanTween.rotateAround(gameObject, Vector3.up, 360, rotateTime).setLoopClamp();
    }
}
