using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class GymImage : MonoBehaviour, IClickable, ITowerLoader
{
    [SerializeField] private int _cost;
    private GameObject _towerSpot;

    public void Deselect() { }

    public GameObject LoadTower()
    {
        return Resources.Load<GameObject>("GymPrefab");
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
