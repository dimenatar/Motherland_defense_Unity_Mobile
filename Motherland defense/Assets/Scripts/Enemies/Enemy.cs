using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private CharacterList _enemyName;
    private int _points;
    private int _health;
    private int _moneyAmountOnDeath;
    private UserMoney _money;
    private CharacterData _data;

    public delegate void Damaged(int health, int damage);
    public delegate void StartFight(GameObject opponent);

    public event Damaged OnDamageTaken;
    public event StartFight OnStartFight;
    public event Action OnStartMove;
    public event Action OnDied;
    public event Action OnFoundOpponent;

    public CharacterData Data => _data;

    public void Initialize(CharacterData data, EnemyCheckPoints enemyCheckPoints, UserMoney money, ViewPanel viewPanel)
    {
        _data = data;
        _enemyName = data.Name;
        _points = data.Points;
        _health = data.Health;
        _money = money;
        _moneyAmountOnDeath = data.MoneyAmountOnDeath;
        GetComponent<EnemyMove>().InitializeMove(enemyCheckPoints, data.Speed);
        GetComponent<EnemyFight>().InitialiseFight(data.PauseBetweenAttacks, data.Damage);
        GetComponent<EnemyAnimation>().InitialiseAnimation(data.AnimationSpeed);
        GetComponent<EnemyAudio>().Initialise(data.HitSound, data.AttackSound, data.DieSound);
        GetComponent<EnemyView>().Initialise(viewPanel);
        OnStartMove?.Invoke();
        //OnDied += RemoveComponents;
        OnDied += AddUserMoney;
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
            RemoveComponents();
        }
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

    public void AddUserMoney()
    {
        _money.AddMoney(_moneyAmountOnDeath);
    }

    private void OnDestroy()
    {
        OnDied?.Invoke();
    }
}
