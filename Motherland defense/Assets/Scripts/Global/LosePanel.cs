using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LosePanel : MonoBehaviour
{
    [SerializeField] private GameObject _losePanel;

    public void ShowLosePanel()
    {
        Time.timeScale = 0;
        _losePanel.SetActive(true);
    }
}
