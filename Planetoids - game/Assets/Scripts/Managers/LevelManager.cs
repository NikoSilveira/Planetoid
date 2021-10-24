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

    //Unlocking levels
    [SerializeField] private int currentLevel;
    [SerializeField] private int currentWorld;
    [SerializeField] private bool unlocksWorld;

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

        //SetHighScore();
        //Unlock();
        //SaveSystem.SaveGame(this);

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

        //SetHighScore();
        //Unlock();
        //SaveSystem.SaveGame(this);

        StartCoroutine(LoadScene(0, 7f));
    }

    public void WinInfinite()
    {
        FindObjectOfType<PlayerController>().PlayerDeath();
        levelIsActive = false;
        defeat.SetActive(true);
        pauseButton.SetActive(false);

        FindObjectOfType<AudioManager>().Play("Lose"); //TODO: modify

        //SetHighScore();
        //Unlock();
        //SaveSystem.SaveGame(this);

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

    //Coroutine - load scene
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
        //victory.GetComponent<Text>().text = "BOSS STAGE CLEARED";
        //victory.SetActive(true);
    }

    //-------------
    //  UNLOCKING
    //-------------

    private void Unlock()
    {
        /*GameData data = SaveSystem.LoadGame();

        worldsToUnlock = data.worldsToUnlock;
        colorsToUnlock = data.colorsToUnlock;
        levelsToUnlock = data.levelsToUnlock;

        int numberOfLevels = SceneManager.sceneCountInBuildSettings - 2;

        //Unlock levels
        if (currentLevel == levelsToUnlock && levelsToUnlock < numberOfLevels)
        {
            levelsToUnlock += 1;
        }

        //Unlock worlds/colors
        if (unlocksWorld && currentWorld == worldsToUnlock)
        {
            worldsToUnlock += 1;
            colorsToUnlock += 1;

            //Text animation
            unlockedCustom.SetActive(true);
            LeanTween.alphaText(unlockedCustom.GetComponent<RectTransform>(), 1f, 1.5f);
        }*/
    }

    //--------------
    //  HIGH SCORE
    //--------------

    private void SetHighScore()
    {
        /*GameData data = SaveSystem.LoadGame();
        highScore = data.highScore;

        int localHighScore = highScore[currentLevel - 1];
        int localScore = FindObjectOfType<Score>().GetScore();

        if (localScore > localHighScore)
        {
            highScore[currentLevel - 1] = localScore;
            FindObjectOfType<HighScore>().SetHighScore(localScore);
        }*/
    }

    //-----------------
    //   SAVE SYSTEM
    //-----------------

    //Create document on awake (1st time)
    private void InitializeSaveData()
    {
        /*if(SaveSystem.LoadGame() == null)
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
        }*/
    }

    //Debugging - Reset document
    /*public void ClearForDebug()
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
    }*/

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

