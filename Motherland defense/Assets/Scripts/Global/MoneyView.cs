using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class MoneyView : MonoBehaviour
{
    [SerializeField] private Text _moneyText;
    [SerializeField] private UserMoney _money;

    private void SetMoneyText(int value)
    {
        _moneyText.text = value.ToString();
    }

    private void Awake()
    {
        Debug.Log(UserProgressManager.Path);
        _money.OnMoneyChanged += SetMoneyText;
    }
}
