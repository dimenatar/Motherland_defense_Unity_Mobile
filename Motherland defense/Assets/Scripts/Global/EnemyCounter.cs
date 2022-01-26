using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    public event Action OnCounterEmpty;

    private int _enemyCounter;
    
    public void AddEnemy()
    {
        _enemyCounter++;
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
}
