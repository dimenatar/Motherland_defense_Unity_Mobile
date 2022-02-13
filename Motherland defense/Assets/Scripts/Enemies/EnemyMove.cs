using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(Rigidbody))]
public class EnemyMove : MonoBehaviour
{
    public event Action OnStartMove;
    public event Action OnStop;

    private float _speed;
    private int _targetCheckPointIndex;
    private EnemyCheckPoints _enemyCheckPoints;
    //private EnemyCheckPoint _targetCheckPoint;
    private Enemy _enemy;
    private Rigidbody _rigidbody;
    private Vector3 _targetPoint;

    public int TargetCheckPointIndex => _targetCheckPointIndex;

    public void ChangeNextCheckPoint(EnemyCheckPoint checkPoint)
    {
        int index = _enemyCheckPoints.GetCheckPointIndex(checkPoint);
        if (index != -1)
        {
            _targetCheckPointIndex = index + 1;
            _targetPoint = _enemyCheckPoints.GetNextCheckPointPosition(checkPoint);
            transform.LookAt(_targetPoint);
        }
    }

    public void ChangeNextCheckPoint(int index)
    {
        _targetPoint = _enemyCheckPoints.GetCheckPointPosition(index);
        Debug.Log(index);
        _targetCheckPointIndex = index;
        transform.LookAt(_targetPoint);
    }

    public void InitializeMove(EnemyCheckPoints enemyCheckPoints, float speed, Vector3 targetCheckPoint)
    {
        _enemyCheckPoints = enemyCheckPoints;
        _speed = speed;
        _enemy = GetComponent<Enemy>();
        _enemy.OnDied += StopMove;
        _enemy.OnFoundOpponent += StopMove;
        _targetPoint = targetCheckPoint;
        StartMove();
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void StartMove()
    {
        OnStartMove?.Invoke();
        StartCoroutine(nameof(Move));
    }

    public void StopMove()
    {
        StopCoroutine(nameof(Move));
        OnStop?.Invoke();
    }

    public IEnumerator Move()
    {
        while (true)
        {
            transform.LookAt(_targetPoint);
            transform.position = Vector3.MoveTowards(transform.position, _targetPoint, _speed);
            yield return new WaitForFixedUpdate();
        }
    }

    public float GetSpeed()
    {
        return _speed;
    }

    public void SetSpeed(float speed)
    {
        if (speed >= 0)
        {
            _speed = speed;
        }
        else
        {
            _speed = 0;
        }
    }
}
