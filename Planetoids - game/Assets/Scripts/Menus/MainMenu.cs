using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/*
 * Script for handling of main menu functionalities
 * Playerprefs: musicVol and sfxVol used to control audio
 */
public class MainMenu : MonoBehaviour
{
    //Level Selection
    public Button[] worldButtons;
    public Button[] levelButtons;

    //Volume control
    public AudioMixer audioMixer;
    public Slider musicSlider;
    public Slider sfxSlider;

    private void Start()
    {
        InitializeWorldButtons();
        InitializeLevelButtons();
        InitializeAudioSettings();
    }

    //-------------------
    //    Start Menu
    //-------------------

    public void Quit()
    {
        Application.Quit();
    }

    //--------------------
    //   Settings Menu
    //--------------------

    private void InitializeAudioSettings()
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

    //--------------------
    //     Level Menu
    //--------------------

    private void InitializeWorldButtons()
    {
        int worldReached = 5;   //read file

        for (int i = 0; i < worldButtons.Length; i++)
        {
            if (i + 1 > worldReached)
            {
                worldButtons[i].interactable = false;
                LeanTween.alpha(worldButtons[i].GetComponent<RectTransform>(), 90f, 0.01f);
            }
        }
    }

    private void InitializeLevelButtons()
    {
        int levelReached = 7;   //read file

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 > levelReached)
            {
                levelButtons[i].interactable = false;
                LeanTween.alpha(levelButtons[i].GetComponent<RectTransform>(), 90f, 0.01f);
            }
        }
    }

    //------------------------
    //   Customization Menu
    //------------------------
}
