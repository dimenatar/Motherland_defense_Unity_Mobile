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
    }

    private void Awake()
    {

    }

    private void Start()
    {
       // Initialize();
    }

    private void OnEnable()
    {
        
    }

    private void OnDestroy()
    {
        OnDamageTaken = null;
    }

    public int GetPoints()
    {
        return _points;
    }

    public int GetHealth()
    {
        return _health;
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
