using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMarker : MonoBehaviour
{
    [SerializeField] private int _levelNumber;
    [SerializeField] private string _loadingLevelName;
    [SerializeField] private Text _levelNumberText;
    [SerializeField] private LevelLoader _loader;
    [SerializeField] private StatiscticsPanel _levelPanel;

    private LevelData _levelData = new LevelData();
    private bool _isFirstClick = true;

    public int LevelNumber => _levelNumber;

    public void SetLevelData(LevelData levelData)
    {
        _levelData = levelData;
    }

    public void OnMarkerClick()
    {
        if (_isFirstClick)
        {
            _levelPanel.gameObject.SetActive(true);
            _levelPanel.Initialise(_levelData.AmountRemainingHealth, _levelData.DamageToUnits, _levelData.AmountEnemies, ConvertFloatTimeToString(_levelData.Time));
            _isFirstClick = false;
        }
        else
        {
            _loader.LoadLevel(_loadingLevelName);
        }
    }

    private string ConvertFloatTimeToString(float time)
    {
        return TimeSpan.FromSeconds(time).ToString("mm':'ss");
    }
}
