using System;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _health;
    [SerializeField] private float _attackDelay;

    public GameObject _currentTarget;

    public delegate void EnemyInteract(GameObject enemy);
    public EnemyInteract OnTargetChanged;
    public EnemyInteract OnStartFight;

    public event Action OnDied;
    
    public event Action OnBasePointReached;

    public List<GameObject> _enemies = new List<GameObject>();

    private GameObject FindClosestEnemy()
    {
        GameObject closestEnemy = _enemies[0];
        Vector3 heroPosition = transform.position;
        float closestDistance = Mathf.Abs(Vector3.Distance(heroPosition, closestEnemy.transform.position));
        float currentDistance = 0;
        for (int i = 1; i < _enemies.Count; i++)
        {
            currentDistance = Mathf.Abs(Vector3.Distance(heroPosition, _enemies[i].transform.position));
            if (closestDistance > currentDistance)
            {
                closestDistance = currentDistance;
                closestEnemy = _enemies[i];
            }
        }
        return closestEnemy;
    }

    public void SetNewTarget()
    {
        _currentTarget = FindClosestEnemy();
        OnDied += _currentTarget.GetComponent<EnemyFight>().StopFight;
        OnDied += _currentTarget.GetComponent<EnemyMove>().StartMove;
        _currentTarget.GetComponent<Enemy>().OnDied += RemoveCurrentTarget;
        OnTargetChanged?.Invoke(_currentTarget);
    }

    public void ReachBasePoint()
    {
        OnBasePointReached?.Invoke();
    }

    public void RemoveCurrentTarget()
    {
        RemoveTarget(_currentTarget);
    }

    public void ChangeTarget()
    {
        Debug.Log("ChangeTarget");
        if (_enemies.Count > 0)
        {
            SetNewTarget();
        }
        else
        {
            OnTargetChanged?.Invoke(null);
        }
    }

    public void AddNewTarget(GameObject target)
    {
        _enemies.Add(target);
        if (!_currentTarget)
        {
            _currentTarget = target;
            ChangeTarget();
        }
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            OnDied?.Invoke();
        }
    }

    public void RemoveTarget(GameObject target)
    {
        if (_enemies.Contains(target))
        {
            OnDied -= target.GetComponent<EnemyFight>().StopFight;
            _enemies.Remove(target);
            if (_currentTarget == target)
            {
                Debug.Log("if");
                _currentTarget = null;
                ChangeTarget();
            }
        }
    }
}
