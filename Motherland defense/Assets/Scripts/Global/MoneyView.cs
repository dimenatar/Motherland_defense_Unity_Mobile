using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class MoneyView : MonoBehaviour
{
    [SerializeField] private int _startMoney;
    private Text _moneyText;

    private void SetText(int value)
    {
        _moneyText.text = value.ToString();
    }

    public void Initialise()
    {
        StartCoroutine(AddMoney());
    }

    private void Start()
    {
        UserMoney.Money = _startMoney;
        _moneyText = GetComponent<Text>();
        _moneyText.text = UserMoney.Money.ToString();
        UserMoney.OnMoneyChanged += SetText;
        Initialise();
    }

    private void OnDestroy()
    {
        UserMoney.OnMoneyChanged -= SetText;
    }

    public IEnumerator AddMoney()
    {
        while (true)
        {
            UserMoney.Increment();
            yield return new WaitForSeconds(1);
        }
    }
}
