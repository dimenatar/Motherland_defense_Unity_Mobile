using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TingImage : MonoBehaviour, IClickable, ITowerLoader
{
    private GameObject _towerSpot;

    public bool IsSelected { get; set; }

    public void Deselect()
    {
        IsSelected = false;
    }

    public GameObject LoadTower()
    {
        GameObject gym = Resources.Load<GameObject>("TingPrefab");
        gym.transform.position += new Vector3(0, 4, 0);
        return gym;
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
