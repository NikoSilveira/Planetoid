using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    //Pass crossfade in levelloader object to transition
    public Animator transition;
    public float transitionTime = 1f;

    //Call for level transition
    public void LoadTargetLevel(int targetLevelIndex)
    {
        StartCoroutine(LoadLevel(targetLevelIndex));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
}
