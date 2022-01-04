using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _attackDelay;

    private GameObject _currentTarget;

    public delegate void EnemyInteract(GameObject enemy);

    public EnemyInteract OnTargetChanged;
    public EnemyInteract OnStartFight;

    private List<GameObject> _enemies = new List<GameObject>();

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
        OnTargetChanged?.Invoke(_currentTarget);
    }

    private void ChangeTarget()
    {
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

    public void RemoveTarget(GameObject target)
    {
        if (_enemies.Contains(target))
        {
            _enemies.Remove(target);
            _currentTarget = null;
        }
        ChangeTarget();
    }
}
