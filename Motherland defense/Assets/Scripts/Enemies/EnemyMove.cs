using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(Transform))]
public class EnemyMove : MonoBehaviour, IEnemyMove
{
    [SerializeField] private EnemyCheckPoints _enemyCheckPoints;
    [SerializeField] private float _speed;

    private EnemyCheckPoint _targetCheckPoint;
    [SerializeField]  private int _targetCheckPointIndex;
    private Transform enemyPosition;

    public virtual void ChangeNextCheckPoint(int index)
    {
        Debug.Log(index);
        _targetCheckPoint = _enemyCheckPoints.GetEnemyCheckPointByIndex(index);
        if (_targetCheckPoint)
            enemyPosition.transform.LookAt(_targetCheckPoint.transform);


    }
    public void Initialize()
    {
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
        StopCoroutine(nameof(Move));
    }
    public void StartMove()
    {
        StartCoroutine(nameof(Move));
    }
    public void EndFight()
    {

    }
    private Vector3 GetNextCheckPointDirection()
    {
        return Vector3.RotateTowards(enemyPosition.position, _targetCheckPoint.transform.position, 1,0);
    }    
    public IEnumerator Move()
    {
        while (_targetCheckPoint)
        {
            enemyPosition.position = Vector3.MoveTowards(enemyPosition.position, _targetCheckPoint.transform.position, Time.deltaTime * _speed);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
