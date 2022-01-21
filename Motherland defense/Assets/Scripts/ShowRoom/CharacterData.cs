using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterData
{
    [SerializeField] private GameObject _model;
    [SerializeField] private string _name;
    [SerializeField] private int _speed;
    [SerializeField] private int _damage;
    [SerializeField] private int _health;

    public GameObject Model => _model;
    public string Name => _name;
    public int Speed => _speed;
    public int Damage => _damage;
    public int Health => _health;
}
