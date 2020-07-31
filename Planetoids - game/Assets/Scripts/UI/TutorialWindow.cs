using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialWindow : MonoBehaviour
{
    //UI elements
    [SerializeField] private GameObject windowPanel;
    [SerializeField] private GameObject window;
    [SerializeField] private GameObject rawImg1, rawImg2;
    [SerializeField] private Texture imageFile1, imageFile2;
    [SerializeField] private Text wispText1, wispText2, windowButtonText;

    private bool isLastWindow;

    void Start()
    {
        isLastWindow = false;
        LeanTween.size(window.GetComponent<RectTransform>(), new Vector2(1f, 1f), 0.01f); //Start minimized
        StartCoroutine(OpenWindow(350f, 250f));
    }

    IEnumerator OpenWindow(float width, float height)
    {
        yield return new WaitForSeconds(1f);
        windowPanel.SetActive(true);
        LeanTween.size(window.GetComponent<RectTransform>(), new Vector2(width, height), 0.8f).setEase(LeanTweenType.easeOutBounce);

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

        wispText1.text = "Green Wisp:\nWill spawn ocassionally. Extinguish it to obtain an additional 45s.";
        wispText2.text = "Dark Blue Wisp:\nWill spawn ocassionally. Extinguish it to obtain protection from one hit of an ominous wisp.";
        windowButtonText.text = "Done";
        rawImg1.GetComponent<RawImage>().texture = imageFile1;
        rawImg2.GetComponent<RawImage>().texture = imageFile2;

        isLastWindow = true;
    }

    public void CloseWindow()
    {
        Time.timeScale = 1f;
        LeanTween.size(window.GetComponent<RectTransform>(), new Vector2(1f, 1f), 0.01f);
        windowPanel.SetActive(false);
    }
}
