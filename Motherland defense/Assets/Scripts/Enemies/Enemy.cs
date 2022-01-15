using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [InspectorName("Enemy name")]
    private EnemyList _enemyName;
    private int _points;
    private int _health;

    public delegate void Damaged(int health, int damage);
    public delegate void StartFight(GameObject opponent);
    public event Damaged OnDamageTaken;
    public event StartFight OnStartFight;
    public event Action OnStartMove;
    public event Action OnDied;
    public event Action OnFoundOpponent;

    public void Initialize(EnemyList enemyName, int points, int health, float pauseBetweenAttacks, int damage, EnemyCheckPoints enemyCheckPoints, 
                           float speed, float animationSpeed, AudioClip hitSound, AudioClip attackSound, AudioClip dieSound)
    {
        _enemyName = enemyName;
        _points = points;
        _health = health;
        GetComponent<EnemyMove>().InitializeMove(enemyCheckPoints, speed);
        GetComponent<EnemyFight>().InitialiseFight(pauseBetweenAttacks, damage);
        GetComponent<EnemyAnimation>().InitialiseAnimation(animationSpeed);
        GetComponent<EnemyAudio>().Initialise(hitSound, attackSound, dieSound);
        OnStartMove?.Invoke();
        OnDied += RemoveComponents;
    }

    public int GetPoints()
    {
        return _points;
    }

    public int GetHealth()
    {
        return _health;
    }

    public void StartFightWith(GameObject opponent)
    {
        OnStartFight?.Invoke(opponent);
    }

    private void RemoveComponents()
    {

        var scripts = GetComponents<MonoBehaviour>();
        foreach (var item in scripts)
        {
            Destroy(item);
        }
        Destroy(GetComponent<BoxCollider>());
    }

    public void FoundOpponent()
    {
        OnFoundOpponent?.Invoke();
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        OnDamageTaken?.Invoke(GetHealth(), damage);
        if (_health <= 0)
        {
            OnDied?.Invoke();
        }
    }
}
