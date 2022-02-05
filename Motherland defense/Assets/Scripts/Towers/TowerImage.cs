using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerImage : MonoBehaviour, IClickable, ITowerLoader
{
    [SerializeField] TowerData _towerData;
    private UserMoney _money;
    private TowerSpot _towerSpot;

    public void Deselect() { }

    public virtual GameObject LoadTower()
    {
        return null;
    }

    public void ObjectClick()
    {
        if (_money.IsEnoughMoney(_towerData.Cost))
        {
            _money.ReduceMoney(_towerData.Cost);
            _towerSpot.CreateTower(this);
        }
    }

    private void Start()
    {
        _towerSpot = transform.parent.parent.gameObject.GetComponent<TowerSpot>();
        _money = _towerSpot.Money;
    }
}
