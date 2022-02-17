using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class UserData
{
    public UserData()
    {
        _levelData = new List<LevelData>();
        _levelData.Add(new LevelData());
    }

    private List<LevelData> _levelData;
    private bool _isCompletedTutorial;
    private bool _isNeedToShowCredits;
    public List<LevelData> LevelData => _levelData;
    public bool IsCompletedTutorial => _isCompletedTutorial;
    public bool IsNeedToShowCredits => _isNeedToShowCredits;
    public void CompleteLevel(LevelData levelData)
    {
        if (_levelData.Select(number => number.LevelNumber).Contains(levelData.LevelNumber))
        {
            int index = _levelData.FindIndex(number => number.LevelNumber == levelData.LevelNumber);
            _levelData[index] = levelData;
        }
        else
        {
            _levelData.Add(levelData);
        }
        if (levelData.LevelNumber == 10)
        {
            _isNeedToShowCredits = true;
        }
    }

    public void ShownCredits()
    {
        _isNeedToShowCredits = false;
    }

    public void CompleteTutorial()
    {
        _isCompletedTutorial = true;
    }
}
