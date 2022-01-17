using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public void LoadLevel(string name)
    {
        SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);
    }
    public void LoadLevelAdditive(string name)
    {
        SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
    }
    public void UnloadScene(string name)
    {
        SceneManager.UnloadSceneAsync(name);
    }   
    public void ExitGame()
    {
        Application.Quit();
    }
}
