using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICharacteristic : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Text _value;

    public void SetSliderMaxValue(string value)
    {
        _slider.maxValue = float.Parse(value);
    }

    public void SetValues(string value)
    {
        _slider.value = float.Parse(value);
        _value.text = value;
    }
}
