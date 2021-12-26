using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(Transform))]
public class EnemyMove : MonoBehaviour
{


    [SerializeField] private EnemyCheckPoints _enemyCheckPoints;
    [SerializeField] private float _speed;
    [SerializeField] private int _targetCheckPointIndex;

    private Transform enemyPosition;
    private EnemyCheckPoint _targetCheckPoint;
    private Enemy _enemy;

    public virtual void ChangeNextCheckPoint(int index)
    {
        _targetCheckPoint = _enemyCheckPoints.GetEnemyCheckPointByIndex(index);
        if (_targetCheckPoint)
            enemyPosition.transform.LookAt(_targetCheckPoint.transform);
    }
    public void Initialize()
    {
        _enemy = GetComponent<Enemy>();
        _enemy.OnStartMove += StartMove;
        _enemy.OnStartFight += StartFight;
        _enemy.OnDied += StopMove;

        _targetCheckPoint = _enemyCheckPoints.GetEnemyCheckPointByIndex(_targetCheckPointIndex);
        enemyPosition = GetComponent<Transform>();
        StartMove();
    }
    
    private void Start()
    {
        Initialize();
    }
    // это вынести в другой класс
    public void StartFight()
    {
       // StopCoroutine(nameof(Move));
    }
    public void StartMove()
    {
        StartCoroutine(nameof(Move));
    }
    public void EndFight()
    {

    }
    public void StopMove()
    {
        StopCoroutine(nameof(Move));
    }

    public IEnumerator Move()
    {
        while (_targetCheckPoint)
        {
            //Debug.DrawLine(enemyPosition.position, _targetCheckPoint.transform.position, Color.red, 10);
            enemyPosition.position = Vector3.MoveTowards(enemyPosition.position, _targetCheckPoint.transform.position, Time.deltaTime * _speed);
            yield return new WaitForSeconds(0.01f);
        }
    }
    
    public float GetSpeed()
    {
        return _speed;
    }

    private Vector3 GetNextCheckPointDirection()
    {
        return Vector3.RotateTowards(enemyPosition.position, _targetCheckPoint.transform.position, 1,0);
    }    
}
