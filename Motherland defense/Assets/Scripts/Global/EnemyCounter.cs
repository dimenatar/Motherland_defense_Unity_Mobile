using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    [SerializeField] private EnemyWaves _waves;
    public event Action OnCounterEmpty;

    private int _enemyCounter;
    
    public void AddEnemies(int amount)
    {
        _enemyCounter += amount;
        Debug.Log(_enemyCounter);
    }

    public void ReduceEnemy()
    {
        _enemyCounter--;
        Debug.Log(_enemyCounter);
        if (_enemyCounter == 0)
        {
            OnCounterEmpty?.Invoke();
        }
    }

    private void Start()
    {
        _waves.Waves.ForEach(enemy => AddEnemies(enemy.Enemies.Count));
    }
}
