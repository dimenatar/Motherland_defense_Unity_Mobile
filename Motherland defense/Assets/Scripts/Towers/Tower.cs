using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour, ITower
{
    [SerializeField] float _reloadTime;
    [SerializeField] Transform _shotStartPosition;

    public GameObject _target = null;
    private List<GameObject> _enemiesInArea = new List<GameObject>();
    
    public virtual IEnumerator Shoot()
    {
        yield return null;
    }

    public void AddEnemyInArea(GameObject enemy)
    {
        if (!_enemiesInArea.Contains(enemy))
        {
            _enemiesInArea.Add(enemy);
        }

        if (!_target)
        {
            ChangeTarget();
        }
    }

    public void RemoveEnemyInArea(GameObject enemy)
    {
        _enemiesInArea.Remove(enemy);

        if (_target == enemy)
        {
            ChangeTarget();
        }
    }

    public GameObject GetTarget()
    {
        return _target;
    }

    public float GetReloadTime()
    {
        return _reloadTime;
    }

    public Transform GetShotStartPosition()
    {
        return _shotStartPosition;
    }

    private void ChangeTarget()
    {
        if (_enemiesInArea.Count > 0)
        {
            _target = _enemiesInArea[Random.Range(0, _enemiesInArea.Count - 1)];
        }
        else
        {
            _target = null;
        }
    }

}
