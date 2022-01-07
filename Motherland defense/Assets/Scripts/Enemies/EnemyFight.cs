using System.Collections;
using UnityEngine;

public class EnemyFight : MonoBehaviour
{
    // изменить тип потом
    [SerializeField] private float _pauseBetweenAttacks;
    [SerializeField] private int _damage;

    public delegate void HitOpponent(Hero hero);
    public event HitOpponent OnHitOpponent;

    private GameObject Opponent = null;
    private Enemy _enemy;

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
        Debug.Log(opponent.name);
        if (opponent.GetComponent<Hero>() && !Opponent)
        {
            Opponent = opponent;
            StartCoroutine(nameof(FightWithOpponent));
        }
    }

    public void StopFight()
    {
        Debug.Log("enemy stop fight");
        Opponent = null;
        StopCoroutine(nameof(FightWithOpponent));
    }
}
