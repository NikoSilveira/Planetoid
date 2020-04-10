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
    public GameObject victory;//here

    private int targetCount; 
    private int counter;
    private bool victoryControl;//super bool

    void Start()
    {
        counter = 0;
        victoryControl = true;//super bool
    }

    void Update()
    {
        counterText.text = counter.ToString() + "/" + targetCount.ToString();

        if (counter == targetCount && victoryControl)//quitar con super bool
        {
            victoryControl = false;//cambiar a super bool

            FindObjectOfType<PlayerController>().SetLevelIsActive(false);//here
            victory.SetActive(true);//here
            victory.GetComponent<Text>().text = RandomMessage();//here
            StartCoroutine(LoadLevel());//here
        }
    }

    IEnumerator LoadLevel()//here
    {
        yield return new WaitForSeconds(1.5f);
        FindObjectOfType<LevelLoader>().LoadTargetLevel(0);
    }

    //---------------------
    //  GETTERS & SETTERS
    //---------------------

    public void SetCounter()
    {
        counter = counter + 1;
    }

    public int GetCounter()
    {
        return counter;
    }

    public void SetTargetCount(int newTargetCount)
    {
        targetCount = newTargetCount;
    }

    //--------------------
    //   RANDOM MESSAGE
    //--------------------

    private string RandomMessage()//here
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
