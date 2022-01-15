using System.Collections;
using UnityEngine;

public class EnemyFight : MonoBehaviour
{
    private GameObject Opponent = null;
    private Enemy _enemy;
    private float _pauseBetweenAttacks;
    private int _damage;

    public delegate void HitOpponent(Hero hero);
    public event HitOpponent OnHitOpponent;

    public void InitialiseFight(float pauseBetweenAttacks, int damage)
    {
        _pauseBetweenAttacks = pauseBetweenAttacks;
        _damage = damage;
    }

    public IEnumerator FightWithOpponent()
    {
        while (true)
        {
            if (Opponent)
            {
                OnHitOpponent?.Invoke(Opponent.GetComponent<Hero>());
            }
            yield return new WaitForSeconds(_pauseBetweenAttacks);
        }
    }

    private void HitHero(Hero hero)
    {
        hero.TakeDamage(_damage);
    }

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
        _enemy.OnStartFight += StartFight;
        _enemy.OnDied += StopFight;
        OnHitOpponent += HitHero;
    }

    public void StartFight(GameObject opponent)
    {
        if (opponent.GetComponent<Hero>() && !Opponent)
        {
            Opponent = opponent;
            StartCoroutine(nameof(FightWithOpponent));
        }
    }

    public void StopFight()
    {
        if (Opponent)
        {
            Opponent = null;
            StopCoroutine(nameof(FightWithOpponent));
        }
    }
}
