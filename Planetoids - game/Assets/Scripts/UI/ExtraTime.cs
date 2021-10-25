using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraTime : MonoBehaviour
{
    private float animDuration;

    private void Start()
    {
        animDuration = 0.50f;
    }

    public void ShowAnimation()
    {
        LeanTween.alpha(this.gameObject, 255f, animDuration).setEase(LeanTweenType.easeInOutCubic);
        LeanTween.alpha(this.gameObject, 0f, animDuration).setEase(LeanTweenType.easeInOutCubic).setDelay(animDuration + 0.20f);
    }
}
