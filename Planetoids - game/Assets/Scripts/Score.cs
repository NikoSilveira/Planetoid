using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Script to handle score UI and logic
 * -Use setter to modify score
 * -Use getter to obtain score
 */

public class Score : MonoBehaviour
{

    public Text scoreText;
    private int score;

    private void Start()
    {
        score = 0;
    }

    void Update()
    {
        scoreText.text = score.ToString();
    }

    //Setter to be called to increase score
    public void setScore(int scoreToAdd)
    {
        score = score + scoreToAdd;
    }

    public int getScore()
    {
        return score;
    }
}
