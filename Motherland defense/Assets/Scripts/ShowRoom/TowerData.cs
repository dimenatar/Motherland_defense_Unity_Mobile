using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TowerData 
{
    [SerializeField] private GameObject _model;
    [SerializeField] private string _name;
    [SerializeField] private int _radius;
    [SerializeField] private int _damage;
    [SerializeField] private int _cost;

    public GameObject Model => _model;
    public string Name => _name;
    public int Radius => _radius;
    public int Damage => _damage;
    public int Cost => _cost;
}
