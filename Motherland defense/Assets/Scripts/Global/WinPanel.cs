using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class WinPanel : MonoBehaviour
{
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private EnemyCounter _enemyCounter;
    [SerializeField] private LevelStatistics _levelStatistics;
    [SerializeField] private StatiscticsPanel _statiscticsPanel;
    [SerializeField] private ObjectDisabler _disabler;

    private void Start()
    {
        _enemyCounter.OnCounterEmpty += ShowWinPanel;
    }

    private void ShowWinPanel()
    {
        _disabler.Disable();
        _winPanel.SetActive(true);
        GetAndFillStatisctics();
        Time.timeScale = 0;
    }
    private void GetAndFillStatisctics()
    {
        LevelData levelData = _levelStatistics.GetLevelStatistic();
        _statiscticsPanel.Initialise(levelData.AmountRemainingHealth, levelData.DamageToUnits, levelData.AmountEnemies, levelData.Time);
    }
}
