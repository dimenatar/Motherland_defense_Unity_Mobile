using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelStatistics : MonoBehaviour
{
    private int _totalDamage;
    private int _killedEnemies;
    private float _levelTimer;
    private DateTime _levelScore;

    public void AddTotalDamage(int health, int damage)
    {
        _totalDamage += damage;
    }

    public void AddKilledEnemies()
    {
        _killedEnemies++;
    }
    private void Start()
    {

    }
}
