using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Script to control the UI counter and boss health bar
 * -Control of victory condition and scene transition
 */
public class Counter : MonoBehaviour
{
    public Text counterText;
    public Slider bossBar;

    private int targetCount;
    private int counter;

    void Start()
    {
        counter = 0;
    }

    void Update()
    {
        counterText.text = counter.ToString() + "/" + targetCount.ToString();
        bossBar.maxValue = targetCount;
        bossBar.value = targetCount - counter;

        if (counter == targetCount)
        {
            if (bossBar.IsActive()){
                FindObjectOfType<LevelManager>().WinBoss(); //Detect boss battle has been won
                bossBar.enabled = false;
            }
            else{
                FindObjectOfType<LevelManager>().Win(); //Detect normal win condition
            }
        }
    }

    //---------------------
    //  GETTERS & SETTERS
    //---------------------

    public void SetCounter()
    {
        counter = counter + 1;
    }

    public int GetCounter()
    {
        return counter;
    }

    public void SetTargetCount(int newTargetCount)
    {
        targetCount = newTargetCount;
    }

}    
