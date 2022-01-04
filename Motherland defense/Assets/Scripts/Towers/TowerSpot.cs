using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider))]
public class TowerSpot : MonoBehaviour, IClickable
{
    [SerializeField] private TowerFactory _towerFactory;
    [SerializeField] private DirectionEnum.Directions _direction;
    [SerializeField] private ClickManager _clickManager;
    [SerializeField] private Canvas _towerMenu;
    private GameObject _tower = null;
    private BoxCollider _collider;

    public void ObjectClick()
    {
        if (_tower)
        {
            //call tower view 
        }
        else
        {
            _clickManager.OnObjectClick -= Deselect;
            _collider.enabled = false;
            _towerMenu.gameObject.transform.position = transform.position + new Vector3(0,0.1f,0);
            _towerMenu.gameObject.SetActive(true);
        }
    }

    public void CreateTower(ITowerLoader towerLoader)
    {
        _tower = _towerFactory.CreateTower(towerLoader, transform.position, _direction);
        _tower.transform.SetParent(transform);
    }

    //public void SetTowerLoader(ITowerLoader towerLoader)
    //{
    //    _towerLoader = towerLoader;
    //}

    //public DirectionEnum.Directions GetDirection()
    //{
    //    return _direction;
    //}

    //public void BuildTower()
    //{
    //    _towerFactory.CreateTower(_towerLoader, transform.position, _direction);
    //    _collider.enabled = true;
    //    _clickManager.OnObjectClick += Deselect;
    //}

    public void Deselect()
    {
        //Debug.Log("deselect");
        //for (int i = 0; i < _towerMenu.transform.childCount; i++)
        //{
        //    Debug.Log(_towerMenu.transform.GetChild(i).gameObject.name);
        //    if (_towerMenu.transform.GetChild(i).gameObject.GetComponent<IClickable>().IsSelected)
        //    {
        //        Debug.Log("selected");
        //        return;
        //    }
        //}
        _towerMenu.gameObject.SetActive(false);
        _collider.enabled = true;
        //_clickManager.OnObjectClick += Deselect;
    }

    //убрать потом
    private void Start()
    {
        //towerLoader = new LoadTing();
        _clickManager.OnObjectClick += Deselect;
        _collider = GetComponent<BoxCollider>();
    }
}
