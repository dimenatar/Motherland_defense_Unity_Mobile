using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TingImage : MonoBehaviour, IClickable, ITowerLoader
{
    [SerializeField] private TowerFactory _towerFactory;
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
        _towerSpot.GetComponent<TowerSpot>().CreateTower(this);
    }

    private void Start()
    {
        _towerSpot = transform.parent.parent.gameObject;
    }
}
