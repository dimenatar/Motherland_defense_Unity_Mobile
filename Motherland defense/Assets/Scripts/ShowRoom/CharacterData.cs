using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Data", menuName = "Character Data", order = 13)]
[Serializable]
public class CharacterData : ScriptableObject
{
    [SerializeField] private GameObject _model;
    [SerializeField] private EnemyList _name;
    [SerializeField] private int _points;
    [SerializeField] private float _pauseBetweenAttacks;
    [SerializeField] private float _animationSpeed;
    [SerializeField] private int _moneyAmountOnDeath;
    [SerializeField] private AudioClip _hitSound;
    [SerializeField] private AudioClip _attackSound;
    [SerializeField] private AudioClip _dieSound;
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;
    [SerializeField] private int _health;

    public GameObject Model => _model;
    public EnemyList Name => _name;
    public float Speed => _speed;
    public int Damage => _damage;
    public int Health => _health;
    public int Points => _points;
    public float PauseBetweenAttacks => _pauseBetweenAttacks;
    public float AnimationSpeed => _animationSpeed; 
    public AudioClip AttackSound => _attackSound;
    public AudioClip DieSound => _dieSound;
    public int MoneyAmountOnDeath => _moneyAmountOnDeath;
    public AudioClip HitSound => _hitSound;
}
