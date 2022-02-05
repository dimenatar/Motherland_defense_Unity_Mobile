using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider))]
public class TowerSpot : MonoBehaviour, IClickable
{
    [SerializeField] private DirectionEnum.Directions _direction;
    [SerializeField] private TowerBundle _towers;
    [SerializeField] public Canvas _towerMenu;
    [SerializeField] private GameObject _towerRangeImage;
    [SerializeField] private TowerFactory _towerFactory;
    [SerializeField] private ViewPanel _viewPanel;

    private Tower _tower = null;
    private BoxCollider _collider;
    private UserMoney _money;
    private bool _isSelected;

    public UserMoney Money => _money;
    public ViewPanel ViewPanel => _viewPanel;

    public void ObjectClick()
    {
        if (_tower)
        {
            _isSelected = true;
            _towerRangeImage.SetActive(true);
            _towerRangeImage.transform.localScale = new Vector2(_tower.Data.Radius, _tower.Data.Radius)/20;
            _viewPanel.ShowTowerPanel(_tower.Data.Name.ToString(), _tower.Data.Damage.ToString(), _tower.Data.Radius.ToString(), _tower.Data.ReloadTime.ToString(), gameObject);
        }
        else
        {
            _collider.enabled = false;
            _towerMenu.gameObject.transform.position = transform.position + new Vector3(0,0.1f,0);
            _towerMenu.gameObject.SetActive(true);
        }
    }

    public void RemoveTower()
    {
        Deselect();
        _money.AddMoney(_tower.Data.Cost);
        Destroy(_tower.gameObject);
        _tower = null;
    }

    public void Initialise(TowerFactory towerFactory, ViewPanel viewPanel, UserMoney userMoney)
    {
        _towerFactory = towerFactory;
        _viewPanel = viewPanel;
        _money = userMoney;
    }

    public void CreateTower(ITowerLoader towerLoader)
    {
        GameObject tower = _towerFactory.CreateTower(towerLoader, transform.position, _direction);
        _tower = tower.GetComponent<Tower>();
        _tower.GetComponent<Tower>().Initialise(_towers.Towers.Where(towerName => towerName.Name.ToString() == _tower.name).First());
        _tower.transform.SetParent(transform);
    }

    public void Deselect()
    {
        if (_tower && _isSelected)
        {
            _viewPanel.HidePanel();
            _towerRangeImage.SetActive(false);
            _isSelected = false;
        }
        _towerMenu.gameObject.SetActive(false);
        _collider.enabled = true;
    }

    private void Start()
    {
        _collider = GetComponent<BoxCollider>();
    }
}
