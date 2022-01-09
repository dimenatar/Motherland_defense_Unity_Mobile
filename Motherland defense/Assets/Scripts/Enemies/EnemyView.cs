using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    // change type later
    [SerializeField] private HealthBar _healthBar;
    public LevelStatistics _levelStatistics;
    private Enemy _enemy;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
        _enemy.OnDamageTaken += _levelStatistics.AddTotalDamage;
        _enemy.OnDamageTaken += SetNewHealthValue;
        _healthBar.SetSliderMaxValue(_enemy.GetHealth());
    }

    private void SetNewHealthValue(int newHealth, int damage)
    {
        _healthBar.SetSliderValue(newHealth);
    }
}
