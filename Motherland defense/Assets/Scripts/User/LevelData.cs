using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class LevelData
{
    public LevelData(int levelNumber, int amountRemainingHealth, int damageToUnits, int amountEnemies, string time, bool isCompleted)
    {
        _levelNumber = levelNumber;
        _amountRemainingHealth = amountRemainingHealth;
        _damageToUnits = damageToUnits;
        _amountEnemies = amountEnemies;
        _time = time;
        _isCompleted = isCompleted;
    }

    public LevelData() {}

    private bool _isCompleted;
    private int _levelNumber = 1;
    private int _amountRemainingHealth;
    private int _damageToUnits;
    private int _amountEnemies;
    private string _time = "00:00";

    public int LevelNumber { get => _levelNumber;}
    public int AmountRemainingHealth { get => _amountRemainingHealth;}
    public int DamageToUnits { get => _damageToUnits;}
    public int AmountEnemies { get => _amountEnemies;}
    public string Time { get => _time;}
    public bool IsCompleted { get => _isCompleted;}
}
