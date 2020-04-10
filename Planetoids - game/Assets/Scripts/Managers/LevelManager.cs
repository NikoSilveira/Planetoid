using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    //UI GameObjects
    public GameObject victory;
    public GameObject defeat;
    public GameObject timeExpired;

    private bool levelIsActive;

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

    public bool GetLevelIsActive()
    {
        return levelIsActive;
    }
}

