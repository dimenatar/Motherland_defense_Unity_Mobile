using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Transform))]
public class EnemyCheckPoints : MonoBehaviour
{
    [SerializeField] private List<EnemyCheckPoint> _checkPoints;

    public EnemyCheckPoint GetEnemyCheckPointByIndex(int index)
    {
        if (index >= _checkPoints.Count || index < 0)
        {
            return null;
        }
        return _checkPoints[index];
    }

    public Vector3 GetCheckPointPosition(int index)
    {
        //не тут
        return GetRandomCheckPointPosition(_checkPoints[index]);
    }

    public Vector3 GetNextCheckPointPosition(EnemyCheckPoint current)
    {
        return GetRandomCheckPointPosition(_checkPoints[_checkPoints.FindIndex(c => c == current)+1]);
    }

    public int GetCheckPointIndex(EnemyCheckPoint enemyCheckPoint)
    {
        return _checkPoints.Contains(enemyCheckPoint) ? _checkPoints.IndexOf(enemyCheckPoint) : -1;
    }

    //не тут
    private Vector3 GetRandomCheckPointPosition(EnemyCheckPoint point)
    {
        //Vector3 p = point.transform.forward.normalized * UnityEngine.Random.Range(-point.GetComponent<CapsuleCollider>().height / 2, point.GetComponent<CapsuleCollider>().height / 2);
        //Debug.DrawLine(point.transform.position, p + point.transform.position, Color.red, 5);
        //return point.transform.forward.normalized * UnityEngine.Random.Range(-point.GetComponent<CapsuleCollider>().height / 2, point.GetComponent<CapsuleCollider>().height / 2) + point.transform.position;
        return point.transform.position;
    }
}
