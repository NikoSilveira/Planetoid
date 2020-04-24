﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/*
 * Script for handling of main menu functionalities
 * Playerprefs: musicVol and sfxVol used to control audio; listener volume for muting; custom for selected customization
 */
public class MainMenu : MonoBehaviour
{
    //Buttons
    public Button[] worldButtons;
    public Button[] levelButtons;
    public Button[] customButtons;

    //Volume control
    public AudioMixer audioMixer;
    public Slider musicSlider;
    public Slider sfxSlider;
    public Text muteText;

    //Customization color
    [HideInInspector] public int customButtonIndex;

    private void Awake()
    {
        InitializePlayerData();
    }

    private void Start()
    {
        InitializeWorldButtons();
        InitializeLevelButtons();
        InitializeCustomButtons();
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

        //Stored audio listner value (muting)
        AudioListener.volume = PlayerPrefs.GetFloat("listenerVolume", 1f);

        if(AudioListener.volume == 0f)
        {
            muteText.text = "Unmute";
        }
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
        if(AudioListener.volume == 0f)
        {
            //Unmute
            AudioListener.volume = 1f;
            PlayerPrefs.SetFloat("listenerVolume", 1f);
            muteText.text = "Mute";
        }
        else
        {
            //Mute
            AudioListener.volume = 0f;
            PlayerPrefs.SetFloat("listenerVolume", 0f);
            muteText.text = "Unmute";
        }
    }

    //--------------------
    //     Level Menu
    //--------------------

    private void InitializeWorldButtons()
    {
        //Read from file
        GameData data = SaveSystem.LoadGame();
        int worldReached = data.worldsToUnlock;

        for (int i = 0; i < worldButtons.Length; i++)
        {
            if (i + 1 > worldReached)
            {
                worldButtons[i].interactable = false;
                LeanTween.alpha(worldButtons[i].GetComponent<RectTransform>(), 90f, 0.01f);
            }
        }

        worldButtons[3].interactable = false;
        LeanTween.alpha(worldButtons[3].GetComponent<RectTransform>(), 90f, 0.01f);
    }

    private void InitializeLevelButtons()
    {
        //Read from file
        GameData data = SaveSystem.LoadGame();
        int levelReached = data.levelsToUnlock;

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


    private void InitializeCustomButtons()
    {
        GameData gameData = SaveSystem.LoadGame();
        int colorReached = gameData.colorsToUnlock;

        for (int i = 0; i < customButtons.Length; i++)
        {
            if(i + 1 > colorReached)
            {
                customButtons[i].interactable = false;
                LeanTween.alpha(customButtons[i].GetComponent<RectTransform>(), 90f, 0.01f);
            }
        }

        //Set color of selected custom on loadscene
        PlayerData playerData = SaveSystem.LoadPlayer();
        customButtons[playerData.customButtonIndex].GetComponent<Image>().color = new Color32(255,190,118,255);
    }

    private void InitializePlayerData()
    {
        if (SaveSystem.LoadPlayer() == null)
        {
            customButtonIndex = 0;
            SaveSystem.SavePlayer(this);
        }
    }

    public void SetColor(int buttonIndex)
    {
        //Deselect previous button
        PlayerData data = SaveSystem.LoadPlayer();
        customButtons[data.customButtonIndex].GetComponent<Image>().color = new Color32(199, 236, 238, 255);

        //Save new selection
        customButtonIndex = buttonIndex;
        SaveSystem.SavePlayer(this);    
        customButtons[buttonIndex].GetComponent<Image>().color = new Color32(255, 190, 118, 255);
    }
    
}