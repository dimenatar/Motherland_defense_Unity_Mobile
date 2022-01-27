using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Transform))]
public class EnemyCheckPoints : MonoBehaviour
{
    [SerializeField] private List<EnemyCheckPoint> checkPoints;

    public EnemyCheckPoint GetEnemyCheckPointByIndex(int index)
    {
        if (index >= checkPoints.Count || index < 0)
        {
            return null;
        }
        return checkPoints[index];
    }

    public int GetCheckPointIndex(EnemyCheckPoint enemyCheckPoint)
    {
        return checkPoints.Contains(enemyCheckPoint) ? checkPoints.IndexOf(enemyCheckPoint) : 0;
    }
}
