using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider health;

    public void SetSliderValue(int value)
    {
        health.value = value;
    }
    public void SetSliderMaxValue(int value)
    {
        health.maxValue = value;
        health.value = value;
    }
}
