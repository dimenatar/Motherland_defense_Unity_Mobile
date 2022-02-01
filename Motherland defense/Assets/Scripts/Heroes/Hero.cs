using System;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    public delegate void Damaged(int updatedHealth, int damage);
    public event Damaged OnDamageTaken;
    public delegate void EnemyInteract(GameObject enemy);
    public EnemyInteract OnTargetChanged;
    public EnemyInteract OnStartFight;

    public event Action OnDied;
    public event Action OnBasePointReached;

    public CharacterData HeroData => _characterData;

    private int _health;
    public GameObject _currentTarget;
    private CharacterData _characterData;
    public List<GameObject> _enemies = new List<GameObject>();

    public void InitializeHero(CharacterData characterData,  Transform basePointToMove, float arrivalToPointRange, ViewPanel viewPanel)
    {
        OnDied += RenameHero;
        OnDied += RemoveScripts;
        _health = characterData.Health;
        _characterData = characterData;
        GetComponent<HeroMove>().Initialise(basePointToMove, characterData.Speed, arrivalToPointRange);
        GetComponent<HeroFight>().Initialise(characterData.PauseBetweenAttacks, characterData.Damage);
        GetComponent<HeroAudio>().Initialise(characterData.HitSound, characterData.AttackSound, characterData.DieSound);
        GetComponent<HeroView>().Initialise(viewPanel);
    }

    public void ReachBasePoint()
    {
        OnBasePointReached?.Invoke();
    }

    //public void RemoveCurrentTarget()
    //{
    //    RemoveTarget(_currentTarget);
    //}

    public void ChangeTarget()
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
        target.GetComponent<Enemy>().OnRemovedFromList += RemoveTarget;
        if (!_currentTarget)
        {
            _currentTarget = target;
            ChangeTarget();
        }
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        Debug.Log($"{gameObject.name} получил урон {damage} осталось хп {_health}");
        OnDamageTaken?.Invoke(_health, damage);
        if (_health <= 0)
        {
            OnDied?.Invoke();
        }
    }

    public int GetHealth()
    {
        return _health;
    }

    public void RemoveTarget(GameObject target)
    {
        target.GetComponent<Enemy>().OnRemovedFromList -= RemoveTarget;
        if (_enemies.Contains(target))
        {
            _enemies.Remove(target);
            if (_currentTarget == target)
            {
                _currentTarget = null;
                ChangeTarget();
            }
        }
    }

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

    private void RenameHero()
    {
        gameObject.name = "Dead hero";
    }

    private void RemoveScripts()
    {
        Debug.Log("Remove");
        var scripts = GetComponents<MonoBehaviour>();
        foreach (var script in scripts)
        {
            Destroy(script);
        }
    }

    private void OnDestroy()
    {
        if (_currentTarget)
        {
            _currentTarget.GetComponent<Enemy>().OnRemovedFromList -= RemoveTarget;
        }
    }

    private void SetNewTarget()
    {
        _currentTarget = FindClosestEnemy();
        if (_currentTarget)
        {
            OnDied += _currentTarget.GetComponent<EnemyFight>().ChangeTarget;
            OnTargetChanged?.Invoke(_currentTarget);
        }
    }
}
