using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpWindow : MonoBehaviour
{
    //UI elements
    [SerializeField] private GameObject windowPanel;
    [SerializeField] private GameObject window;
    
    void Start()
    {
        //Start minimized
        LeanTween.scale(window, new Vector2(0.1f, 0.1f), 0.01f);
    }

    public void OpenWindow()
    {
        windowPanel.SetActive(true);
        LeanTween.scale(window, new Vector2(1f, 1f), 0.7f).setEase(LeanTweenType.easeOutBounce);
    }

    public void CloseWindow()
    {
        LeanTween.scale(window, new Vector2(0.1f, 0.1f), 0.01f);
        windowPanel.SetActive(false);
    }

    public void QuitConfirmation()
    {
        Application.Quit();     //Quit game
    }
}
