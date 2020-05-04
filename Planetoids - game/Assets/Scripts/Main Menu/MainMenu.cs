using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Panels
    public GameObject[] panels;

    //Buttons
    public Button[] worldButtons;
    public Button[] levelButtons;
    public Button[] customButtons;

    public Text[] highScoreText;

    //Audio Settings
    public AudioMixer audioMixer;
    public Slider musicSlider;
    public Slider sfxSlider;
    public Text muteText;
    
    public Text joystickText;

    //Customization color
    [HideInInspector] public int customButtonIndex;

    private void Awake()
    {
        InitializePlayerData();
    }

    private void Start()
    {
        InitializePanels();
        InitializeWorldButtons();
        InitializeLevelButtons();
        InitializeCustomButtons();
        InitializeAudioSettings();
        InitializeJoystickSettings();
    }

    //-----------------
    //  Panel Control
    //-----------------

    private void InitializePanels()
    {
        int panelControlValue = PlayerPrefs.GetInt("PanelControl", 0);

        if (panelControlValue != 0)
        {
            panels[0].SetActive(false);
            panels[panelControlValue].SetActive(true);
            PlayerPrefs.SetInt("PanelControl",0);
        }
    }

    public void SetCurrentWorldPanel(int world) //Assign to world buttons
    {
        PlayerPrefs.SetInt("PanelControl", world);
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnBeforeSceneLoadRuntimeMethod()
    {
        PlayerPrefs.SetInt("PanelControl", 0);
    }

    //-------------------
    //    Start Menu
    //-------------------

    public void Quit()
    {
        FindObjectOfType<PopUpWindow>().OpenWindow();
    }

    //--------------------
    //   Settings Menu
    //--------------------

    //---------- Audio ----------

    private void InitializeAudioSettings()
    {
        float initialMusic = PlayerPrefs.GetFloat("musicVol", 0.7f);
        float initialSFX = PlayerPrefs.GetFloat("sfxVol", 0.7f);

        musicSlider.SetValueWithoutNotify(initialMusic);
        sfxSlider.SetValueWithoutNotify(initialSFX);

        audioMixer.SetFloat("musicVol", Mathf.Log10(initialMusic) * 20);
        audioMixer.SetFloat("sfxVol", Mathf.Log10(initialSFX) * 20);

        //Stored audio listener value (muting)
        AudioListener.volume = PlayerPrefs.GetFloat("listenerVolume", 1f);

        if(AudioListener.volume == 0f)
            muteText.text = "Unmute";
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

    //--------- Joystick ---------

    private void InitializeJoystickSettings()
    {
        int sideValue = PlayerPrefs.GetInt("JoystickSide", 0);

        if(sideValue == 1)
            joystickText.GetComponent<Text>().text = "Joystick: L";
    }

    public void ChangeJoystickSide()
    {
        int sideValue = PlayerPrefs.GetInt("JoystickSide", 0);

        if (sideValue == 0)         //Move left
        {
            PlayerPrefs.SetInt("JoystickSide", 1);
            joystickText.GetComponent<Text>().text = "Joystick: L";
        }
        else if(sideValue == 1)     //Move right
        {
            PlayerPrefs.SetInt("JoystickSide", 0);
            joystickText.GetComponent<Text>().text = "Joystick: R";
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
            highScoreText[i].GetComponent<Text>().text = "HS: " + data.highScore[i].ToString();
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

    public void SetColor(int buttonIndex)   //start from 0
    {
        //Deselect previous button
        PlayerData data = SaveSystem.LoadPlayer();
        customButtons[data.customButtonIndex].GetComponent<Image>().color = new Color32(199, 236, 238, 255);

        //Save new selection
        customButtonIndex = buttonIndex;
        SaveSystem.SavePlayer(this);    
        customButtons[buttonIndex].GetComponent<Image>().color = new Color32(255, 190, 118, 255);
    }
    
    //--------------
    //    AUDIO
    //--------------

    public void ButtonSFX()
    {
        FindObjectOfType<AudioManager>().Play("Button");
    }
}
