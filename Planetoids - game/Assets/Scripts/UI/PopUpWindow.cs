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
        LeanTween.size(window.GetComponent<RectTransform>(), new Vector2(1f, 1f), 0.01f); //Start minimized
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
        Application.Quit();
    }
}
