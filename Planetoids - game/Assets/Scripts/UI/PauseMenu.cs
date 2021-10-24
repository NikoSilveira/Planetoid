using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;              //Assign panel under canvas to inspector

    //Volume control
    public AudioMixer audioMixer;
    public Slider musicSlider;
    public Slider sfxSlider;
    public Text muteText;

    //Joystick side
    public Text joystickText;

    private void Start()
    {
        InitializeAudioSettings();
        InitializeJoystick();
    }

    //--------------
    //  Pause Menu
    //--------------

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

    public void Restart()
    {
        Time.timeScale = 1f;
        FindObjectOfType<LevelLoader>().LoadTargetLevel(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitLevel()
    {
        Time.timeScale = 1f;
        FindObjectOfType<LevelLoader>().LoadTargetLevel(0);
    }

    //-----------------
    //  Settings Menu
    //-----------------

    //Audio------

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

        //Stored audio listener value (muting)
        AudioListener.volume = PlayerPrefs.GetFloat("listenerVolume", 1f);

        if (AudioListener.volume == 0f)
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
        if (AudioListener.volume == 0f)
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

    //Joystick------

    public void ChangeJoystickSide()
    {
        FindObjectOfType<FixedJoystick>().ChangeJoystickSide(joystickText);
    }

    private void InitializeJoystick()
    {
        FindObjectOfType<FixedJoystick>().InitializeJoystick(joystickText);
    }

}
