using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;
    [InspectorName("Enemy name")]
    [SerializeField] private string _enemyClassName;
    [SerializeField] private int _points;

    public delegate void Damaged(int health, int damage);
    public event Damaged OnDamageTaken;

    public delegate void State();
    public event State OnStartMove;
    public event State OnStartFight;
    public event State OnDied;

    public void Initialize()
    {
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

    private void RemoveComponents()
    {

        var scripts = GetComponents<ScriptableObject>();
        foreach (var item in scripts)
        {
            Destroy(item);
        }
        Destroy(GetComponent<BoxCollider>());
        Destroy(GetComponent<Animator>());
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
