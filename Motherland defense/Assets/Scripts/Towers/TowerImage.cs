using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerImage : MonoBehaviour, IClickable, ITowerLoader
{
    [SerializeField] TowerData _towerData;
    private UserMoney _money;
    private GameObject _towerSpot;

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
            _towerSpot.GetComponent<TowerSpot>().CreateTower(this);
        }
    }

    private void Initialise()
    {
        _money = _towerSpot.GetComponent<UserMoney>();
    }

    private void Start()
    {
        _towerSpot = transform.parent.parent.gameObject;
        _towerSpot.GetComponent<TowerSpot>().OnInitialised += Initialise;
    }
}
