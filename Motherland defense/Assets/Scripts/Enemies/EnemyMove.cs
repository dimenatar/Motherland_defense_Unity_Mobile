using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(Rigidbody))]
public class EnemyMove : MonoBehaviour
{


    [SerializeField] private EnemyCheckPoints _enemyCheckPoints;
    [SerializeField] private float _speed;
    [SerializeField] private int _targetCheckPointIndex;

    private EnemyCheckPoint _targetCheckPoint;
    private Enemy _enemy;
    private Rigidbody _rigidbody;
    public virtual void ChangeNextCheckPoint(int index)
    {
        _targetCheckPoint = _enemyCheckPoints.GetEnemyCheckPointByIndex(index);
        if (_targetCheckPoint)
        {
            transform.LookAt(_targetCheckPoint.transform.position);
        }
    }
    public void Initialize()
    {
        _enemy = GetComponent<Enemy>();
        _enemy.OnStartMove += StartMove;
        _enemy.OnStartFight += StartFight;
        _enemy.OnDied += StopMove;

        _targetCheckPoint = _enemyCheckPoints.GetEnemyCheckPointByIndex(_targetCheckPointIndex);
        
        StartMove();
    }
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
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
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetCheckPoint.transform.position, _speed);
            transform.LookAt(_targetCheckPoint.transform.position);
            //transform.Translate(transform.forward * _speed);
            yield return new WaitForSeconds(0.01f);
        }
    }
    
    
    public float GetSpeed()
    {
        return _speed;
    }

    private Vector3 GetNextCheckPointDirection()
    {
        return Vector3.RotateTowards(transform.position, _targetCheckPoint.transform.position, 1,0);
    }    
}
