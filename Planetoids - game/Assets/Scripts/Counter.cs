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

    public Text counterText;    //Assign in inspector
    public GameObject victory;  //Assign in inspector
    public int targetCount;     //Set in inspector
    private int counter;

    void Start()
    {
        counter = 0;
    }

    void Update()
    {
        counterText.text = counter.ToString() + "/" + targetCount.ToString();

        if (counter == targetCount)
        {
            victory.SetActive(true);
            StartCoroutine(LoadLevel());
        }
    }

    //Setter to be called to increase counter by 1
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

    //Load level upon reaching win condition
    IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(1.5f);
        FindObjectOfType<LevelLoader>().LoadTargetLevel(0);
    }
}
