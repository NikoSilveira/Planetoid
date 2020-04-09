using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Script to control the UI counter
 * -Control of vistory condition and scene transition
 */
public class Counter : MonoBehaviour
{
    public Text counterText;
    public GameObject victory;

    [SerializeField] private int targetCount; 

    private int counter;
    private bool victoryControl;

    void Start()
    {
        counter = 0;
        victoryControl = true;
    }

    void Update()
    {
        counterText.text = counter.ToString() + "/" + targetCount.ToString();

        if (counter == targetCount && victoryControl)
        {
            victoryControl = false;

            FindObjectOfType<PlayerController>().SetLevelIsActive(false);
            victory.SetActive(true);
            victory.GetComponent<Text>().text = RandomMessage();
            StartCoroutine(LoadLevel());
        }
    }

    IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(1.5f);
        FindObjectOfType<LevelLoader>().LoadTargetLevel(0);
    }

    //---------------------
    //  GETTERS & SETTERS
    //---------------------

    public void setCounter()
    {
        counter = counter + 1;
    }

    public int getCounter()
    {
        return counter;
    }

    public int getTargetCount()
    {
        return targetCount;
    }

    //--------------------
    //   RANDOM MESSAGE
    //--------------------

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
}
