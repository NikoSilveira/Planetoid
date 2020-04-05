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


    //-------------------
    //    Start Menu
    //-------------------

    public void Play()
    {
        FindObjectOfType<LevelLoader>().LoadTargetLevel(1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    //--------------------
    //   Settings Menu
    //--------------------

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

    //------------------------
    //   Customization Menu
    //------------------------
}
