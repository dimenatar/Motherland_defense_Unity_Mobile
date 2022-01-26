using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelStatistics : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private int _levelNumber;
    private int _totalDamage;
    private int _amountEnemies;
    private float _levelTimer = 0;

    public LevelData GetLevelStatistic()
    {
        return new LevelData(_levelNumber, _health.CurrentHealth, _totalDamage, _amountEnemies, ConvertFloatTimeToString(_levelTimer));
    }

    public void AddTotalDamage(int health, int damage)
    {
        _totalDamage += damage;
    }

    public void AddEnemy()
    {
        _amountEnemies++;
    }

    private void FixedUpdate()
    {
        _levelTimer += Time.deltaTime;
    }

    private string ConvertFloatTimeToString(float time)
    {
        return TimeSpan.FromSeconds(time).ToString("mm':'ss");
    }
}
