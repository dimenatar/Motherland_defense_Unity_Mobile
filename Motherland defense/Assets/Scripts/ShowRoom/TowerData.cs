using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerData : ScriptableObject
{
    [SerializeField] private GameObject _model;
    [SerializeField] private float _radius;
    [SerializeField] private int _damage;
    [SerializeField] private int _cost;

    public GameObject Model => _model;
    public float Radius => _radius;
    public int Damage => _damage;
    public int Cost => _cost;
}
