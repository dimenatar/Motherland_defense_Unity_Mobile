using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class LevelData
{
    public LevelData(int levelNumber, int amountRemainingHealth, int damageToUnits, int amountEnemies, float time)
    {
        _levelNumber = levelNumber;
        _amountRemainingHealth = amountRemainingHealth;
        _damageToUnits = damageToUnits;
        _amountEnemies = amountEnemies;
        _time = time;
    }

    public LevelData() {}

    private int _levelNumber = 1;
    private int _amountRemainingHealth;
    private int _damageToUnits;
    private int _amountEnemies;
    private float _time;

    public int LevelNumber { get => _levelNumber;}
    public int AmountRemainingHealth { get => _amountRemainingHealth;}
    public int DamageToUnits { get => _damageToUnits;}
    public int AmountEnemies { get => _amountEnemies;}
    public float Time { get => _time;}
}
