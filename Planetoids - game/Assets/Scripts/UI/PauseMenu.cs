using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Script for controlling pause menu and pause mechanics
 */
public class PauseMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;  //Assign panel under canvas to inspector

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadSettings()
    {
        //Code in settings menu display here
    }

    public void QuitLevel()
    {
        //Modify later - give player inmunity for the 1s the world will be moving
        Time.timeScale = 1f;
        FindObjectOfType<LevelLoader>().LoadTargetLevel(0);
    }

}
