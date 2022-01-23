using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerImage : MonoBehaviour, IClickable, ITowerLoader
{
    [SerializeField] private int _cost;
    [SerializeField] private UserMoney _money;
    private GameObject _towerSpot;

    public void Deselect() { }

    public virtual GameObject LoadTower()
    {
        return null;
    }

    public void ObjectClick()
    {
        if (_money.IsEnoghtMoney(_cost))
        {
            _money.ReduceMoney(_cost);
            _towerSpot.GetComponent<TowerSpot>().CreateTower(this);
        }
    }

    private void Start()
    {
        _towerSpot = transform.parent.parent.gameObject;
    }
}
