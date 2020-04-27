using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private float startingTime;

    private float currentTime = 0f;
    public Text timerText;
    public Text framerate;

    private void Start()
    {
        currentTime = startingTime;
    }

    private void Update()
    {
        if (currentTime > 0)
        {
            if (FindObjectOfType<LevelManager>().GetLevelIsActive())
            {
                currentTime = currentTime - 1 * Time.deltaTime;
            }
        }
        else
        {
            FindObjectOfType<LevelManager>().RunOutOfTime();
        }
        
        timerText.text = currentTime.ToString("0");
        framerate.text = (1.0f / Time.deltaTime).ToString("0");
    }

    public float GetCurrentTime()
    {
        return currentTime;
    }
}
