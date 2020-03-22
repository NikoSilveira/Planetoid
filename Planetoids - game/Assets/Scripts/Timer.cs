using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{

    float currentTime = 0f;
    public float startingTime = 10f;

    private void Start()
    {
        currentTime = startingTime;
    }

    private void Update()
    {
        currentTime = currentTime - 1 * Time.deltaTime;
        print(currentTime);
    }
}
