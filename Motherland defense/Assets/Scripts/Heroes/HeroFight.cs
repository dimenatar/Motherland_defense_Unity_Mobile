using System.Collections;
using UnityEngine;

public class HeroFight : MonoBehaviour
{
    public delegate void HitEnemy(Enemy enemy);
    public event HitEnemy OnHitEnemy;

    private float _attackDelay= 0.5f;
    private int _damage = 1;
    private Enemy _target;
    private Hero hero;

    public void Initialise(float attackDelay, int damage)
    {
        _attackDelay = attackDelay;
        _damage = damage;
    }

    private void Start()
    {
        hero = GetComponent<Hero>();
        hero.OnStartFight += StartFight;
        OnHitEnemy += DamageEnemy;
    }

    private void StartFight(GameObject enemy)
    {
        _target = enemy.GetComponent<Enemy>();
        _target.OnDied += StopFight;
        hero.OnDied += StopFight;
        StartCoroutine(nameof(FightWithEnemy));
    }

    private void StopFight()
    {
        _target.OnDied -= StopFight;
        StopCoroutine(nameof(FightWithEnemy));

    }

    private void DamageEnemy(Enemy enemy)
    {
        enemy.TakeDamage(_damage);
    }

    public IEnumerator FightWithEnemy()
    {
        while (true)
        {
            if (_target)
            {
                OnHitEnemy?.Invoke(_target);
            }
            yield return new WaitForSeconds(_attackDelay);
        }
    }
}
