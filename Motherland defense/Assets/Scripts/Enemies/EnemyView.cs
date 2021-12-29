using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    // change type later
    [SerializeField] private GameObject _healthBar;
    public LevelStatistics _levelStatistics;
    private Enemy _enemy;

    private void Start()
    {
        _enemy.OnDamageTaken += _levelStatistics.AddTotalDamage;
        _enemy.OnDamageTaken += SetNewHealthValue;
    }

    private void SetNewHealthValue(int newHealth, int damage)
    {
        
    }
}
