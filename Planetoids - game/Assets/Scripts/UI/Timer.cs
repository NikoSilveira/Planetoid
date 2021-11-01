using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private float startingTime;

    private float currentTime = 0f;
    public Text timerText;
    public Text bonusTimerText;

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

        TimerSFX();
        timerText.text = currentTime.ToString("0");
    }

    public float GetCurrentTime()
    {
        return currentTime;
    }

    public void SetCurrentTime(float extraTime)
    {
        currentTime += extraTime;
        bonusTimerText.text = "+" + extraTime + "s";
        LeanTween.alphaText(bonusTimerText.GetComponent<RectTransform>(), 1f, 1.25f).setLoopPingPong(1);

        FindObjectOfType<AudioManager>().Play("Timey");
    }

    //Running out - SFX
    private void TimerSFX()
    {
        float[] soundInstant = {50f,10f,9f,8f,7f,6f,5f,4f,3f,2f,1f};

        for (int i = 0; i < soundInstant.Length; i++)
        {
            if(currentTime > soundInstant[i] && currentTime < soundInstant[i] + 0.05f)
            {
                FindObjectOfType<AudioManager>().Play("Time");
            }
        }
    }
}
