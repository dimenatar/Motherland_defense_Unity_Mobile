using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    private bool _isLoading;

    private void Start()
    {
        SceneManager.sceneLoaded += SceneLoaded;
    }

    public void LoadLevel(string name)
    {
        if (!_isLoading)
        {
            _isLoading = true;
            SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);
        }
    }

    public void LoadLevelAdditive(string name)
    {
        SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
    }

    public void ExitToLevelSelection()
    {
        SceneManager.LoadScene("LevelSelectionScene");
        Time.timeScale = 1;
    }

    public void UnloadScene(string name)
    {
        SceneManager.UnloadSceneAsync(name);
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }

    public void Reload()
    {
        Time.timeScale = 1;
        if (!_isLoading)
        {
            _isLoading = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void SceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        _isLoading = false;
    }
}
