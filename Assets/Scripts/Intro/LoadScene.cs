using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private bool allowSceneToChange;

    private void Awake()
    {
        allowSceneToChange = false;
    }

    public void ChangeSceneToLoad(string otherSceneToLoad)
    {
        sceneToLoad = otherSceneToLoad;
    }
    
    public void StartAsyncLoading()
    {
        StartCoroutine(LoadSceneCoroutine());
    }

    public void AllowSceneToChange()
    {
        allowSceneToChange = true;
    }

    IEnumerator LoadSceneCoroutine()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        asyncOperation.allowSceneActivation = false;
        
        while (!asyncOperation.isDone)
        {
            if (asyncOperation.progress >= 0.9f)
            {
                yield return new WaitUntil(()=>
                {
                     return allowSceneToChange;
                });
                asyncOperation.allowSceneActivation = true;
                allowSceneToChange = false;
            }

            yield return null;
        }
    }
}
