using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Health : MonoBehaviour
{
    public delegate void HealthChanged(int value);
    public event HealthChanged OnHealthChanged;
    public event HealthChanged OnHealthSetted;

    public event Action OnLose;

    private Text _healthText;
    private int _currentHealth;
    public int CurrentHealth => _currentHealth;

    public void Initialise(int startValue)
    {
        _currentHealth = startValue;
        OnHealthSetted?.Invoke(startValue);
        OnHealthChanged?.Invoke(startValue);
        SetText(_currentHealth);
    }

    public void ReduceHealth(int value)
    {
        _currentHealth -= value;
        OnHealthChanged?.Invoke(value);
        if (_currentHealth <= 0)
        {
            OnLose?.Invoke();
        }
        SetText(_currentHealth);
    }

    private void Awake()
    {
        _healthText = GetComponent<Text>();
    }

    private void SetText(int value)
    {
        _healthText.text = value.ToString();
    }
}
