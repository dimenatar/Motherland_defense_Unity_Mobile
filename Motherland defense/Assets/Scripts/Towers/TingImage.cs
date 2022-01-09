using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TingImage : MonoBehaviour, IClickable, ITowerLoader
{
    [SerializeField] private int _cost;
    private GameObject _towerSpot;

    public bool IsSelected { get; set; }

    public void Deselect()
    {
        IsSelected = false;
    }

    public GameObject LoadTower()
    {
        return Resources.Load<GameObject>("TingPrefab");
    }

    public void ObjectClick()
    {
        if (UserMoney.IsEnoughMoney(_cost))
        {
            UserMoney.ReduceMoney(_cost);
            _towerSpot.GetComponent<TowerSpot>().CreateTower(this);
        }
    }

    private void Start()
    {
        _towerSpot = transform.parent.parent.gameObject;
    }
}
