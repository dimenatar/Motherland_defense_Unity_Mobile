using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpot : MonoBehaviour, IClickable
{
    [SerializeField] private TowerFactory _towerFactory;
    [SerializeField] private DirectionEnum.Directions _direction;
    private GameObject _tower = null;
    private ITowerLoader towerLoader;
    public void OnObjectClick()
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
    //убрать потом
    private void Start()
    {
        towerLoader = new CreateTing();
    }
}
