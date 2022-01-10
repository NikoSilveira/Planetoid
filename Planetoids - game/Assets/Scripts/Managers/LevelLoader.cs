using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    //Pass crossfade in levelloader object to transition
    public Animator transition;
    private float transitionTime = 1f;
    [SerializeField] private GameObject loadingSymbol;

    //Call for level transition
    public void LoadTargetLevel(int targetLevelIndex)
    {
        StartCoroutine(LoadLevel(targetLevelIndex));

        //Loading anim
        //LeanTween.alpha(loadingSymbol.GetComponent<RectTransform>(), 1f, 1.25f);
        //LeanTween.rotateAround(loadingSymbol, Vector3.back, 180, 1.0f).setLoopClamp().setEaseInOutCubic();
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        //Black fade anim
        transition.SetTrigger("Start");

        //Loading anim
        LeanTween.alpha(loadingSymbol.GetComponent<RectTransform>(), 1f, 1.25f);
        LeanTween.rotateAround(loadingSymbol, Vector3.back, 180, 1.25f).setLoopClamp().setEaseInOutCubic();

        //Audio theme stop
        FindObjectOfType<AudioManager>().Stop("Theme");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadSceneAsync(levelIndex);
    }
}
