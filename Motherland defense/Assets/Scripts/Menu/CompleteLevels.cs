using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CompleteLevels : MonoBehaviour
{
    [SerializeField] private List<LevelMarker> _levelMarkers;
    [SerializeField] private int _totalAmountLevels;
    private UserData _userData;

    private void Start()
    {
        _userData = UserProgressManager.LoadUserData(UserProgressManager.Path);
        EnableAvailableLevels();
    }

    private void EnableAvailableLevels()
    {
        if (_userData == null)
        {
            Debug.Log("user data null");
            _userData=new UserData();
            UserProgressManager.SaveUserData(UserProgressManager.Path, _userData);
            InitialiseLevelMarker(0, new LevelData());
        }
        else
        {
            for (int i = 0; i < _userData.LevelData.Count; i++)
            {
                Debug.Log(_userData.LevelData[i].LevelNumber + " " + _userData.LevelData[i].Time);
                InitialiseLevelMarker(i, _userData.LevelData[i]);
            }
            if (_userData.LevelData.Count < _totalAmountLevels)
            {
                InitialiseLevelMarker(_userData.LevelData.Count, new LevelData());
            }
        }
    }

    private void InitialiseLevelMarker(int index, LevelData data)
    {
        _levelMarkers[index].gameObject.SetActive(true);
        _levelMarkers[index].SetLevelData(data);
    }
}
