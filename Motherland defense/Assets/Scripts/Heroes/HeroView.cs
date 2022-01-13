using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroView : MonoBehaviour
{
    [SerializeField] private HealthBar _healthBar;
    private Hero _hero;

    private void Start()
    {
        _hero = GetComponent<Hero>();
        _hero.OnDamageTaken += SetNewHealthValue;
        _healthBar.SetSliderMaxValue(_hero.GetHealth());
    }

    private void SetNewHealthValue(int newHealth, int damage)
    {
        _healthBar.SetSliderValue(newHealth);
    }
}
