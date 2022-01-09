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
    public GameObject unlockedCustom;

    //Unlocking levels and elements
    [SerializeField] private int currentLevel;
    [SerializeField] private int currentWorld;
    [SerializeField] private bool unlocksWorld;
    [SerializeField] private bool isBossLevel;
    //[SerializeField] private int minimumScore;

    //Save system
    [HideInInspector] public int levelsToUnlock;
    [HideInInspector] public int worldsToUnlock;
    [HideInInspector] public int colorsToUnlock;
    [HideInInspector] public List<int> highScore = new List<int>();

    //Boss elements
    [SerializeField] private new GameObject camera;
    [SerializeField] private GameObject bossFlameParticles;

    private bool levelIsActive;
    [SerializeField] private bool isInfinite;

    private void Awake()
    {
        Application.targetFrameRate = 30;
        InitializeSaveData();
    }

    void Start()
    {
        Application.targetFrameRate = 30;
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

    public void WinBoss()
    {
        if (!levelIsActive)
        {
            return;
        }

        levelIsActive = false;

        pauseButton.SetActive(false);

        //Camera and flame animations
        LeanTween.move(camera, new Vector3(11f, 9f, 0f), 1.75f).setEase(LeanTweenType.easeInOutCubic);
        LeanTween.rotate(camera, new Vector3(20, 270, 0), 1.75f).setEase(LeanTweenType.easeInOutCubic);
        Invoke("KillBoss", 2.5f);

        SetHighScore();
        Unlock();
        SaveSystem.SaveGame(this);

        if (unlocksWorld) //Message prompt for unlocking new world
        {
            unlockedCustom.GetComponent<Text>().text = "New World and Wisp Color Unlocked!";
            StartCoroutine(LoadScene(0, 10f));
        }
        else if (PlayerPrefs.GetInt("CreditsHaveRolled", 0) == 1) //Final world - no credits
        {
            unlockedCustom.GetComponent<Text>().text = "Final Boss Extinguished!";
            StartCoroutine(LoadScene(0, 10f));
        }
        else //Final world- credits
        {
            unlockedCustom.GetComponent<Text>().text = "Final Boss Extinguished!";
            StartCoroutine(LoadScene(2, 10f));
            PlayerPrefs.SetInt("CreditsHaveRolled", 1); //Set so credits won't appear each time 6-B is cleared
        }

        //Unlock text anim
        LeanTween.moveLocalX(unlockedCustom, 0f, 1.25f).setEase(LeanTweenType.easeInOutExpo).setDelay(5.0f);
        LeanTween.moveLocalX(unlockedCustom, 1000f, 1.25f).setEase(LeanTweenType.easeInOutExpo).setDelay(8f);
    }

    public void WinInfinite()
    {
        FindObjectOfType<PlayerController>().PlayerDeath();
        levelIsActive = false;
        defeat.SetActive(true);
        pauseButton.SetActive(false);

        FindObjectOfType<AudioManager>().Play("Lose"); //TODO: modify

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
        if (isInfinite)
        {
            WinInfinite(); //Alternative for infinite levels
            return;
        }

        FindObjectOfType<PlayerController>().PlayerDeath();
        levelIsActive = false;
        defeat.SetActive(true);
        pauseButton.SetActive(false);

        FindObjectOfType<AudioManager>().Play("Lose");
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex));
    }

    public void RunOutOfTime()
    {
        if (!levelIsActive)
        {
            return;
        }
        if (isInfinite)
        {
            WinInfinite(); //Alternative for infinite levels
            return;
        }

        levelIsActive = false;
        timeExpired.SetActive(true);
        pauseButton.SetActive(false);

        FindObjectOfType<AudioManager>().Play("Lose");
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex));
    }

    //--------------------------
    //  COMPLIEMENTARY METHODS
    //--------------------------

    //Load scene
    IEnumerator LoadScene(int sceneIndex, float delay=3f)
    {
        yield return new WaitForSeconds(delay);
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

    private void KillBoss()
    {
        bossFlameParticles.GetComponent<ParticleSystem>().Stop();
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

        int numberOfLevels = SceneManager.sceneCountInBuildSettings - 3; //Era 2 pero creo que debe ser 3

        //Unlock levels
        if(currentLevel == levelsToUnlock && isBossLevel && (numberOfLevels-levelsToUnlock) >= 2) //If current lvl is highest available, is boss lvl, still have 2 or more locked
        {
            levelsToUnlock += 2;
        }
        else if (currentLevel == levelsToUnlock && isBossLevel && (numberOfLevels-levelsToUnlock) == 1) //If current lvl is highest available, is boss lvl, only final E level locked
        {
            levelsToUnlock += 1;
        }
        else if (currentLevel == levelsToUnlock && levelsToUnlock < numberOfLevels) //If current level is highest available level and not out of bounds
        {
            levelsToUnlock += 1;
        }

        //Unlock worlds/colors
        if (unlocksWorld && currentWorld == worldsToUnlock)
        {
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
            FindObjectOfType<HighScore>().SetHighScore(localScore);
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
            colorsToUnlock = 1;

            //High Score
            int numberOfLevels = SceneManager.sceneCountInBuildSettings - 3; //estaba en 2, pero creo que debería ser 3
            for (int i = 0; i < numberOfLevels; i++)
            {
                highScore.Add(0);
            }

            SaveSystem.SaveGame(this);
        }
    }

    public void DeleteSaveData()
    {
        //Unlockables
        levelsToUnlock = 1;
        worldsToUnlock = 1;
        colorsToUnlock = 1;

        //High Score
        int numberOfLevels = SceneManager.sceneCountInBuildSettings - 3; //estaba en 2, pero creo que debería ser 3
        for (int i = 0; i < numberOfLevels; i++)
        {
            highScore.Add(0);
        }

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

