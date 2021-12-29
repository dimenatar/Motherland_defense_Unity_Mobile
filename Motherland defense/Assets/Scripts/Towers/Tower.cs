using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour, ITower
{
    [SerializeField] float _reloadTime;
    [SerializeField] Transform _shotStartPosition;

    public GameObject _target = null;
    public List<GameObject> _enemiesInArea = new List<GameObject>();

    private Vector3 diff;
    private float rotateZ;

    public virtual IEnumerator Shoot()
    {
        yield return null;
    }

    public float GetTransform()
    {
        diff = GetTarget().transform.position - GetShotStartPosition().position;
        rotateZ = Mathf.Atan2(diff.x, diff.z) * Mathf.Rad2Deg;
        return rotateZ;
    }
    public void AddEnemyInArea(GameObject enemy)
    {
        if (!_enemiesInArea.Contains(enemy))
        {
            _enemiesInArea.Add(enemy);
            enemy.GetComponent<Enemy>().OnDied += RemoveTarget;
        }

        if (!_target)
        {
            ChangeTarget();
        }
    }

    public void RemoveEnemyInArea(GameObject enemy)
    {
        _enemiesInArea.Remove(enemy);
        enemy.GetComponent<Enemy>().OnDied -= RemoveTarget;
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

    private void RemoveTarget()
    {
        _enemiesInArea.Remove(_target);
        _target.GetComponent<Enemy>().OnDied -= RemoveTarget;
        ChangeTarget();
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
