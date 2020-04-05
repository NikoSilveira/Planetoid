using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/*
 * Script for controlling pause menu and pause mechanics
 */
public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;              //Assign panel under canvas to inspector

    //Volume control
    public AudioMixer audioMixer;
    public Slider musicSlider;
    public Slider sfxSlider;

    private void Start()
    {
        //Get initial volume values stores in playerprefs
        float initialMusic = PlayerPrefs.GetFloat("musicVol", 0.7f);
        float initialSFX = PlayerPrefs.GetFloat("sfxVol", 0.7f);

        //Set sliders with playerprefs
        musicSlider.SetValueWithoutNotify(initialMusic);
        sfxSlider.SetValueWithoutNotify(initialSFX);

        //Set volume
        audioMixer.SetFloat("musicVol", Mathf.Log10(initialMusic) * 20);
        audioMixer.SetFloat("sfxVol", Mathf.Log10(initialSFX) * 20);
    }

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

    public void QuitLevel()
    {
        Time.timeScale = 1f;
        FindObjectOfType<LevelLoader>().LoadTargetLevel(0);
    }

    //-----------------
    //  Settings Menu
    //-----------------

    public void SetVolumeMusic(float volume)
    {
        audioMixer.SetFloat("musicVol", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVol", volume);
    }

    public void SetVolumeSfx(float volume)
    {
        audioMixer.SetFloat("sfxVol", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("sfxVol", volume);
    }

    public void Mute()
    {
        //Feature for later
    }

}
