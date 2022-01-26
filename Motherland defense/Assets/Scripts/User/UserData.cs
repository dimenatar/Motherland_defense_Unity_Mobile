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

    public List<LevelData> LevelData => _levelData;

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
    }
}
