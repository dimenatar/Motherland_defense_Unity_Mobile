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
    private int _healthAmount;
    private float _levelTimer = 0;

    public LevelData GetLevelStatistic()
    {
        return new LevelData(_levelNumber, _healthAmount, _totalDamage, _amountEnemies, _levelTimer);
    }

    public void AddTotalDamage(int health, int damage)
    {
        _totalDamage += damage;
    }

    public void AddEnemy()
    {
        _amountEnemies++;
    }

    private void SetStartHealth(int value)
    {
        _healthAmount = value;
    }

    private void ChangeHealthAmount(int value)
    {
        _healthAmount -= value;
    }

    private void Start()
    {
        _health.OnHealthChanged += ChangeHealthAmount;
        _health.OnHealthSetted += SetStartHealth;
    }

    private void FixedUpdate()
    {
        _levelTimer += Time.deltaTime;
    }
}
