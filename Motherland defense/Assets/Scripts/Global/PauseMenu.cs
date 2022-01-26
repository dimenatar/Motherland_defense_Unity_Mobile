using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;

    public void ShowPauseMenu()
    {
        _pausePanel.SetActive(true);
        Time.timeScale = 0;

    }

    public void HidePauseMenu()
    {
        _pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            ShowPauseMenu();
        }
    }
}
