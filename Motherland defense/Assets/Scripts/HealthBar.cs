using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _health;
    [SerializeField] private Gradient _healthColor;
    [SerializeField] private Image _fill;

    public void SetSliderValue(int value)
    {
        _health.value = value;
        _fill.color = _healthColor.Evaluate(_health.normalizedValue);
        
    }
    public void SetSliderMaxValue(int value)
    {
        _health.maxValue = value;
        _health.value = value;
        _fill.color = _healthColor.Evaluate(1);
    }
}
