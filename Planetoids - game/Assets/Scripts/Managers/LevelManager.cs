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

    private bool levelIsActive;

    private void Awake()
    {
        InitializeSaveData();
    }

    void Start()
    {
        levelIsActive = true;
    }

    //METHODS

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

    //Unlocking levels/worlds
    private void Unlock()
    {
        int numberOfScenes = SceneManager.sceneCountInBuildSettings;
        int nextLevel = SceneManager.GetActiveScene().buildIndex - 1;
        GameData data = SaveSystem.LoadGame();

        if (nextLevel > numberOfScenes) //Avoid overflow
        {
            return;
        }

        levelsToUnlock = nextLevel;

        if (unlocksWorld)
        {
            worldsToUnlock = data.worldsToUnlock + 1;
        }
        else
        {
            worldsToUnlock = data.worldsToUnlock;
        }

        SaveSystem.SaveGame(this);
    }

    //Create document on awake (1st time)
    private void InitializeSaveData()
    {
        if(SaveSystem.LoadGame() == null)
        {
            levelsToUnlock = 1;
            worldsToUnlock = 1;
            SaveSystem.SaveGame(this);
        }
        else
        {
            return;
        }
    }

    public bool GetLevelIsActive()
    {
        return levelIsActive;
    }
}

