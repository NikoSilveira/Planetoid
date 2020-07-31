using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpWindow : MonoBehaviour
{
    //UI elements
    [SerializeField] private GameObject windowPanel;
    [SerializeField] private GameObject window;

    [SerializeField] private bool displayOnStart;
    
    void Start()
    {
        //Start minimized
        LeanTween.size(window.GetComponent<RectTransform>(), new Vector2(1f, 1f), 0.01f);

        if (displayOnStart)
        {
            OpenWindow(350f, 250f);
        }
    }

    public void OpenWindow(float width, float height)
    {
        windowPanel.SetActive(true);
        LeanTween.size(window.GetComponent<RectTransform>(), new Vector2(width, height), 0.8f).setEase(LeanTweenType.easeOutBounce);
    }

    public void CloseWindow()
    {
        LeanTween.size(window.GetComponent<RectTransform>(), new Vector2(1f, 1f), 0.01f);
        windowPanel.SetActive(false);
    }

    public void QuitConfirmation()
    {
        Application.Quit();     //Quit game
    }
}
