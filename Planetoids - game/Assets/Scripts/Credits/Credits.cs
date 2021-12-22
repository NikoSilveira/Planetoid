using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    [SerializeField] TextAsset file;

    void Start()
    {
        this.gameObject.GetComponent<Text>().text = file.text;  //Read from file
        LeanTween.moveLocalY(this.gameObject, 4300f, 76f).setOnComplete(ExitCredits);
    }

    private void ExitCredits()
    {
        FindObjectOfType<LevelLoader>().LoadTargetLevel(0);     //Back to main menu
    }
}