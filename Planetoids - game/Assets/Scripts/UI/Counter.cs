using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Script to control the UI counter
 * -Control of vistory condition and scene transition
 */
public class Counter : MonoBehaviour
{
    public Text counterText;

    private int targetCount;
    private int counter;

    void Start()
    {
        counter = 0;
    }

    void Update()
    {
        counterText.text = counter.ToString() + "/" + targetCount.ToString();

        if (counter == targetCount)
        {
            FindObjectOfType<LevelManager>().Win();
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
