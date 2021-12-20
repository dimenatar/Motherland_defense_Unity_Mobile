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
}
