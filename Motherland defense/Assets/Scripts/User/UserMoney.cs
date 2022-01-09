using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public static class UserMoney
{
    public delegate void MoneyChanged(int amount);
    public static event MoneyChanged OnMoneyChanged;

    private static int _money;
    public static int Money 
    {   
        get => _money;
        set
        {
            _money = value;
            OnMoneyChanged?.Invoke(_money);
        }
    }

    public static void Initialise(int money)
    {
        Money = money;
    }

    public static void ReduceMoney(int amount)
    {
        if (Money < amount)
        {
            throw new ArgumentOutOfRangeException(nameof(amount));
        }
        Money -= amount;
    }

    public static bool IsEnoughMoney(int amount)
    {
        return Money >= amount;
    }

    public static void Increment()
    {
        Money++;    
    }
}
