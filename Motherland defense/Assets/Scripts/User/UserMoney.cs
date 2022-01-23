using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UserMoney : MonoBehaviour
{
    [SerializeField] private int _startMoney;

    public delegate void MoneyChanged(int changedValue);
    public event MoneyChanged OnMoneyChanged;

    private int _money = 0;

    private void Start()
    {
        AddMoney(_startMoney);
    }

    public void ReduceMoney(int cost)
    {
        _money -= cost;
        OnMoneyChanged?.Invoke(_money);
    }

    public bool IsEnoghtMoney(int value)
    {
        return _money >= value;
    }

    public void AddMoney(int amount)
    {
        _money += amount;
        OnMoneyChanged?.Invoke(_money);
    }

    public int GetMoney()
    {
        return _money;
    }
}
