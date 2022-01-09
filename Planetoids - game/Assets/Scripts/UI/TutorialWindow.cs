using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Canvas must be enabled, child panel must be disabled. Scene 1-1. DontShowAgain must be disabled.
 * Playerprefs DontShowAgain1 remebers if 1-1 tutorial is to be shown again.
 */
public class TutorialWindow : MonoBehaviour
{
    //UI elements
    [SerializeField] private GameObject windowPanel;
    [SerializeField] private GameObject window;
    [SerializeField] private GameObject rawImg1, rawImg2;
    [SerializeField] private GameObject dontShowAgain, wisp2;
    [SerializeField] private Texture imageFile1, imageFile2;
    [SerializeField] private Text wispText1, wispText2, windowButtonText;
    
    private Toggle toggle;
    private bool isLastWindow;

    void Start()
    {
        toggle = dontShowAgain.transform.GetChild(0).gameObject.GetComponent<Toggle>();
        isLastWindow = false;
        LeanTween.size(window.GetComponent<RectTransform>(), new Vector2(0.01f, 0.01f), 0.01f); //Start minimized

        if (PlayerPrefs.GetInt("DontShowAgain1", 0) == 0) //if window is set to appear
        {
            StartCoroutine(OpenWindow(500f, 360f));
        }
    }

    IEnumerator OpenWindow(float width, float height)
    {
        yield return new WaitForSeconds(0.35f);
        windowPanel.SetActive(true);
        LeanTween.size(window.GetComponent<RectTransform>(), new Vector2(width, height), 0.5f).setEase(LeanTweenType.easeInOutCubic);

        //Pause time
        yield return new WaitForSeconds(0.81f);
        Time.timeScale = 0f;
    }

    public void NextWindow()
    {
        if (isLastWindow)
        {
            CloseWindow();
            return;
        }

        wispText1.text = "Booster Wisps:\nTimey (green), shieldy (gray) and speedy (red).\nWill spawn ocassionally.";
        windowButtonText.text = "Done";
        rawImg1.GetComponent<RawImage>().texture = imageFile1;
        wisp2.SetActive(false);
        dontShowAgain.SetActive(true);

        isLastWindow = true;
    }

    public void CloseWindow()
    {
        if (toggle.isOn)
        {
            PlayerPrefs.SetInt("DontShowAgain1", 1);
        }

        Time.timeScale = 1f;
        LeanTween.size(window.GetComponent<RectTransform>(), new Vector2(1f, 1f), 0.01f);
        windowPanel.SetActive(false);
    }
}
