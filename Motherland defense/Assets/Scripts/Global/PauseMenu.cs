using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        Time.timeScale = SpeedGameUp.Speeds.Where(pair => pair.Key == SpeedGameUp.CurrentSpeed).Select(pair => pair.Value).FirstOrDefault();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            ShowPauseMenu();
        }
    }
}
