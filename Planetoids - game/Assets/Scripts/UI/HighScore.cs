using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    private Text highScoreText;

    void Start()
    {
        GameData data = SaveSystem.LoadGame();

        int currentLevel = FindObjectOfType<LevelManager>().GetCurrentLevel();
        int highScore = data.highScore[currentLevel - 1];

        highScoreText = gameObject.GetComponent<Text>();
        highScoreText.text = "High Score: " + highScore.ToString();
    }
}
