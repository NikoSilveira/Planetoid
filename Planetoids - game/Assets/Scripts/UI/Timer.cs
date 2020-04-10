using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Script to control the game timer
 */

public class Timer : MonoBehaviour
{
    private float currentTime = 0f;

    [SerializeField] private float startingTime;

    public Text timerText;
    public GameObject timeExpired;//here

    private void Start()
    {
        currentTime = startingTime;
    }

    private void Update()
    {
        if (currentTime > 0)
        {
            if (FindObjectOfType<PlayerController>().GetLevelIsActive())//here
            {
                currentTime = currentTime - 1 * Time.deltaTime;
            }
        }
        else
        {
            FindObjectOfType<PlayerController>().SetLevelIsActive(false);//here
            timeExpired.SetActive(true);//here
            StartCoroutine(LoadLevel());//here
        }
        
        timerText.text = currentTime.ToString("0");
    }

    IEnumerator LoadLevel()//here
    {
        yield return new WaitForSeconds(1.5f);
        FindObjectOfType<LevelLoader>().LoadTargetLevel(1);
    }
}
