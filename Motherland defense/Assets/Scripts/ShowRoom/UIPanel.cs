using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPanel : MonoBehaviour
{
    [Header("put in right order. Like for characer: speed, damage and health")]
    [SerializeField] private UICharacteristic _characteristic;
    [SerializeField] private UICharacteristic _characteristic1;
    [SerializeField] private UICharacteristic _characteristic2;
    [SerializeField] private Text _name;
    
    public void SetMaxValues(int maxValue1, int maxValue2, int maxValue3)
    {
        _characteristic.SetSliderMaxValue(maxValue1);
        _characteristic1.SetSliderMaxValue(maxValue2);
        _characteristic2.SetSliderMaxValue(maxValue3);
    }

    public void SetValues(int value1, int value2, int value3, string name)
    {
        _characteristic.SetValues(value1);
        _characteristic1.SetValues(value2);
        _characteristic2.SetValues(value3);
        _name.text = name;
    }
}
