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

    public Vector3 GetNextCheckPointPosition(int index)
    {
        return GetRandomCheckPointPosition(checkPoints[index + 1]);
    }

    public Vector3 GetNextCheckPointPosition(EnemyCheckPoint current)
    {
        return GetRandomCheckPointPosition(checkPoints[checkPoints.FindIndex(c => c == current)+1]);
    }

    public int GetCheckPointIndex(EnemyCheckPoint enemyCheckPoint)
    {
        return checkPoints.Contains(enemyCheckPoint) ? checkPoints.IndexOf(enemyCheckPoint) : -1;
    }

    private Vector3 GetRandomCheckPointPosition(EnemyCheckPoint point)
    {
        Debug.Log($"start = {point.transform.position}");
        //Vector3 p1 = point.GetComponent<CapsuleCollider>().b
        Vector3 p = point.transform.forward.normalized * UnityEngine.Random.Range(-point.GetComponent<CapsuleCollider>().height / 2, point.GetComponent<CapsuleCollider>().height / 2);
        Debug.DrawLine(point.transform.position, p + point.transform.position, Color.red, 5);
        Debug.Log($"end = {p + point.transform.position}");
        return p + point.transform.position;
    }
}
