using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanging : MonoBehaviour
{
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Exit();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            changeSceneAsync("SampleScene");
        }
    }


    private AsyncOperation async;

    public void changeScene(string sceneName)
    {
        print("changing to: " + sceneName);
        SceneManager.LoadScene(sceneName);
    }

    public void changeSceneAsync(string sceneName)
    {
        LoadSceneAsync(sceneName);
    }


    public void Exit()
    {
        Application.Quit();
    }

    private void LoadSceneAsync(string sceneName)
    {
        async = SceneManager.LoadSceneAsync(sceneName);
    }
}
