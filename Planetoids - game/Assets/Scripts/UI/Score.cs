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
    private bool comboEnd;

    private void Start()
    {
        comboMultiplier = 1;
        comboTimer = 0f;
        comboEnd = false;
        score = 0;
    }

    void Update()
    {
        //Combo timer
        if(comboTimer > 0f)
        {
            comboTimer = comboTimer - 1 * Time.deltaTime;
        }
        else if(comboEnd)
        {
            comboMultiplier = 1;
            comboEnd = false;
        }

        scoreText.text = score.ToString();
    }

    public void SetScore(int scoreToAdd, bool isCombo)
    {
        if (isCombo)
        {
            StartCoroutine(ComboAnim());
            scoreToAdd = scoreToAdd * comboMultiplier;
            comboMultiplier++;
            comboEnd = true;
            comboTimer = 10f;   //Combo time limit
        }

        score = score + scoreToAdd;
    }

    IEnumerator ComboAnim()
    {
        comboText.GetComponent<Text>().text = comboMultiplier.ToString() + "°";

        LeanTween.alphaText(comboText.GetComponent<RectTransform>(), 1f, 1.5f);
        yield return new WaitForSeconds(1.5f);
        LeanTween.alphaText(comboText.GetComponent<RectTransform>(), 0f, 1.5f);
    }

    public int GetScore()
    {
        return score;
    }
}
