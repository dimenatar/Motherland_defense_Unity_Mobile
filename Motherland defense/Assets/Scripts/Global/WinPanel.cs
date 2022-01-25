using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WinPanel : MonoBehaviour
{
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private EnemyCounter _enemyCounter;
    private void Start()
    {
        _enemyCounter.OnCounterEmpty += ShowWinPanel;
    }

    private void ShowWinPanel()
    {
        _winPanel.SetActive(true);
        Time.timeScale = 0;
    }
}
