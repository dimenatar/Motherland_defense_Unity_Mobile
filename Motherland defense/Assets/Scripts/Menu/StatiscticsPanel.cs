using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatiscticsPanel : MonoBehaviour
{
    [SerializeField] private Text _remainingHealth;
    [SerializeField] private Text _damageToUnits;
    [SerializeField] private Text _amountEnemies;
    [SerializeField] private Text _timeToComplete;

    public void Initialise(int health, int damage, int amount, string time)
    {
        _remainingHealth.text = health.ToString();
        _damageToUnits.text = damage.ToString();
        _amountEnemies.text = amount.ToString();
        _timeToComplete.text = time.ToString();
    }
}
