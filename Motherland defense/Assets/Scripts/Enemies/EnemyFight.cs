using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFight : MonoBehaviour
{
    public GameObject _opponent = null;
    private Enemy _enemy;
    private float _pauseBetweenAttacks;
    private int _damage;

    public List<GameObject> _opponents = new List<GameObject>();

    public delegate void HitOpponent(Hero hero);
    public event HitOpponent OnHitOpponent;

    public event Action OnStopFight;

    public void InitialiseFight(float pauseBetweenAttacks, int damage)
    {
        _pauseBetweenAttacks = pauseBetweenAttacks;
        _damage = damage;
    }

    public IEnumerator FightWithOpponent()
    {
        while (true)
        {
            if (_opponent)
            {
                transform.LookAt(_opponent.transform);
                OnHitOpponent?.Invoke(_opponent.GetComponent<Hero>());
            }
            yield return new WaitForSeconds(_pauseBetweenAttacks);
        }
    }

    private void HitHero(Hero hero)
    {
        hero.TakeDamage(_damage);
    }

    private void OnDestroy()
    {
        _enemy.OnStartFight -= StartFight;
        _enemy.OnDied -= StopFight;
        OnHitOpponent -= HitHero;
        OnStopFight -= StopFight;
        OnStopFight -= GetComponent<EnemyMove>().StartMove;
    }

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
        _enemy.OnStartFight += StartFight;
        _enemy.OnDied += StopFight;
        OnHitOpponent += HitHero;
        OnStopFight += StopFight;
        OnStopFight += GetComponent<EnemyMove>().StartMove;
    }

    public void StartFight(GameObject opponent)
    {
        _opponents.Add(opponent);
        if (opponent.GetComponent<Hero>() && !_opponent)
        {
            _opponent = opponent;
            StartCoroutine(nameof(FightWithOpponent));
        }
    }

    public void ChangeTarget()
    {
        if (_opponent)
        {
            _opponents.Remove(_opponent);
            if (_opponents.Count > 0)
            {
                _opponent = _opponents[0];
            }
            else
            {
                OnStopFight?.Invoke();
            }
        }
        else
        {
            OnStopFight?.Invoke();
        }
    }

    private void StopFight()
    {
        if (_opponent)
        {
            _opponent = null;
            StopCoroutine(nameof(FightWithOpponent));
        }
    }
}
