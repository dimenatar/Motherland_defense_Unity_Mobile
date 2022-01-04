using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroFight : MonoBehaviour
{
    private float _attackDelay= 0.5f;
    private int _damage = 1;
    private Enemy _target;
    private Hero hero;

    private void Start()
    {
        hero = GetComponent<Hero>();
        hero.OnStartFight += StartFight;
    }

    private void StartFight(GameObject enemy)
    {
        Debug.Log("StartFight");
        _target = enemy.GetComponent<Enemy>();
        _target.OnDied += StopFight;
        StartCoroutine(nameof(FightWithEnemy));
    }

    private void StopFight()
    {
        StopCoroutine(nameof(FightWithEnemy));
        _target.OnDied -= StopFight;
    }

    public IEnumerator FightWithEnemy()
    {
        while (true)
        {
            _target.TakeDamage(_damage);
            yield return new WaitForSeconds(_attackDelay);
        }
    }
}
