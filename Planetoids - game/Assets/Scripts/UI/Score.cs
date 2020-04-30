using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;
    public GameObject comboText;
    private int score;

    //Combo
    private int comboMultiplier;
    private float comboTimer;
    private bool comboActive;

    private void Start()
    {
        comboMultiplier = 1;
        comboTimer = 0f;
        comboActive = false;
        score = 0;
    }

    void Update()
    {
        //Combo timer
        if(comboTimer > 0f)
        {
            comboTimer = comboTimer - 1 * Time.deltaTime;
        }
        else if(comboActive)
        {
            comboMultiplier = 1;
            comboActive = false;
            LeanTween.alphaText(comboText.GetComponent<RectTransform>(), 0f, 0.5f); //fade out
        }

        scoreText.text = score.ToString();
    }

    public void SetScore(int scoreToAdd, bool isCombo)
    {
        if (isCombo)
        {
            //Animation and SFX
            comboText.GetComponent<Text>().text = comboMultiplier.ToString() + "X";
            LeanTween.alphaText(comboText.GetComponent<RectTransform>(), 1f, 3f).setLoopPingPong(1);   //fade in
            FindObjectOfType<AudioManager>().Play("combo" + comboMultiplier);

            scoreToAdd = scoreToAdd * comboMultiplier;
            comboActive = true;
            comboTimer = 6f;        //Combo time limit

            if (comboMultiplier < 8)
            {
                comboMultiplier++;
            }
        }

        score = score + scoreToAdd;
    }

    public int GetScore()
    {
        return score;
    }
}
