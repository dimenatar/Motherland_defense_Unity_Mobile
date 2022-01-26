using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLevelPropress : MonoBehaviour
{
    [SerializeField] private LevelStatistics _levelStatistics;
    [SerializeField] private EnemyCounter _enemyCounter;

    public void SaveProgress()
    {
        UserData userData = UserProgressManager.LoadUserData(UserProgressManager.Path);
        userData.CompleteLevel(_levelStatistics.GetLevelStatistic());
        UserProgressManager.SaveUserData(UserProgressManager.Path, userData);
    }
    private void Start()
    {
        _enemyCounter.OnCounterEmpty += SaveProgress;
    }
}
