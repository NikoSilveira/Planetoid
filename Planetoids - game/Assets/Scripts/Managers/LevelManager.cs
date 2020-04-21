using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
 * THE MANAGER MUST BE ASSIGNED TO MAIN SCENE AND ALL LEVELS
 */
public class LevelManager : MonoBehaviour
{
    //UI GameObjects
    public GameObject victory;
    public GameObject defeat;
    public GameObject timeExpired;
    public GameObject pauseButton;

    //Unlocking levels
    [SerializeField] private bool unlocksWorld;
    [HideInInspector] public int levelsToUnlock;
    [HideInInspector] public int worldsToUnlock;

    //Unlocking customizations
    [HideInInspector] public int colorsToUnlock;


    private bool levelIsActive;

    private void Awake()
    {
        InitializeSaveData();
    }

    void Start()
    {
        levelIsActive = true;
    }

    //-----------
    //  METHODS
    //-----------

    public void Win()
    {
        if (!levelIsActive)
        {
            return;
        }

        levelIsActive = false;
        victory.SetActive(true);
        victory.GetComponent<Text>().text = RandomMessage();
        pauseButton.SetActive(false);
        Unlock();

        StartCoroutine(LoadMainMenu());
    }

    public void Lose()
    {
        if (!levelIsActive)
        {
            return;
        }

        FindObjectOfType<PlayerController>().PlayerDeath();
        levelIsActive = false;
        defeat.SetActive(true);
        pauseButton.SetActive(false);

        StartCoroutine(LoadMainMenu());
    }

    public void RunOutOfTime()
    {
        if (!levelIsActive)
        {
            return;
        }

        levelIsActive = false;
        timeExpired.SetActive(true);
        pauseButton.SetActive(false);

        StartCoroutine(LoadMainMenu());
    }

    //--------------------------
    //  COMPLIEMENTARY METHODS
    //--------------------------

    //Coroutine - load scene
    IEnumerator LoadMainMenu()
    {
        yield return new WaitForSeconds(1.5f);
        FindObjectOfType<LevelLoader>().LoadTargetLevel(0);
    }

    //Random message for WIN
    private string RandomMessage()
    {
        string[] message = {
            "VICTORY!",
            "A-BLAZE-ING!",
            "HEATED WIN!",
            "HOT TAKE!"
        };

        int random = Random.Range(0, message.Length);

        return message[random];
    }

    //-------------
    //  UNLOCKING
    //-------------

    private void Unlock()
    {
        int numberOfScenes = SceneManager.sceneCountInBuildSettings;
        int nextLevel = SceneManager.GetActiveScene().buildIndex - 1;
        GameData data = SaveSystem.LoadGame();

        worldsToUnlock = data.worldsToUnlock;
        colorsToUnlock = data.colorsToUnlock;

        //Unlock levels
        if (nextLevel > numberOfScenes) //Avoid overflow
        {
            return;
        }
        else
        {
            levelsToUnlock = nextLevel;
        }

        //Unlock worlds/colors
        if (unlocksWorld)
        {
            worldsToUnlock += 1;
            colorsToUnlock += 1;
        }

        SaveSystem.SaveGame(this);
    }

    //------------
    //   OTHERS
    //------------

    //Create document on awake (1st time)
    private void InitializeSaveData()
    {
        if(SaveSystem.LoadGame() == null)
        {
            levelsToUnlock = 1;
            worldsToUnlock = 1;
            colorsToUnlock = 3;

            SaveSystem.SaveGame(this);
        }
    }

    public bool GetLevelIsActive()
    {
        return levelIsActive;
    }
}

