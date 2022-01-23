using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Tower Data", menuName = "Tower Data", order = 51)]
[Serializable]
public class TowerData : ScriptableObject
{
    [SerializeField] private GameObject _model;
    [SerializeField] private TowerList _name;
    [SerializeField] private int _radius;
    [SerializeField] private int _damage;
    [SerializeField] private int _cost;
    [SerializeField] private float _reloadTime;
    [SerializeField] private AudioClip _shotSound;

    public GameObject Model => _model;
    public TowerList Name => _name;
    public int Radius => _radius;
    public int Damage => _damage;
    public int Cost => _cost;
    public float ReloadTime => _reloadTime;
    public AudioClip ShotSound => _shotSound;
}
