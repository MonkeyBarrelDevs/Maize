using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] Animator transition;

    [SerializeField] float transitionTime;

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
            LoadNextLevel();
    }

    public void LoadNextLevel() 
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void ReplayTransition() 
    {
        StartCoroutine(CycleTransition());
    }

    public void LoadLevelAtIndex(int index)
    {
        StartCoroutine(LoadLevel(index));
    }

    IEnumerator LoadLevel(int levelIndex) 
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }

    IEnumerator CycleTransition() 
    {
        yield return new WaitForSeconds(transitionTime);

    }
}
