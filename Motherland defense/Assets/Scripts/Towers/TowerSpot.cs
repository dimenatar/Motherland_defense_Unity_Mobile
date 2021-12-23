using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpot : MonoBehaviour, IClickable
{
    [SerializeField] private TowerFactory _towerFactory;
    [SerializeField] private DirectionEnum.Directions _direction;
    [SerializeField] private ClickManager _clickManager;
    private GameObject _tower = null;
    private ITowerLoader towerLoader;

    public void ObjectClick()
    {
        if (_tower)
        {
            //call tower view 
        }
        else
        {
            _towerFactory.CreateTower(towerLoader, transform.position, _direction);
        }
    }
    public void Deselect()
    {
        //
    }

    //убрать потом
    private void Start()
    {
        towerLoader = new CreateTing();
        _clickManager.OnObjectClick += ObjectClick;
    }
}
