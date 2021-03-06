using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, ICharacter
{
    private CharacterList _enemyName;
    private int _points;
    private int _health;
    private int _moneyAmountOnDeath;
    private UserMoney _money;
    private CharacterData _data;
    private bool _isDead = false;

    public delegate void Damaged(int health, int damage);
    public delegate void StartFight(GameObject opponent);
    public delegate void RemovedFromList (GameObject current);

    public event RemovedFromList OnRemovedFromList;
    public event Damaged OnDamageTaken;
    public event StartFight OnStartFight;
    public event Action OnDied;
    public event Action OnFoundOpponent;
    public event Action OnDestroed;

    public CharacterData Data => _data;

    private void OnDestroy()
    {
        OnDestroed?.Invoke();
    }

    public void Initialize(CharacterData data, EnemyCheckPoints enemyCheckPoints, Vector3 targetCheckPoint, UserMoney money, ViewPanel viewPanel)
    {
        _data = data;
        _enemyName = data.Name;
        _points = data.Points;
        _health = data.Health;
        _money = money;
        _moneyAmountOnDeath = data.MoneyAmountOnDeath;
        GetComponent<EnemyMove>().InitializeMove(enemyCheckPoints, data.Speed, targetCheckPoint);
        GetComponent<EnemyFight>().InitialiseFight(data.PauseBetweenAttacks, data.Damage);
        GetComponent<EnemyAnimation>().InitialiseAnimation(data.AnimationSpeed);
        GetComponent<EnemyAudio>().Initialise(data.HitSound, data.AttackSound, data.DieSound);
        GetComponent<EnemyView>().Initialise(viewPanel);
        OnDied += AddUserMoney;
        OnDied += ChangeNameToDied;
        OnDied += AddDestroyEffect;
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
        if (!_isDead)
        {
            _health -= damage;
            OnDamageTaken?.Invoke(GetHealth(), damage);
            if (_health <= 0)
            {
                _isDead = true;
                RemoveComponents();
                OnRemovedFromList?.Invoke(gameObject);
                OnDied?.Invoke();
            }
        }
    }

    private void AddDestroyEffect()
    {
        gameObject.AddComponent<DestroyBodyAfterTime>();
    }

    private void RemoveComponents()
    {
        var scripts = GetComponents<MonoBehaviour>();
        foreach (var item in scripts)
        {
            Destroy(item);
        }
        Destroy(GetComponent<BoxCollider>());
        Destroy(GetComponent<Rigidbody>());
    }

    private void ChangeNameToDied()
    {
        Debug.Log($"{gameObject.name} died");
        gameObject.name = "Died";
    }

    private void AddUserMoney()
    {
        _money.AddMoney(_moneyAmountOnDeath);
    }
}
