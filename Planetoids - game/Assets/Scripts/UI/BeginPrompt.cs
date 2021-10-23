using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Control the animation for the "start prompt"
 */
public class BeginPrompt : MonoBehaviour
{
    private float transitionDuration;

    void Start()
    {
        transitionDuration = 0.75f;

        LeanTween.moveLocalX(this.gameObject, 0f, transitionDuration).setEase(LeanTweenType.easeInOutExpo).setDelay(0.5f);
        LeanTween.moveLocalX(this.gameObject, 800f, transitionDuration).setEase(LeanTweenType.easeInOutExpo).setDelay(1.25f + transitionDuration);
    }
}
