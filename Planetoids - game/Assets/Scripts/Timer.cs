using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    float currentTime = 0f;
    public float startingTime = 10f;

    public Text timerText;

    private void Start()
    {
        currentTime = startingTime;
    }

    private void Update()
    {
        if (currentTime > 0)
        {
            currentTime = currentTime - 1 * Time.deltaTime;
        }
        
        timerText.text = currentTime.ToString("0");
    }
}
