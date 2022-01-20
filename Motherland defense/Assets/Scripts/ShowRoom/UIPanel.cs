using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanel : MonoBehaviour
{
    [Header("put in right order. Like for characer: speed, damage and health")]
    [SerializeField] private UICharacteristic _characteristic;
    [SerializeField] private UICharacteristic _characteristic1;
    [SerializeField] private UICharacteristic _characteristic2;

    
    public void Initialise(int maxValue1, int maxValue2, int maxValue3)
    {
        _characteristic.SetSliderMaxValue(maxValue1);
        _characteristic1.SetSliderMaxValue(maxValue2);
        _characteristic2.SetSliderMaxValue(maxValue3);
    }

    public void SetValues(int value1, int value2, int value3)
    {
        _characteristic.SetValues(value1);
        _characteristic1.SetValues(value2);
        _characteristic2.SetValues(value3);
    }
}
