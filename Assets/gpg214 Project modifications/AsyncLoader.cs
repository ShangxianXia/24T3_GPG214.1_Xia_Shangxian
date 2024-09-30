using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsyncLoader : MonoBehaviour
{
    private Action OnLevelLoadedAction;

    [SerializeField] private string LevelToLoad = "Un-named scene";

    private void OnEnable()
    {
        OnLevelLoadedAction += OnLevelLoaded;
    }

    private void OnDisable()
    {
        OnLevelLoadedAction -= OnLevelLoaded;
    }

    private void Start()
    {
        StartCoroutine(CoroutineLoadLevel(LevelToLoad, OnLevelLoadedAction));
    }

    IEnumerator CoroutineLoadLevel(string sceneName,  Action OnLevelLoadedCallBack)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false; // turn off scene activation or script wont exist 

        // the 0.9f is the loadin of all the assets for the next scene
        // the last 0.1 is swapping from current scene to next.

        while (asyncLoad.progress < 0.9f)
        {
            Debug.Log("Loading...");
            yield return null;
        }

        // invoke the action (Event) to let everyone know the scene loaded correctly
        if (OnLevelLoadedCallBack != null)
        {
            OnLevelLoadedCallBack.Invoke();
            yield return null;
        }

        // allow the scene to activate and complete its loading to 100%
        asyncLoad.allowSceneActivation = true;
        yield return null;
    }

    private void OnLevelLoaded() 
    {
        Debug.Log("The main scene has loaded");
    }

}
