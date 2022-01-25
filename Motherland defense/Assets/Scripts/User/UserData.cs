using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public class UserData : MonoBehaviour
{
    [SerializeField] private string _path;
    private List<LevelData> _levelData;
    private int _currentLevel;

    public void CompleteLevel()
    {
        // save progress
    }

    private void Start()
    {
        
    }

    public int GetAmountCompletedLevels()
    {
        return _levelData.Count;
    }
}
