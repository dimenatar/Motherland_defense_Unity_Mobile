using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICharacteristic : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Text _value;

    public void SetSliderMaxValue(int value)
    {
        _slider.maxValue = value;
    }

    public void SetValues(int value)
    {
        _slider.value = value;
        _value.text = value.ToString();
    }
}
