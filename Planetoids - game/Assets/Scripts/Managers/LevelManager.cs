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
    [SerializeField] private int currentLevel;
    [SerializeField] private int currentWorld;
    [SerializeField] private bool unlocksWorld;

    //Save system
    [HideInInspector] public int levelsToUnlock;
    [HideInInspector] public int worldsToUnlock;
    [HideInInspector] public int colorsToUnlock;
    //[HideInInspector] public int[] highScore;
    [HideInInspector] public List<int> highScore = new List<int>();

    private bool levelIsActive;

    private void Awake()
    {
        Application.targetFrameRate = 30;
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

        SetHighScore();
        Unlock();
        SaveSystem.SaveGame(this);

        StartCoroutine(LoadScene(0));
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

        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex));
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

        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex));
    }

    //--------------------------
    //  COMPLIEMENTARY METHODS
    //--------------------------

    //Coroutine - load scene
    IEnumerator LoadScene(int sceneIndex)
    {
        yield return new WaitForSeconds(1.5f);
        FindObjectOfType<LevelLoader>().LoadTargetLevel(sceneIndex);
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
        GameData data = SaveSystem.LoadGame();

        worldsToUnlock = data.worldsToUnlock;
        colorsToUnlock = data.colorsToUnlock;
        levelsToUnlock = data.levelsToUnlock;

        int numberOfLevels = SceneManager.sceneCountInBuildSettings - 2;

        //Unlock levels
        if (currentLevel == levelsToUnlock && levelsToUnlock < numberOfLevels)
        {
            //if current level is equal to last unlocked level and
            //the unlocked levels are below total available -> unlock
            levelsToUnlock += 1;
        }

        //Unlock worlds/colors
        if (unlocksWorld && currentWorld == worldsToUnlock)
        {
            //if bool is true and currentWorld is equal to last unlocked world
            worldsToUnlock += 1;
            colorsToUnlock += 1;
        }
    }

    //--------------
    //  HIGH SCORE
    //--------------

    private void SetHighScore()
    {
        GameData data = SaveSystem.LoadGame();
        highScore = data.highScore;

        int localHighScore = highScore[currentLevel - 1];
        int localScore = FindObjectOfType<Score>().GetScore();

        if (localScore > localHighScore)
        {
            highScore[currentLevel - 1] = localScore;
        }
    }

    //-----------------
    //   SAVE SYSTEM
    //-----------------

    //Create document on awake (1st time)
    private void InitializeSaveData()
    {
        if(SaveSystem.LoadGame() == null)
        {
            //Unlockables
            levelsToUnlock = 1;
            worldsToUnlock = 1;
            colorsToUnlock = 3;

            //High Score
            int numberOfLevels = SceneManager.sceneCountInBuildSettings - 2;
            for (int i = 0; i < numberOfLevels; i++)
            {
                highScore.Add(0);
            }

            SaveSystem.SaveGame(this);
        }
    }

    //Debugging - Reset document
    public void ClearForDebug()
    {
        //Unlockables
        levelsToUnlock = 1;
        worldsToUnlock = 1;
        colorsToUnlock = 3;
        
        /*//High Score
        int numberOfLevels = SceneManager.sceneCountInBuildSettings - 2;
        for (int i = 0; i < numberOfLevels; i++)
        {
            highScore.Add(0);
        }*/

        SaveSystem.SaveGame(this);
    }

    //-------------
    //   GETTERS
    //-------------

    public bool GetLevelIsActive()
    {
        return levelIsActive;
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }
}

