using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : MonoBehaviour
{
    [SerializeField] private GameObject _model;
    [SerializeField] private int _speed;
    [SerializeField] private int _damage;
    [SerializeField] private int _health;

    public GameObject Model => _model;
    public int Speed => _speed;
    public int Damage => _damage;
    public int Health => _health;
}
