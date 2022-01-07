using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroFight : MonoBehaviour
{
    [SerializeField] private float _attackDelay= 0.5f;
    [SerializeField] private int _damage = 1;
    private Enemy _target;
    private Hero hero;

    private void Start()
    {
        hero = GetComponent<Hero>();
        hero.OnStartFight += StartFight;
    }

    private void StartFight(GameObject enemy)
    {
        Debug.Log("enemy " + enemy.name);
        _target = enemy.GetComponent<Enemy>();
        _target.OnDied += StopFight;
        StartCoroutine(nameof(FightWithEnemy));
    }

    private void StopFight()
    {
        _target.OnDied -= StopFight;
        StopCoroutine(nameof(FightWithEnemy));

    }

    public IEnumerator FightWithEnemy()
    {
        while (true)
        {
            Debug.Log("hero hit");
            _target.TakeDamage(_damage);
            yield return new WaitForSeconds(_attackDelay);
        }
    }
}
