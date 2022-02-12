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
    private EnemyCheckPoint _targetCheckPoint;
    private Enemy _enemy;
    private Rigidbody _rigidbody;

    public int TargetCheckPointIndex => _targetCheckPointIndex;

    public void ChangeNextCheckPoint(int index)
    {
        _targetCheckPoint = _enemyCheckPoints.GetEnemyCheckPointByIndex(index);
        _targetCheckPointIndex = index;
        if (_targetCheckPoint)
        {
            transform.LookAt(_targetCheckPoint.transform.position);
        }
    }

    public void InitializeMove(EnemyCheckPoints enemyCheckPoints, float speed, EnemyCheckPoint targetCheckPoint)
    {
        _enemyCheckPoints = enemyCheckPoints;
        _speed = speed;
        _enemy = GetComponent<Enemy>();
        _enemy.OnDied += StopMove;
        _enemy.OnFoundOpponent += StopMove;
        _targetCheckPoint = targetCheckPoint;
        StartMove();
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void StartMove()
    {
        Debug.Log(gameObject.name +  " Start move");
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
            transform.position = Vector3.MoveTowards(transform.position, _targetCheckPoint.transform.position, _speed);
            transform.LookAt(_targetCheckPoint.transform.position);
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
