using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Tower : MonoBehaviour, ITower
{
    [SerializeField] private Transform _shotStartPosition;

    private float _reloadTime;
    public GameObject _target = null;
    public List<GameObject> _enemiesInArea = new List<GameObject>();
    private AudioSource _source;
    private AudioClip _shotSound;
    private Vector3 diff;
    private float rotateZ;
    private TowerData _data;

    public TowerData Data => _data;

    public void Initialise(TowerData data)
    {
        _data = data;
        _reloadTime = data.ReloadTime;
        _shotSound = data.ShotSound;
    }

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

    public void PlayShotSound()
    {
        if (_source == null)
        {
            _source = GetComponent<AudioSource>();
        }
        _source.PlayOneShot(_shotSound);
    }

    public void AddEnemyInArea(GameObject enemy)
    {
        if (!_enemiesInArea.Contains(enemy))
        {
            _enemiesInArea.Add(enemy);
            enemy.GetComponent<Enemy>().OnRemovedFromList += RemoveEnemyInArea;
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
        Debug.Log("Change target");
        if (_enemiesInArea.Count > 0)
        {
            _target = _enemiesInArea[Random.Range(0, _enemiesInArea.Count)];
        }
        else
        {
            _target = null;
        }
    }
}
