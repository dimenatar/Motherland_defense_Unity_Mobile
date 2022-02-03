using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class LevelMarker : MonoBehaviour
{
    [SerializeField] private int _levelNumber;
    [SerializeField] private string _loadingLevelName;
    [SerializeField] private Sprite _activeImage;
    [SerializeField] private Sprite _passiveImage;
    [SerializeField] private LevelLoader _loader;
    [SerializeField] private StatiscticsPanel _levelPanel;
    [SerializeField] private MarkerDeselector _markerDeselector;

    private LevelData _levelData = new LevelData();
    private bool _isFirstClick = true;

    public int LevelNumber => _levelNumber;

    public void SetLevelData(LevelData levelData)
    {
        Debug.Log("set");
        _levelData = levelData;
    }

    public void Deselect()
    {
        GetComponent<Image>().sprite = _passiveImage;
        _isFirstClick = true;
    }

    public void Select()
    {
        GetComponent<Image>().sprite = _activeImage;
    }

    public void OnMarkerClick()
    {
        if (_isFirstClick)
        {
            _markerDeselector.Deselect();
            Select();
            _levelPanel.gameObject.SetActive(true);
            _levelPanel.Initialise(_levelData.AmountRemainingHealth, _levelData.DamageToUnits, _levelData.AmountEnemies, _levelData.Time);
            _isFirstClick = false;
        }
        else
        {
            _loader.LoadLevel(_loadingLevelName);
        }
    }
}
