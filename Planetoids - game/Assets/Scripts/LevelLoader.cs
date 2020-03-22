using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    //Pass crossfade in levelloader object to transition
    public Animator transition;
    public float transitionTime = 1f;

    void Update()
    {
        
    }

    //Call for level transition

    public void loadTargetLevel(int targetLevelIndex)
    {
        StartCoroutine(LoadLevel(targetLevelIndex));
    }

    //Will be called by loading functions
    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
}
