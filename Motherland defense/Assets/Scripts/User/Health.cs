using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Health : MonoBehaviour
{
    private Text _healthText;
    private int _startHealth;
    private int _currentHealth;

    public void Initialise(int startValue)
    {
        _startHealth = startValue;
        _currentHealth = startValue;
        SetText(_currentHealth);
    }

    public void ReduceHealth(int value)
    {
        _currentHealth -= value;
        SetText(_currentHealth);
    }

    private void Awake()
    {
        _healthText = GetComponent<Text>();
    }

    private void Start()
    {
        
    }

    private void SetText(int value)
    {
        _healthText.text = value.ToString();
    }

    public float GetHealthRatio()
    {
        return _startHealth / _currentHealth;
    }

}
